using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

using static Dialogue.LogicalLines.LogicalLineUtilities.Encapsulation;
using Unity.VisualScripting.Dependencies.Sqlite;

/*namespace Dialogue.LogicalLines
{
    public class LL_Dorms : ILogicalLine
    {
        public string keyword => "dorm";
        private const char dorm_identifier = '+';


        public IEnumerator Execute(Dialogue_Line line)
        {
            var currentConversation = DialogueSystem.instance.conversationManager.conversation;
            var progress = DialogueSystem.instance.conversationManager.conversationProgress;
            EncapsulatedData data = RipEncapsulationData(currentConversation, progress, ripHeaderandEncapsulators: true, parentStartingIndex: currentConversation.fileStartIndex);
            List<Dorm> dorms = GetDormsFromData(data);

            string title = line.dialogue.rawData;
            ChoicePanel panel = ChoicePanel.instance;
            string[] dormTitles = dorms.Select(d => d.title).ToArray();


            while (panel.isWaitingOnUserChoice)
                yield return null;

            Dorm selectedDorm = dorms[panel.lastDecision.answerIndex];

            Conversation dormConversation = new Conversation(selectedDorm.resultLines, file: currentConversation.file, fileStartIndex: selectedDorm.startIndex, fileEndIndex: selectedDorm.endIndex);
            DialogueSystem.instance.conversationManager.conversation.SetProgress(data.endingIndex - currentConversation.fileStartIndex);
            DialogueSystem.instance.conversationManager.EnqueuePriority(dormConversation);

        }

        public bool Matches(Dialogue_Line line)
        {
            return (line.hasSpeaker && line.speakerData.name.ToLower() == keyword);
        }

        private List<Dorm> GetDormsFromData(EncapsulatedData data)
        {
            List<Dorm> dorms = new List<Dorm>();
            int encapsulateDepth = 0;
            bool isFirstChoice = true;

            Dorm dorm = new Dorm
            {
                title = string.Empty,
                resultLines = new List<string>(),
            };

            int choiceIndex = 0, i = 0;

            //foreach (var line in data.lines.Skip(1))
            for (i = 1; i < data.lines.Count; i++)
            {
                var line = data.lines[i];
                if (IsDormStart(line) && encapsulateDepth == 1)
                {
                    if (!isFirstChoice)
                    {
                        dorm.startIndex = data.startingIndex + (choiceIndex + 1);
                        dorm.endIndex = data.startingIndex + (i - 1);
                        dorms.Add(dorm);
                        dorm = new Dorm
                        {
                            title = string.Empty,
                            resultLines = new List<string>(),
                        };
                    }

                    choiceIndex = i;
                    dorm.title = line.Trim().Substring(1);
                    isFirstChoice = false;
                    continue;
                }

                AddLineToResults(line, ref dorm, ref encapsulateDepth);
            }
        }

        private bool IsDormStart(string line) => line.Trim().StartsWith(dorm_identifier);


        private void AddLineToResults(string line, ref Dorm dorm, ref int encapsulateDepth)
        {
            line.Trim();

            if (IsEncapsulationStart(line))
            {
                if (encapsulateDepth > 0)
                    dorm.resultLines.Add(line);

                encapsulateDepth++;
                return;
            }

            if (IsEncapsulationEnd(line))
            {
                encapsulateDepth--;

                if (encapsulateDepth > 0)
                    dorm.resultLines.Add(line);

                return;
            }

            dorm.resultLines.Add(line);
        }

            if (!dorms.Contains(dorm))
            {
                dorm.startIndex = data.startingIndex + (choiceIndex + 1);
                dorm.endIndex = data.startingIndex + (i - 2);
                dorms.Add(dorm);
            }

            return dorms;



        public struct Dorm
        {
            public string title;
            public List<string> resultLines;
            public int startIndex;
            public int endIndex;
        }

    }
}
*/