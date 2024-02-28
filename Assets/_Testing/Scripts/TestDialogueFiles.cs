using Dialogue;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using System.IO;

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

            VNManager.instance.LoadFile(filePath);
              
        }
    }
}