using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System.IO;

namespace Commands
{
    public class DL_CommandData
    {
        public List<Command> commands;
        private const char commandSplitter_ID = ',';
        private const char argumentContainer_ID = '(';
        private const string waitCommand_ID = "[wait]";


        public struct Command
        {
            public string name;
            public string[] arguments;
            public bool waitForCompletion;
        }

        public DL_CommandData(string rawCommands)
        {
            commands = RipCommands(rawCommands);
        }

        private List<Command> RipCommands(string rawCommands)
        {
            string[] data = rawCommands.Split(commandSplitter_ID, System.StringSplitOptions.RemoveEmptyEntries);   // splits commands up if more than one in a line
            List<Command> result = new List<Command>();

            foreach (string cmd in data)
            {
                Command command = new Command();
                int index = cmd.IndexOf(argumentContainer_ID);
                command.name = cmd.Substring(0, index).Trim();

                if (command.name.ToLower().StartsWith(waitCommand_ID))
                {
                    command.name = command.name.Substring(waitCommand_ID.Length);
                    command.waitForCompletion = true;
                }

                else
                    command.waitForCompletion = false;

                command.arguments = GetArgs(cmd.Substring(index + 1, cmd.Length - index - 2));
                result.Add(command);
            }

            return result;
        }

        private string[] GetArgs(string args)
        {
            List<string> argList = new List<string>();
            StringBuilder currentArg = new StringBuilder();
            bool inQuotes = false;

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == '"')
                {
                    inQuotes = !inQuotes;
                    continue;
                }

                if (!inQuotes && args[i] == ' ')
                {
                    argList.Add(currentArg.ToString());
                    currentArg.Clear();
                    continue;
                }

                currentArg.Append(args[i]);
            }

            if (currentArg.Length > 0)
                argList.Add(currentArg.ToString());

            return argList.ToArray();
        }
    }
}