using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace stuff
{
    public class CharSprite : Character
    {
        public CharSprite(string name) : base(name)
        {
            Debug.Log($"Created Sprite Character: '{name}'");
        }
    }
}