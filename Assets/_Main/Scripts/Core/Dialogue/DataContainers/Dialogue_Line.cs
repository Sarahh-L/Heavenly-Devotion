using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

namespace Dialogue
{
    public class Dialogue_Line
    {
        public DL_SpeakerData speaker;
        public DL_DialogueData dialogue;
        //public DL_CommandData commandData;

        public bool hasDialogue => dialogue.hasDialogue; // dialogue != string.Empty;

        public bool hasSpeaker => speaker != null;

       //public bool hasCommands => commandData != null;

        // default constructor
        public Dialogue_Line(string speaker, string dialogue)
        {
            this.speaker = (string.IsNullOrWhiteSpace(speaker) ? null : new DL_SpeakerData(speaker));
            this.dialogue = new DL_DialogueData(dialogue);
            //commandData = (string.IsNullOrWhiteSpace(commands) ? null : new DL_CommandData(commands));
        }
    }
}