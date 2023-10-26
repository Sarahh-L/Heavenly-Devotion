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
        void Start()
        {
            InputDecoder.readScript("Scripts/script");
        }

        void Update()
        {
            // Hides active hierarchy
            if (Input.GetKeyDown("h"))
            {
                if (InputDecoder.InterfaceElements.activeInHierarchy)
                    InputDecoder.InterfaceElements.SetActive(false);
                
                else
                    InputDecoder.InterfaceElements.SetActive(true);
            }

            if (InputDecoder.Commands[InputDecoder.CommandLine] != InputDecoder.lastCommand)
            {
                InputDecoder.lastCommand = InputDecoder.Commands[InputDecoder.CommandLine];
                InputDecoder.PausedHere = false;
                InputDecoder.ParseInputLine(InputDecoder.Commands[InputDecoder.CommandLine]);
            }

            if (!InputDecoder.PausedHere && InputDecoder.CommandLine < InputDecoder.Commands.Count - 1)
                InputDecoder.CommandLine++;


            if (InputDecoder.PausedHere && Input.GetKeyDown(KeyCode.Space) && InputDecoder.CommandLine < InputDecoder.Commands.Count - 1)
                InputDecoder.CommandLine++;
        }
    }
}