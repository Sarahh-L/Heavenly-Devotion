using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Characters;

namespace History
{
    [System.Serializable]
    public class CharacterData
    {
        public string characterName;
        public string diaplayName;
        public bool enabled;
        public Color color;
        public int priority;
        public bool isHighlighted;
        public bool isFacingleft;
        public Vector2 position;
        public CharacterConfigCache characterConfig;

        public string dataJSON;

        [System.Serializable]
        public class CharacterConfigCache
        {
            public string name;
            public string alias;

            public Character.CharacterType characterType;

            public Color nameColor;
            public Color dialogueColor;

            public string nameFont;
            public string dialogueFont;

            public float nameFontScale = 1f;
            public float dialogueFontScale = 1f;

            public CharacterConfigCache(CharacterConfigData reference)
            {
                name = reference.name;
                alias = reference.alias;

                characterType = reference.characterType;

                nameColor = reference.nameColor;
                dialogueColor = reference.dialogueColor;

                nameFont = FilePaths.resources_font + reference.nameFont.name;
                dialogueFont = FilePaths.resources_font + reference.dialogueFont.name;

                nameFontScale = reference.nameFontScale;
                dialogueFontScale = reference.dialogueFontScale;
            }
        }
    
        public static List<CharacterData> Capture()
        {
            List <CharacterData> characters = new List<CharacterData>();

            foreach (var character in CharacterManager.instance.allCharacters)
            {
                if (!character.isVisible) 
                    continue;

                CharacterData entry = new CharacterData();
                entry.characterName = character.name;
                entry.diaplayName = character.displayName;
                entry.enabled = character.isVisible;
                entry.color = character.color;
                entry.priority = character.priority;
                entry.isHighlighted = character.highlighted;
                entry.position = character.targetPosition;
                entry.characterConfig = new CharacterConfigCache(character.config);

                switch (character.config.characterType)
                {
                    case Character.CharacterType.Sprite:
                    case Character.CharacterType.SpriteSheet:
                        Spritedata sData = new Spritedata();
                        sData.layers = new List<Spritedata.LayerData>();

                        CharSprite sc = character as CharSprite;
                        foreach (var layer in sc.layers)
                        {
                            var layerData = new Spritedata.LayerData();
                            layerData.color = layer.renderer.color;
                            layerData.spriteName = layer.renderer.sprite.name;
                            sData.layers.Add(layerData);
                        }
                        entry.dataJSON = JsonUtility.ToJson(sData);
                        break;
                }
                characters.Add(entry);
            }
            return characters;
        }

        [System.Serializable]
        public class Spritedata
        {
            public List<LayerData> layers;

            [System.Serializable] 
            public class LayerData 
            {
                public string spriteName;
                public Color color;
            }
        }
    }
}