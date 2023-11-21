using Dialogue;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace stuff
{
    public class CharacterManager : MonoBehaviour
    {
        public static CharacterManager instance { get; private set; }

        private Dictionary<string, Character> characters = new Dictionary<string, Character>();

        private CharacterConfigSO config => DialogueSystem.instance.config.characterConfigurationAsset;

        private const string CharacterCastingID = " as ";

        private const string CharacterNameID = "<charname>";
        private string characterRootPath => $"Characters/{CharacterNameID}";
        private string characterPrefabPath => $"{characterRootPath}/Character-[{CharacterNameID}]";

        [SerializeField] private RectTransform _characterpanel = null;
        public RectTransform characterPanel => _characterpanel;


        private void Awake()
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

        private CharacterInfo GetCharacterInfo(string name)
        {
            CharacterInfo result = new CharacterInfo();
            
            string[] nameData = name.Split(CharacterCastingID, System.StringSplitOptions.RemoveEmptyEntries);
            result.name = nameData[0];
            result.castingName = nameData.Length > 1 ? nameData[1] : result.name;

            result.config = config.GetConfig(result.castingName);

            result.prefab = GetPrefabForCharacter(result.castingName);
          
            return result;
        }

        private GameObject GetPrefabForCharacter(string characterName)
        {
            string prefabPath = FormatCharacterPath(characterPrefabPath, characterName);
            return Resources.Load<GameObject>(prefabPath);
        }

        private string FormatCharacterPath (string path, string characterName) => path.Replace(CharacterNameID, characterName);

        private Character CreateCharacterFromInfo(CharacterInfo info)
        {
            CharacterConfigData config = info.config;

            switch (config.characterType)
            {
                case Character.CharacterType.Text:
                    return new CharText(info.name, config);

                case Character.CharacterType.Sprite:
                case Character.CharacterType.SpriteSheet:
                    return new CharSprite(info.name, config, info.prefab);

                default:
                    return null;
            }
        }

        private class CharacterInfo
        {
            public string name = "";

            public string castingName = "";

            public CharacterConfigData config = null;

            public GameObject prefab = null;
        }
    }
}
