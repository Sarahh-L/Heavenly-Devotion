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
        //[SerializeField] private TextAsset fileToRead = null;
        public TMP_FontAsset tempFont;

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(Test());
            //Character Alexandria = CharacterManager.instance.CreateCharacter("Alexandria");
            //Character Teevee = CharacterManager.instance.CreateCharacter("Teevee");
        }

        IEnumerator Test()
        {
            yield return new WaitForSeconds(1f);

            Character Alexandria = CharacterManager.instance.CreateCharacter("Alexandria");

            yield return new WaitForSeconds(1f);

            yield return Alexandria.Hide();

            yield return new WaitForSeconds(0.5f);

            yield return Alexandria.Show();

            yield return Alexandria.Say("hello");
            //Character Teevee = CharacterManager.instance.CreateCharacter("Teevee");

           /* List<string> lines = new List<string>()
            {
                "Hi there!",
                "guguifgjh",
                "this will work"
            };

            yield return Alexandria.Say(lines);

            Alexandria.SetNameColor(Color.red);
            Alexandria.SetDialogueColor(Color.blue);
            Alexandria.SetDialogueFont(tempFont);

            yield return Alexandria.Say(lines);

            Alexandria.ResetConfigurationData();

            yield return Alexandria.Say(lines);*/

            //lines = new List<string>()
            //{
              //  "I am teevee",
             //   "guh"
            //}

           // yield return Teevee.Say(lines);

            //List<string> Lines = FileManager.ReadTextAsset(fileToRead);
            //DialogueSystem.instance.Say(Lines);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }}