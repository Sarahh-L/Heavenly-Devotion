using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using stuff;
using Dialogue;
using TMPro;

namespace Testing
{
    public class TestCharacters : MonoBehaviour
    {

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
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}