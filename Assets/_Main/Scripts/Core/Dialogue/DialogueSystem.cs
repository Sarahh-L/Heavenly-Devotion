using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Dialogue
{
    public class DialogueSystem : MonoBehaviour
    {

        public DialogueContainer dialogueContainer = new DialogueContainer();
        private ConversationManager conversationManager = new ConversationManager();

        public static DialogueSystem instance;

        public bool isRunningConversation => conversationManager.isRunning;

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
                DestroyImmediate(gameObject);
        }

        public void Say(string speaker, string dialogue)
        {
            List<string> conversation = new List<string>() { $"{speaker} \"{dialogue}\""};
            Say(conversation);
        }

        public void Say(List<string> conversation)
        {
            conversationManager.StartConversation(conversation);
        }
    }
}
