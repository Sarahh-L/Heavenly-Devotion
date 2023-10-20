using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Testing;
using Unity.Collections.LowLevel.Unsafe;
using System.IO;

namespace Dialogue
{
    public class Dialogue_Line
    {
       public string speaker;
       public string dialogue;
       public DL_CommandData commandData;

       public bool hasDialogue => dialogue != string.Empty;

       public bool hasSpeaker => speaker != string.Empty;

       public bool hasCommands => commandData != null;

        // default constructor
        public Dialogue_Line(string speaker, string dialogue, string commands)
       {
            this.speaker = speaker;
            this.dialogue = dialogue;
            commandData = (string.IsNullOrWhiteSpace(commands) ? null : new DL_CommandData(commands));
       }
    }
}