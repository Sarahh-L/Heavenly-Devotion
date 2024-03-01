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
        private const string ELSE = "else";
        private readonly string[] containers = new string[] { "(", ")" };

        public IEnumerator Execute(Dialogue_Line line)
        {
            string rawCondition = ExtractCondition(line.rawData.Trim());
            bool conditionResult = EvaluateConditions(rawCondition);
            

            Conversation currentConversation = DialogueSystem.instance.conversationManager.conversation;
            int currentProgress = DialogueSystem.instance.conversationManager.conversationProgress;

            EncapsulatedData ifData = RipEncapsulationData(currentConversation, currentProgress, false, parentStartingIndex: currentConversation.fileStartIndex);
            EncapsulatedData elseData = new EncapsulatedData();
        
            if (ifData.endingIndex + 1 < currentConversation.Count)
            {
                string nextLine = currentConversation.GetLines()[ifData.endingIndex + 1].Trim();
                if (nextLine == ELSE)
                {
                    elseData = RipEncapsulationData(currentConversation, ifData.endingIndex + 1, false, parentStartingIndex: currentConversation.fileStartIndex);
                    ifData.endingIndex = elseData.endingIndex;
                }
            }

            currentConversation.SetProgress(elseData.isNull ? ifData.endingIndex : elseData.endingIndex);

            EncapsulatedData selData = conditionResult ? ifData : elseData;
            
            if (!selData.isNull && selData.lines.Count > 0)
            {
                // Remove the header and encapsulator lines from the conversation indexes
                selData.startingIndex += 2; // remove header and starting encapsulator {
                selData.endingIndex -= 1;   // remove ending encapsulator }

                Conversation newConversation = new Conversation(selData.lines, file: currentConversation.file, fileStartIndex: selData.startingIndex, fileEndIndex: selData.endingIndex);
                DialogueSystem.instance.conversationManager.EnqueuePriority(newConversation);
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