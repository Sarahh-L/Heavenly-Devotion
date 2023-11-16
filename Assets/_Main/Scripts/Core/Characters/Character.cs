using Dialogue;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace stuff 
{
    public abstract class Character
    {
        public string name = "";
        public string displayName = "";
        public RectTransform root = null;
        public CharacterConfigData config;

        public DialogueSystem dialogueSystem => DialogueSystem.instance;


        // constructor
        public Character(string name, CharacterConfigData config)
        {
            this.name = name;
            displayName = name;
            this.config = config;
        }

        // makes dialogue a list
        public Coroutine Say(string dialogue) => Say(new List<string> {dialogue});

        public Coroutine Say(List<string> dialogue)
        {
            dialogueSystem.ShowSpeakerName(displayName);
            dialogueSystem.ApplySpeakerDataToDialogueContainer(config);
            return dialogueSystem.Say(dialogue);
        }

        // Char config data
        public enum CharacterType
        {
            Text,
            Sprite,
            SpriteSheet
        }
    }
}
