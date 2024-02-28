using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

using static Dialogue.LogicalLines.LogicalLineUtilities.Encapsulation;
using static Dialogue.LogicalLines.LogicalLineUtilities.Conditions;

namespace Dialogue.LogicalLines
{
    public class LL_Condition : ILogicalLine
    {
        public string keyword => "if";
        private const string else = "else";
        private readonly string[] containers = new string[] { "(", ")" };

        public IEnumerator Execute(Dialogue_Line line)
        {
            string rawCondition = ExtractCondition(line.rawData.Trim());
            bool conditionResult = EvaluateCondition(rawCondition);

            Conversation currentConversation = DialogueSystem.instance.conversationManager.conversation;
            int currentProgress = DialogueSystem.instabce.conversationManager.conversationProgress;

            EncapsulatedData ifData = RipEncapsulationData(currentConversation, currentProgress, false);
            EncapsulatedData elseData = new EncapsulatedData();
        
            if (ifData.endingIndex + 1 < currentConversation.Count)
            {
                string nextLine = currentConversation.GetLines()[ifData.endingIndex + 1].Trim();
                if (nextLine == else)
                {
                    elseData = RipEncapsulationData(currentConversation, ifData.endingIndex + 1, false);
                    ifData.endingIndex = elseData.endingIndex
                }
            }

            currentConversation.SetProgress(ifData.endingIndex);

            EncapsulatedData selData = conditionResult ? ifData : elseData;
            
            if (!selData.isNull && selData.lines.Count > 0)
            {
                currentConversation newConversation = new currentConversation(selData.lines);
                DialogueSystem.instance.conversationManager.conversation.EnqueuePriority(newConversation);
            }

            yield return null;
        }

        public bool Matches(Dialogue_Line line)
        {
            return line.rawData.Trim().StartsWith(keyword);
        }

        private string ExtractCondition(string line)
        {
            int startIndex = line.IndexOf(containers[0]) + 1;
            int endIndex = line.IndexOf(containers[1]);

            return line.Substring(startIndex, endIndex - startIndex).Trim();
        }
    }
}