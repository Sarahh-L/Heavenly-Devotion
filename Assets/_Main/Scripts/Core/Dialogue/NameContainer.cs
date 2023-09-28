using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


// The box that holds the name on screen. part of dialogue container


public class NameContainer : MonoBehaviour
{
    [SerializeField] private GameObject root;
    [SerializeField] private TextMeshProUGUI nameText;


    public void Show(string nameToShow = "")
    {
        root.SetActive(true);

        if (nameToShow != string.Empty)
            nameText.text = nameToShow;
    }

    public void Hide()
    {
        root.SetActive(false);
    }
}
