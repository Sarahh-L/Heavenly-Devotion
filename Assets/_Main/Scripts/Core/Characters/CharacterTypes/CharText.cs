using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace stuff
{
    public class CharText : Character
    {
        public CharText(string name, CharacterConfigData config) : base(name, config)
        {
            Debug.Log($"Created Text Character: '{name}'");
        }
    }
}