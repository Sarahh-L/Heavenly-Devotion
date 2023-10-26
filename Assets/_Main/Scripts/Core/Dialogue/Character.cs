using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace stuff { 
    public class Character : MonoBehaviour
    {
        public static Character instance;

        //public Expressions[] expression;

        //public int emotionState;

        private GameObject Neutral;
        private GameObject Mad;
        private GameObject Happy;
        private GameObject Sad;
        private GameObject Confused;
        private GameObject Murder;

        public GameObject[] Luke;
        public GameObject[] Hazel;
        public GameObject[] Sunny;
        public GameObject[] Tempest;
        public GameObject[] Rot;
        public GameObject[] Life;

        //public string emotionName;
        //internal static Characters emotion;

        void Awake()
        {
            instance = this;
            ActiveCharacterNeutral();
        }

        /*public Character (string emotionInput)
        {
            this.emotionName = emotionInput;
        }*/
        #region keycodes
        public void Update()
        {
            if (Input.GetKeyDown("d"))
            {
                ActiveCharacterNeutral();
            }
            if (Input.GetKeyDown("a"))
            {
                ActiveCharacterMad();
            }
            if (Input.GetKeyDown("s"))
            {
                ActiveCharacterConfused();
            }
            if (Input.GetKeyDown("e"))
            {
                ActiveCharacterHappy();
            }
            if (Input.GetKeyDown("w"))
            {
                ActiveCharacterSad();
            }
            if (Input.GetKeyDown("q"))
            {
                ActiveCharacterMurder();
            }
        }
        #endregion

        #region Luke
        // Neutral expression
        public void ActiveCharacterNeutral()
        {
            Luke[0].SetActive(true);
            Luke[1].SetActive(false);
            Luke[2].SetActive(false);
            Luke[3].SetActive(false);
            Luke[4].SetActive(false);
            Luke[5].SetActive(false);
        }

    // Mad expression
        public void ActiveCharacterMad()
        {
            Luke[0].SetActive(false);
            Luke[1].SetActive(true);
            Luke[2].SetActive(false);
            Luke[3].SetActive(false);
            Luke[4].SetActive(false);
            Luke[5].SetActive(false);
        }

    // Happy expression
        public void ActiveCharacterHappy()
        {
            Luke[0].SetActive(false);
            Luke[1].SetActive(false);
            Luke[2].SetActive(true);
            Luke[3].SetActive(false);
            Luke[4].SetActive(false);
            Luke[5].SetActive(false);
        }

    // Sad expression
        public void ActiveCharacterSad()
        {
            Luke[0].SetActive(false);
            Luke[1].SetActive(false);
            Luke[2].SetActive(false);
            Luke[3].SetActive(true);
            Luke[4].SetActive(false);
            Luke[5].SetActive(false);
        }

    // Confused expression
        public void ActiveCharacterConfused()
        {
            Luke[0].SetActive(false);
            Luke[1].SetActive(false);
            Luke[2].SetActive(false);
            Luke[3].SetActive(false);
            Luke[4].SetActive(true);
            Luke[5].SetActive(false);
        }

    // Murderous intent
        public void ActiveCharacterMurder()
        {
            Luke[0].SetActive(false);
            Luke[1].SetActive(false);
            Luke[2].SetActive(false);
            Luke[3].SetActive(false);
            Luke[4].SetActive(false);
            Luke[5].SetActive(true);
        }
        #endregion

    }
}
