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
            CharacterInfo result = new CharacterInfo();

            result.name = characterName;

            result.config = config.GetConfig(characterName);

            return result;
        }

        private Character CreateCharacterFromInfo(CharacterInfo info)
        {
            CharacterConfigData config = info.config;

            if (config.characterType == Character.CharacterType.Text)
                return new CharText(info.name);

            if (config.characterType == Character.CharacterType.Sprite || config.characterType == Character.CharacterType.SpriteSheet)
                return new CharSprite(info.name);

            return null;
        }

        private class CharacterInfo
        {
            public string name = "";

            public CharacterConfigData config = null;
        }
    }
}
