using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using stuff;

namespace Dialogue
{
    public class ConversationManager
    {
        private DialogueSystem dialogueSystem => DialogueSystem.instance;

        private Coroutine process = null;
        public bool isRunning => process != null;

    // constructor for conversation characterManager
        private TextArchitect architect = null;
    // keypress
        private bool userPrompt = false;


        public ConversationManager(TextArchitect architect)
        {
            this.architect = architect;
            dialogueSystem.onUserPrompt_Next += OnUserPrompt_Next;
        }

        private void OnUserPrompt_Next()
        {
            userPrompt = true;
        }

        #region Conversation runner
        public Coroutine StartConversation(List<string> conversation)
        {
            StopConversation();

            process = dialogueSystem.StartCoroutine(RunningConversation(conversation));

            return process;
        }


        public void StopConversation()
        {
            if (!isRunning)
                return;

            dialogueSystem.StopCoroutine(process);
            process = null;
        }

        IEnumerator RunningConversation(List<string> conversation)
        {
            for (int i = 0; i < conversation.Count; i++)
            {
                // don't show blank lines or try to run logic through them
                if (string.IsNullOrWhiteSpace(conversation[i]))
                    continue;
                
                Dialogue_Line line = DialogueParser.Parse(conversation[i]);

                // show dialogue
                if (line.hasDialogue)
                    yield return Line_RunDialogue(line);

                //if (line.hasCommands)
                    //yield return Line_RunCommands(line);
            }

        }
        #endregion

        #region Dialogue architect
        IEnumerator Line_RunDialogue(Dialogue_Line line)
        {
            // Show or hide speaker name if there is one present
            if (line.hasSpeaker)
                HandleSpeakerLogic(line.speakerData);
            // Build Dialogue
            yield return BuildLineSegments(line.dialogue);

            // wait for user input
            yield return WaitForUserInput();

        }
        #endregion

        #region Character spawning- Sprite / name handling
        private void HandleSpeakerLogic(DL_SpeakerData speakerData)
        {
            bool characterMustBeCreated = (speakerData.makeCharacterEnter || speakerData.isCastingPosition || speakerData.isCastingExpressions);

            Character character = CharacterManager.instance.GetCharacter(speakerData.name, createIfDoesNotExist: characterMustBeCreated);

            // Forces character to enter the screen
            if (speakerData.makeCharacterEnter && (!character.isVisible && !character.isRevealing))
                character.Show();
            // add character name to UI
            dialogueSystem.ShowSpeakerName(speakerData.displayName);

            // custmize dialogue for this character- if applicable
            DialogueSystem.instance.ApplySpeakerDataToDialogueContainer(speakerData.name);

            // Cast position
            if (speakerData.isCastingPosition)
                character.MoveToPostion(speakerData.castPosition);

            // Cast expression
            if (speakerData.isCastingExpressions)
            {
                foreach (var ce in speakerData.CastExpressions)
                    character.OnRecieveCastingExpression(ce.layer, ce.expression);
            }
        }
        #endregion

        #region Unused command system starter
        /*IEnumerator Line_RunCommands(Dialogue_Line line)
        {
            Debug.Log(line.commandData);
            yield return null;
        }*/
        #endregion

        #region Line building / appending
        IEnumerator BuildLineSegments(DL_DialogueData line)
        {
            for (int i = 0; i < line.segments.Count; i++)
            {
                DL_DialogueData.Dialogue_Segment segment = line.segments[i];

                yield return WaitForDialogueSegmentSignalToBeTriggered(segment);

                yield return BuildDialogue(segment.dialogue, segment.appendText);
            }
        }

        IEnumerator WaitForDialogueSegmentSignalToBeTriggered(DL_DialogueData.Dialogue_Segment segment)
        {
            switch(segment.startSignal)
            {
                case DL_DialogueData.Dialogue_Segment.StartSignal.A:
                    yield return WaitForUserInput();
                    break;
                case DL_DialogueData.Dialogue_Segment.StartSignal.WA:
                    yield return new WaitForSeconds(segment.signalDelay);
                    break;
            }
        }


        IEnumerator BuildDialogue(string dialogue, bool append = false)
        {
            // build dialogue
            if (!append)
                architect.Build(dialogue);

            else
                architect.Append(dialogue);

            // wait for dialogue to complete
            while(architect.isBuilding)
            {
                if (userPrompt)
                {
                    if(!architect.hurryUp)
                        architect.hurryUp = true;
                    else
                        architect.ForceComplete();

                    userPrompt = false;
                }
                yield return null;
            }
        }
        #endregion

        #region User input
        IEnumerator WaitForUserInput()
        {
            while(!userPrompt)
                yield return null;
                
            userPrompt = false;
        }
        #endregion
    }
}