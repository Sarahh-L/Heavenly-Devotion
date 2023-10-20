using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections.LowLevel.Unsafe;

namespace Dialogue
{
    public class Dialogue_Line
    {
       public string speaker;
       public string dialogue;
       public DL_CommandData commandData;

       public bool hasDialogue => dialogue != string.Empty;

       public bool hasCommands => commandData != null;

       public bool hasSpeaker => speaker != string.Empty;

    // default constructor
       public Dialogue_Line(string speaker, string dialogue, string commands)
       {
            this.speaker = speaker;
            this.dialogue = dialogue;
            this.commandData = (string.IsNullOrWhiteSpace(commands) ? null : new DL_CommandData(commands));
       }
    }
}