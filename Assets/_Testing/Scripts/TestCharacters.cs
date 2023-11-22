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

        private Character CreateCharacter(string name) => CharacterManager.instance.CreateCharacter(name);

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(Test());
            //Character Alexandria = CharacterManager.instance.CreateCharacter("Alexandria");
            //Character Teevee = CharacterManager.instance.CreateCharacter("Teevee");
        }

        IEnumerator Test()
        {

            //Character Alexandria = CharacterManager.instance.CreateCharacter("Alexandria");
            //Character Teevee = CharacterManager.instance.CreateCharacter("Teevee");

            Character Alexandria = CreateCharacter("guh as Alexandria");
            Character Alexandria1 = CreateCharacter("fgusdg as Alexandria");

            Alexandria1.Hide();
            yield return new WaitForSeconds(1);
            Alexandria.Show();
            yield return new WaitForSeconds(2);
            Alexandria.Hide();
            Alexandria1.Show();
            Alexandria.Show();
            Alexandria.SetPosition(Vector2.one);

            //Teevee.SetPosition(new Vector2(0.5f, 0.5f));

            //yield return null;
            //Character Teevee = CharacterManager.instance.CreateCharacter("Teevee");

           /*List<string> lines = new List<string>()
           {
                "Hi there!",
                "guguifgjh",
                "this will work"
           };

            yield return Alexandria.Say(lines);*/

            //yield return Alexandria.Say(lines);
          

            Alexandria1.SetNameColor(Color.black);
            Alexandria1.SetDialogueColor(Color.blue);
            Alexandria1.SetDialogueFont(tempFont);
           // Teevee.SetDialogueColor(Color.yellow);

            //yield return Alexandria1.Say(lines);
         //   yield return Teevee.Say(lines);

         //   Alexandria.ResetConfigurationData();

          //  yield return Alexandria.Say(lines);

            //lines = new List<string>()
            //{
              //  "I am teevee",
             //   "guh"
            //}

           // yield return Teevee.Say(lines);

            List<string> Lines = FileManager.ReadTextAsset(fileToRead);
            Alexandria.Say(Lines);
        }

        // Update is called once per frame
        void Update()
        {

       }
    }
}