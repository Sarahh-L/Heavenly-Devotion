using Dialogue;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace stuff
{
    public class CharacterManager : MonoBehaviour
    {
        public static CharacterManager instance { get; private set; }

        private Dictionary<string, Character> characters = new Dictionary<string, Character>();

        private CharacterConfigSO config => DialogueSystem.instance.config.characterConfigurationAsset;
        public void Awake()
        {
            instance = this;
        }

        public CharacterConfigData GetCharacterConfig(string characterName)
        {
            return config.GetConfig(characterName);
        }

        public Character GetCharacter(string characterName, bool createIfDoesNotExist = false)
        {
            if (characters.ContainsKey(characterName.ToLower()))
                return characters[characterName.ToLower()];

            else if (createIfDoesNotExist) 
                return CreateCharacter(characterName);

            return null;
        }

        public Character CreateCharacter(string characterName)
        {
            if (characters.ContainsKey(characterName.ToLower()))
            {
                Debug.LogWarning($"A character called '{characterName}' already exists, Could not create character.");
                return null;
            }

            CharacterInfo info = GetCharacterInfo(characterName);

            Character character = CreateCharacterFromInfo(info);

            characters.Add(characterName.ToLower(), character);

            return character;

        }

        private CharacterInfo GetCharacterInfo(string characterName)
        {
            CharacterInfo result = new CharacterInfo
            {
                name = characterName,

                config = config.GetConfig(characterName)
            };

            return result;
        }

        private Character CreateCharacterFromInfo(CharacterInfo info)
        {
            CharacterConfigData config = info.config;

            switch (info.config.characterType)
            {
                case Character.CharacterType.Text:
                    return new CharText(info.name, config);

                case Character.CharacterType.Sprite:
                case Character.CharacterType.SpriteSheet:
                    return new CharSprite(info.name, config);

                default:
                    return null;
            }
        }

        private class CharacterInfo
        {
            public string name = "";

            public CharacterConfigData config = null;
        }
    }
}
