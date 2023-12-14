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

            Character Alexandria = CreateCharacter("Alexandria");
            CharSprite Teevee = CreateCharacter("Teevee") as CharSprite;
            Character Loki = CreateCharacter("Loki");
            Character Scylla = CreateCharacter("Scylla");
            Character Shein = CreateCharacter("Shein");
            Character Mako = CreateCharacter("Mako");

            Alexandria.SetPosition(Vector2.zero);

            Sprite TeeveeSprite = Teevee.GetSprite("upset");

            Teevee.SetSprite(TeeveeSprite, 0);

            yield return Teevee.Show();
           
            yield return Teevee.MoveToPostiion(Vector2.one, smooth: true);
            yield return Teevee.MoveToPostiion(Vector2.zero, smooth: true);

            Teevee.SetNameColor(Color.black);
            Teevee.SetDialogueColor(Color.blue);
            Teevee.SetDialogueFont(tempFont);

            yield return Alexandria.Say("testing testing");
            yield return Teevee.Say("did it work?");
            yield return Alexandria.Say("i think so");

            yield return null;


            List<string> Lines = FileManager.ReadTextAsset(fileToRead);
            Teevee.Say(Lines);
        }

        // Update is called once per frame
        void Update()
        {

       }
    }
}