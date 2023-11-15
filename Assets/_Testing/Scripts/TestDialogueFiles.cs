using Dialogue;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Testing
{
    public class TestDialogueFiles : MonoBehaviour
    { 

        [SerializeField] private TextAsset fileToRead = null;
        void Start()
        {
            StartConversation();
            InputDecoder.readScript(fileToRead);
        }

        void Update()
        {
             // Hides active hierarchy
             /*if (Input.GetKeyDown("h"))
             {
                 if (InputDecoder.InterfaceElements.activeInHierarchy)
                     InputDecoder.InterfaceElements.SetActive(false);

                 else
                     InputDecoder.InterfaceElements.SetActive(true);
             }*/
              
             //if (InputDecoder.Commands[InputDecoder.CommandLine] != InputDecoder.lastCommand)
             //{
                 //InputDecoder.lastCommand = InputDecoder.Commands[InputDecoder.CommandLine];
                // InputDecoder.PausedHere = false;
                // InputDecoder.ParseInputLine(InputDecoder.Commands[InputDecoder.CommandLine]);
             //}

             if (!InputDecoder.PausedHere && InputDecoder.CommandLine < InputDecoder.Commands.Count - 1)
                 InputDecoder.CommandLine++;


             //if (InputDecoder.PausedHere && Input.GetKeyDown(KeyCode.Space) && InputDecoder.CommandLine < InputDecoder.Commands.Count - 1)
                 //InputDecoder.CommandLine++;
         
        }

        void StartConversation()
        {
            List<string> lines = FileManager.ReadTextAsset(fileToRead);

            /*for (int i = 0; i < lines.Count; i++)
            {
                string line = lines[i];

                if (string.IsNullOrWhiteSpace(line)) 
                    continue;
                
                Dialogue_Line dl = DialogueParser.Parse(line);

                Debug.Log($"{dl.speaker.name} as [{(dl.speaker.castName != string.Empty ? dl.speaker.castName : dl.speaker.name)}]at {dl.speaker.castPosition}");

                List<(int l, string ex)> expr = dl.speaker.CastExpressions;
                for (int c = 0; c < expr.Count; c++)
                {
                    Debug.Log($"[layer[{expr[c].l}] = '{expr[c].ex}']");
                }
            
            }*/

            DialogueSystem.instance.Say(lines);
        }
    }
}