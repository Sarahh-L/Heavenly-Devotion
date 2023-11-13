using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using stuff;

public class TestCharacters : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Character Luke = CharacterManager.instance.CreateCharacter("Luke");
        Character Narrator = CharacterManager.instance.CreateCharacter("Narrator");
        Character Bruh = CharacterManager.instance.CreateCharacter("Bruh");
        Character Bruh1 = CharacterManager.instance.CreateCharacter("Bruh");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
