using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Characters;

public class InputPanelTesting : MonoBehaviour
{
    public InputPanel inputPanel;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Running());
    }

    IEnumerator Running()
    {
        Character Alexandria = CharacterManager.instance.CreateCharacter("Alexandria", revealAfterCreation: true);

        yield return Alexandria.Say("Hi, whats your name");

        inputPanel.Show("What is your name?");

        while (inputPanel.isWaitingOnUserInput)
            yield return null;

        string characterName = inputPanel.lastInput;

        yield return Alexandria.Say($"swaggy, {characterName}");
    }
}
