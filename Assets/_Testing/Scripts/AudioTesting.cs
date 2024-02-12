using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Characters;

namespace Testing
{
    public class AudioTesting : MonoBehaviour
    {
        void Start()
        {
            StartCoroutine(Running());
        }

        Character CreateCharacter(string name) => CharacterManager.instance.CreateCharacter(name);

        IEnumerator Running()
        {
            CharSprite Alexandria = CreateCharacter("Alexandria") as CharSprite;
            Character Me = CreateCharacter("Me");
            Alexandria.Show();

            yield return new WaitForSeconds(0.5f);

            AudioManager.instance.PlaySoundEffect("Audio/SFX/sparkle",loop: true);
            yield return Me.Say("bro shut up");
            yield return new WaitForSeconds(1f);
            Alexandria.Animate("Hop");
            AudioManager.instance.StopSoundEffect("sparkle");
        }
    }
}
