using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Commands
{
    public class CommandTesting : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(Running());
        }

        IEnumerator Running()
        {
            yield return CommandManager.instance.Execute("print");
            yield return CommandManager.instance.Execute("print_1p", "Hello world");
            yield return CommandManager.instance.Execute("print_mp", "Line 1", "Line 2", "Line 3");

            yield return CommandManager.instance.Execute("lambda");
            yield return CommandManager.instance.Execute("lambda_1p", "hello lambda");
            yield return CommandManager.instance.Execute("lambda_mp", "bruh", "uh", "yea");

            yield return CommandManager.instance.Execute("process");
            yield return CommandManager.instance.Execute("process_1p", "3");
            yield return CommandManager.instance.Execute("process_mp", "Process Line 1", "Process Line 2", "Process Line 3");
        }
    }
}
