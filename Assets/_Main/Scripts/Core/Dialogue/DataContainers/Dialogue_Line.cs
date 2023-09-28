using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Testing;

namespace Dialogue
{
    public class Dialogue_Line
    {
       public string speaker;
       public string dialogue;
       public string commands;

       public bool hasDialogue => dialogue != string.Empty;

       public bool hasCommands => commands != string.Empty;

       public bool hasSpeaker => speaker != string.Empty;

    // default constructor
       public Dialogue_Line(string speaker, string dialogue, string commands)
       {
            this.speaker = speaker;
            this.dialogue = dialogue;
            this.commands = commands;
       }
    }
}