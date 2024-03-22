using Dialogue;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using System.IO;
using VisualNovel;

namespace Testing
{
    public class TestDialogueFiles : MonoBehaviour
    { 

        [SerializeField] private TextAsset fileToRead = null;
        void Start()
        {
            StartConversation();
            //InputDecoder.readScript(fileToRead);
        }

        void Update()
        {
      
        }

        void StartConversation()
        {
            string fullPath = AssetDatabase.GetAssetPath(fileToRead);

            int resourcesIndex = fullPath.IndexOf("Resources/");
            string relativePath = fullPath.Substring(resourcesIndex + 10);

            string filePath = Path.ChangeExtension(relativePath, null);

            LoadFile(filePath);
              
        }

        public void LoadFile(string filePath)
        {
            List<string> lines = new List<string>();
            TextAsset file = Resources.Load<TextAsset>(filePath);

            try
            {
                lines = FileManager.ReadTextAsset(file);
            }
            catch
            {
                Debug.LogError($"Dialogue file at path 'Resources/{filePath} does not exist!");
                return;
            }

            DialogueSystem.instance.Say(lines, filePath);
        }
    }
}