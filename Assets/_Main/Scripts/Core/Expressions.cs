using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]

public class Expressions
{
    public string characterName;

    public Emotions[] emotions;

    [System.Serializable]
    public class Emotions
    {
        public GameObject Neutral;
        public GameObject Mad;
        public GameObject Happy;
        public GameObject Sad;
        public GameObject Confused;
        public GameObject Murder;
    }
}
