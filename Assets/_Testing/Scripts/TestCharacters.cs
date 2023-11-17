using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using stuff;
using TMPro;
using Dialogue;

namespace Testing
{
    public class TestCharacters : MonoBehaviour
    {
        [SerializeField] private TextAsset fileToRead = null;
        public TMP_FontAsset tempFont;

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(Test());
        }

        IEnumerator Test()
        {
            Character Luke = CharacterManager.instance.CreateCharacter("Luke");
            Character Sunny = CharacterManager.instance.CreateCharacter("Sunny");

            List<string> lines = new List<string>()
            {
                "Hi there!",
                "guguifgjh",
                "this will work"
            };

            yield return Luke.Say(lines);

            Luke.SetNameColor(Color.red);
            Luke.SetDialogueColor(Color.blue);
            Luke.SetDialogueFont(tempFont);

            yield return Luke.Say(lines);

            lines = new List<string>()
            {
                "I am sunny",
                "guh"
            };

            yield return Sunny.Say(lines);

            List<string> Lines = FileManager.ReadTextAsset(fileToRead);
            DialogueSystem.instance.Say(Lines);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}