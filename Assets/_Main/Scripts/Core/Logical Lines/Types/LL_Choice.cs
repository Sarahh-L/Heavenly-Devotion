using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogue.LogicalLines
{
    public class LL_Choice : ILogicalLine
    {
        public string keyword => "choice";
        private const char encapsulation_start = "{";
        private const char encapsulation_end = "}";
        private const char choice_identifier = "-";

        public IEnumerator Execute(Dialogue_Line line)
        {
           RawChoiceData data = RipChoiceData();
           List<Choice> choices = GetChoicesFromData(data);
        
            string title = line.dialogueData.rawData;
            ChoicePanel panel = ChoicePanel.instance;
            string[] choiceTitle = choices.Select (c => c.title).ToArray();

            panel.Show(title, choiceTitles);

            while (panel.isWaitingOnUserChoice)
                yield return null;

            Choice selectedChoice = choices[panel.lastDecision.answerIndex];

            Conversation newConversation = new Conversation(selectedChoice.resultLines);
            DialogueSystem.instance.conversationManager.conversation.SetProgress(data.endingIndex);
            DialogueSystem.instance.conversationManager.EnqueuePriority(newConversation);
        
        }

        public bool Matches(Dialogue_Line line)
        {
        }

        private RawChoiceData RipChoiceData()
        {
            Conversation currentConversation = DialogueSystem.instance.conversationManager.conversation;
            int currentProgress = DialogueSystem.instance.conversationManager.conversationProgress;
            int encapsulateDepth = 0;
            RawChoiceData data = new RawChoiceData { lines = new List<string>(), endingIndex = 0};
            for (int i = currentProgress; i < currentConversation.Count; i++)
            {
                string line = currentConversation.GetLines()[i];
                data.lines.Add(line);

                if (IsEncapsulationStart(line))
                {
                    encapsulateDepth++;
                    continue;
                }

                if (IsEncapsulationEnd(line))
                {
                    encapsulateDepth--;
                    if (encapsulateDepth == 0)
                    {
                        data.endingIndex = i;
                        break;
                    }
                }
            }
        }

        private List<Choice> GetChoicesFromData(RawChoiceData data)
        {
            List<Choice> choices = new List<Choice>();
            int encapsulateDepth = 0;
            bool isFirstChoice = true;

            Choice choice = new Choice
            {
                title = string.Empty,
                resultLines = new List<string>(),
            };

            foreach (var line in data.lines.Skip(1))
            {
                if (IsChoiceStart(line) && encapsulateDepth == 1)
                {
                    if (!isFirstChoice)
                    {
                        choices.Add(choice);
                        choice = new Choice
                        {
                            title = string.Empty,
                            resultLines = new List<string>(),
                        };
                    }

                    choice.title = line.Trim().Substring(1);
                    isFirstChoice = false;
                    continue;
                }

                AddLineToResults(line, ref choice, ref encapsulateDepth);
            }

            if (!choices.Contains(choice))
                choices.Add(choice);

            return choices;
        }

        private void AddLineToResults(string line, ref Choice choice, ref int encapsulateDepth)
        {
            line.Trim();

            if (IsEncapsulationStart(line))
            {
                if (encapsulateDepth > 0)
                    choice.resultLines.Add(line);

                encapsulateDepth++;
                return;
            }

            if (IsEncapsulationEnd(line))
            {
                encapsulateDepth--;

                if (encapsulateDepth > 0)
                    choice.resultLines.Add(line);

                return;
            }

            choice.resultLines.Add(line);
        }

        private bool IsEncapsulationStart(string line) => line.Trim().StartsWith(encapsulation_start);
        private bool IsEncapsulationEnd(string line) => line.Trim().StartsWith(encapsulation_end);

        private bool IsChoiceStart(string line) => line.Trim().StartsWith(choice_identifier);

        private struct RawChoiceData
        {
            public List<string> lines;
            public int endingIndex;
        }

        private struct Choice
        {
            public string title;
            public List<string> resultLines;
        }
    }
}