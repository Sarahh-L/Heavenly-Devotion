using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialogue;

public class TestDialogueFiles : MonoBehaviour
{
    // Start is called before the first frame update
        void Start()
        {
            StartConversation();
        }

        
        void StartConversation()
        {
            List<string> lines = FileManager.ReadTextAsset("L_S");

            DialogueSystem.instance.Say(lines);
        }
}