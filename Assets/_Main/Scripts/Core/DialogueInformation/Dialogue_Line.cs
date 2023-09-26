using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogue
{
    public class Dialogue_Line
    {
       public string speaker;
       public string dialogue;
       public string commands;

    // default constructor
       public Dialogue_Line(string speaker, string dialogue, string commands)
       {
            this.speaker = speaker;
            this.dialogue = dialogue;
            this.commands = commands;
       }
    }
}