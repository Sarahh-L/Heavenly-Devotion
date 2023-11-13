using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace stuff 
{
    public abstract class Character
    {
        public string name = "";
        public RectTransform root = null;

        // constructor
        public Character(string name)
        {
            this.name = name;
        }

        // Char config data
        public enum CharacterType
        {
            Text,
            Sprite,
            SpriteSheet
        }


       /* public static Character instance;

        //public Expressions[] expression;

        //public int emotionState;

        public GameObject[] characters;


        //public string emotionName;
        //internal static Characters emotion;

        /*public Character (string emotionInput)
        {
            this.emotionName = emotionInput;
        }
        public void Start()
        {
            Debug.Log("your mom");
            ActiveCharacter();
        }

     public void ActiveCharacter()
        {
            Debug.Log(":)");
            if (Input.GetKeyDown("p"))
            {
                characters[0].SetActive(true);      // Luke
                Debug.Log("Luke");
            }
            if (Input.GetKeyDown("o"))
            {
                characters[1].SetActive(true);      // Sunny
                Debug.Log("Sunny");
            }
            if (Input.GetKeyDown("i"))
            {
                characters[2].SetActive(true);      // Hazel
                Debug.Log("hazel");
            }
            if (Input.GetKeyDown("4"))
            {
                characters[3].SetActive(true);
            }
            if (Input.GetKeyDown("5"))
            {
                characters[4].SetActive(true);
            }
            if (Input.GetKeyDown("6"))
            {
                characters[5].SetActive(true);
            }
        }
       */
    }
}
