using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace stuff
{
    public class CharText : Character
    {
        public CharText(string name) : base(name)
        {
            Debug.Log($"Created Text Character: '{name}'");
        }
    }
}