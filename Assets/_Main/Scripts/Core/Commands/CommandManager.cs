using UnityEngine;
using System.Reflection;
using System.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

namespace Commands
{
    public class CommandManager : MonoBehaviour
    {
        private const char Sub_Command_Identifier = '.';
        public static CommandManager instance { get; private set; }

        private CommandDatabase database;
        //private Dictionary<string, CommandDatabase> subDatabases;

        private List<CommandProcess> activeProcesses = new List<CommandProcess>();
        private CommandProcess topProcess => activeProcesses.Last();

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;

                database = new CommandDatabase();

                Assembly assembly = Assembly.GetExecutingAssembly();
                Type[] extensionTypes = assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(CMD_DatabaseExtension))).ToArray();

                foreach (Type extension in extensionTypes)
                {
                    MethodInfo extendMethod = extension.GetMethod("Extend");
                    extendMethod.Invoke(null, new object[] { database });
                }
            }
            else
                DestroyImmediate(gameObject);
        }

        public CoroutineWrapper Execute(string commandName, params string[] args)
        {
            //if (commandName.Contains(Sub_Command_Identifier))
               // return ExecuteSubCommand(commandName, args);
            Delegate command = database.GetCommand(commandName);

            if (command == null)
                return null;

            return StartProcess(commandName, command, args);
        }

        //private CoroutineWrapper ExecuteSubCommand(string commandName, string[] args)
        //{

        //}

        private CoroutineWrapper StartProcess(string commandName, Delegate command, string[] args)
        {
            System.Guid processID = System.Guid.NewGuid();
            CommandProcess cmd = new CommandProcess(processID, commandName, command, null, args, null);
            activeProcesses.Add(cmd);
            
            Coroutine co = StartCoroutine(RunningProcess(cmd));

            cmd.runningProcess = new CoroutineWrapper(this, co);

            return cmd.runningProcess;
        }

        public void StopCurrentProcess()
        {
            if (topProcess != null)
                KillProcess(topProcess);
        }

        public void StopAllProcesses()
        {
            foreach (var c in activeProcesses)
            {
                if (c.runningProcess != null && !c.runningProcess.isDone)
                    c.runningProcess.Stop();

                c.onTerminateAction?.Invoke();
            }

            activeProcesses.Clear();
        }

        private IEnumerator RunningProcess(CommandProcess process)
        {
            yield return WaitingForProcessToComplete(process.command, process.args);

            KillProcess(process);
        }

        public void KillProcess(CommandProcess cmd)
        {
            activeProcesses.Remove(cmd);

            if (cmd.runningProcess != null && !cmd.runningProcess.isDone)
                cmd.runningProcess.Stop();

            cmd.onTerminateAction?.Invoke();
            
        }

        public void AddTerminationActionToCurrentProcess(UnityAction action)
        {
            CommandProcess process = topProcess;
            if (process == null)
                return;

            process.onTerminateAction = new UnityEvent();
            process.onTerminateAction.AddListener(action);

        }

        private IEnumerator WaitingForProcessToComplete(Delegate command, string[] args)
        {
            // Action
            if (command is Action)
                command.DynamicInvoke();

            else if (command is Action<string>)
                command.DynamicInvoke(args[0]);

            else if (command is Action<string[]>)
                command.DynamicInvoke((object)args);

            // Lambda
            else if (command is Func<IEnumerator>)
                yield return ((Func<IEnumerator>)command)();

            else if (command is Func<string, IEnumerator>)
                yield return ((Func<string, IEnumerator>)command)(args[0]);

            else if (command is Func<string[], IEnumerator>)
                yield return ((Func<string[], IEnumerator>)command)(args);

        }
    }
}
