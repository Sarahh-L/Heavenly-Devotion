using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gdfsg{
public class test : MonoBehaviour
{
    Character Luke;
    public GameObject characterNeutral;
    public GameObject characterMad;
    public GameObject characterHappy;
    public GameObject characterSad;
    public GameObject characterConfused;
    public GameObject characterMurder;

    void Awake()
    {
        Luke = Character.instance;
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

    public void ActiveCharacterHappy()
    {
        Debug.Log("uyhsudsj");
        characterNeutral.SetActive(false);
        characterMad.SetActive(false);
        characterHappy.SetActive(true);
        characterSad.SetActive(false);
        characterConfused.SetActive(false);
        characterMurder.SetActive(false);
    }

    public void ActiveCharacterSad()
    {
        characterNeutral.SetActive(false);
        characterMad.SetActive(false);
        characterHappy.SetActive(false);
        characterSad.SetActive(true);
        characterConfused.SetActive(false);
        characterMurder.SetActive(false);
    }
}
}