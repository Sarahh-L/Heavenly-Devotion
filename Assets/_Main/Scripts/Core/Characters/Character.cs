using Dialogue;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using TMPro.EditorUtilities;
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

        private List<string> FormatDialogue(List<string> dialogue)
        {
            List<string> formattedDialogue = new List<string>();
            foreach (string line in dialogue)
            {
                formattedDialogue.Add($"{displayName} \"{line}\"");
            }
            return formattedDialogue;
        }


        // makes dialogue a list
        public Coroutine Say(string dialogue) => Say(new List<string> {dialogue});

        public Coroutine Say(List<string> dialogue)
        {
            dialogueSystem.ShowSpeakerName(displayName);
            dialogue = FormatDialogue(dialogue);
            UpdateTextCustomizationsOnScreen();
            return dialogueSystem.Say(dialogue);
        }

        public void SetNameColor(Color color) => config.nameColor = color;
        public void SetDialogueColor(Color color) => config.dialogueColor = color;
        public void SetNameFont(TMP_FontAsset font) => config.nameFont = font;
        public void SetDialogueFont(TMP_FontAsset font) => config.dialogueFont = font;

        public void UpdateTextCustomizationsOnScreen() => dialogueSystem.ApplySpeakerDataToDialogueContainer(config);

        // Char config data
        public enum CharacterType
        {
            Text,
            Sprite,
            SpriteSheet
        }
    }
}
