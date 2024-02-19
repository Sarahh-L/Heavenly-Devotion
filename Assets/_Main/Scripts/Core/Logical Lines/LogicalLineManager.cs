using Systems.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogue.LogicalLines
{
    public class LogicalLineManager
    {
        private DialogueSystem dialogueSystem => dialogueSystem.instance;
        private List<ILogicalLine> logicalLines = new List<ILogicalLine>();

        public bool TryGetLogic(Dialogue_Line line out Coroutine logic)
        {
            foreach (var logicalLine in logicalLines)
            {
                if (logicalLine.Matches(line))
                {
                    logic = dialogueSystem.StartCoroutine(logicalLine.Execute());
                    return true;
                }
            }

            logic = null;
            return false;
        }
    }
}