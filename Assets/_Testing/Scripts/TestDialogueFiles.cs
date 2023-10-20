using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialogue;

public class TestDialogueFiles : MonoBehaviour
{

    [SerializeField] private TextAsset fileToRead = null;

    // Start is called before the first frame update
    void Start() => StartConversation();

        
    void StartConversation()
    {
        List<string> lines = FileManager.ReadTextAsset(fileToRead);

        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                Debug.Log("Empty line or null");
                continue;
            }
            Debug.Log(line);

            Dialogue_Line dl = DialogueParser.Parse(line);

            if (dl.hasCommands)
            {
                Debug.Log($"number of lines: {dl.commandData.commands.Count}");

                for (int i = 0; i < dl.commandData.commands.Count; i++)
                {
                    Debug.Log("dsfsduighg");
                    DL_CommandData.Command command = dl.commandData.commands[i];
                    Debug.Log($"Command [{i}] '{command.name}' has arguments [{string.Join(", ", command.arguments)}]");
                }
            }

         
        }

        //DialogueSystem.instance.Say(lines);
    }
}
