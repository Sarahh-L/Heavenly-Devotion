using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Testing
{
    public class TestDialogueFiles : MonoBehaviour
    {
        public string inputLine;

        void Start()
        {
            InputDecoder.CharacterList.Add(new Characters("L", "Luke", Color.white, "Luke.jpg"));

            inputLine = "L \"this is some text\"";
            InputDecoder.ParseInputLine(inputLine);

            inputLine = "show background";
            InputDecoder.ParseInputLine(inputLine);

        }

        void Update()
        {

        }

        /*[SerializeField] private TextAsset fileToRead = null;
        // Start is called before the first frame update
        void Start() => StartConversation();
        void StartConversation()
        {
            List<string> lines = FileManager.ReadTextAsset(fileToRead);
            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                Dialogue_Line dl = DialogueParser.Parse(line);

                /*for (int i = 0; i < dl.commandData.commands.Count; i++)
                {
                    Debug.Log("dsfsduighg");
                    DL_CommandData.Command command = dl.commandData.commands[i];
                    Debug.Log($"Command [{i}] '{command.name}' has arguments [{string.Join(", ", command.arguments)}]");
                }*/
            //}
            //DialogueSystem.instance.Say(lines);
        //}
    }
}