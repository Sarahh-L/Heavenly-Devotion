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

            CharSprite Alexandria = CreateCharacter("Alexandria") as CharSprite;
            CharSprite Teevee = CreateCharacter("Teevee") as CharSprite;
            //Character Loki = CreateCharacter("Loki");
            //Character Scylla = CreateCharacter("Scylla");
            //Character Shein = CreateCharacter("Shein");
            //Character Mako = CreateCharacter("Mako");

            Alexandria.SetPosition(Vector2.zero);
            Teevee.SetPosition(new Vector2(0.5f, 0.5f));

            Sprite AlexandriaUpsetSprite = Alexandria.GetSprite("Alexandria - Upset");
            Sprite AlexandriaNeutralSprite = Alexandria.GetSprite("Alexandria - Neutral");
            Sprite AlexandriaAngrySprite = Alexandria.GetSprite("Alexandria - Angry");
            Sprite AlexandriaHappySprite = Alexandria.GetSprite("Alexandria - Happy");
            Sprite TeeveeNeutralSprite = Teevee.GetSprite("Teevee - Upset");

            yield return new WaitForSeconds(1);
            Alexandria.SetSprite(AlexandriaUpsetSprite, 0);
            yield return Alexandria.TransitionColor(Color.red, speed: 0.3f);
            yield return Alexandria.TransitionColor(Color.blue);
            yield return Alexandria.TransitionColor(Color.yellow);
            yield return Alexandria.TransitionColor(Color.white);

            yield return Alexandria.MoveToPostiion(Vector2.one, smooth: true);
            yield return Alexandria.MoveToPostiion(Vector2.zero, smooth: true);

            Alexandria.SetSprite(AlexandriaNeutralSprite, 0);
            yield return new WaitForSeconds(2);
            Alexandria.SetSprite(AlexandriaHappySprite, 0);
            yield return new WaitForSeconds(2);
            Alexandria.SetSprite(AlexandriaAngrySprite, 0);
            Teevee.SetSprite(TeeveeNeutralSprite, 0);

            yield return Alexandria.Say("testing testing");
            yield return Alexandria.Say("did it work?");
            yield return Alexandria.Say("i think so");

            yield return null;


            List<string> Lines = FileManager.ReadTextAsset(fileToRead);
            Alexandria.Say(Lines);
        }

        // Update is called once per frame
        void Update()
        {

       }
    }
}