using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gdfsg{
    public class Character : MonoBehaviour
    {
        public GameObject characterNeutral;
        public GameObject characterMad;
        public GameObject characterHappy;
        public GameObject characterSad;
        public GameObject characterConfused;
        public GameObject characterMurder;

        public static Character instance;

        void Awake()
        {
            instance = this;
        }

        public void Update()
        {
            if (Input.GetKeyDown("d"))
            {
                ActiveCharacterHappy();
            }
            else if (Input.GetKeyDown("a"))
            {
                ActiveCharacterSad();
            }
        }

    // Neutral expression
        public void ActiveCharacterNeutral()
        {
            characterNeutral.SetActive(true);
            characterMad.SetActive(false);
            characterHappy.SetActive(false);
            characterSad.SetActive(false);
            characterConfused.SetActive(false);
            characterMurder.SetActive(false);
        }

    // Mad expression
        public void ActiveCharacterMad()
        {
            characterNeutral.SetActive(false);
            characterMad.SetActive(true);
            characterHappy.SetActive(false);
            characterSad.SetActive(false);
            characterConfused.SetActive(false);
            characterMurder.SetActive(false);
        }

    // Happy expression
        public void ActiveCharacterHappy()
        {
            characterNeutral.SetActive(false);
            characterMad.SetActive(false);
            characterHappy.SetActive(true);
            characterSad.SetActive(false);
            characterConfused.SetActive(false);
            characterMurder.SetActive(false);
        }

    // Sad expression
        public void ActiveCharacterSad()
        {
            characterNeutral.SetActive(false);
            characterMad.SetActive(false);
            characterHappy.SetActive(false);
            characterSad.SetActive(true);
            characterConfused.SetActive(false);
            characterMurder.SetActive(false);
        }

    // Confused expression
        public void ActiveCharacterConfused()
        {
            characterNeutral.SetActive(false);
            characterMad.SetActive(false);
            characterHappy.SetActive(false);
            characterSad.SetActive(false);
            characterConfused.SetActive(true);
            characterMurder.SetActive(false);
        }

    // Murderous intent
        public void ActiveCharacterMurder()
        {
            characterNeutral.SetActive(false);
            characterMad.SetActive(false);
            characterHappy.SetActive(false);
            characterSad.SetActive(false);
            characterConfused.SetActive(false);
            characterMurder.SetActive(true);
        }

    }
}
