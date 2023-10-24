using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine.UI;
using Testing;
using Dialogue;
using TMPro;


public class InputDecoder
{
    public static List<Characters> CharacterList = new List<Characters> ();
// efines and finds background image
    private static GameObject Background = GameObject.Find("Background");
    private static Image BackgroundImage = Background.GetComponent<Image> ();
    public static GameObject DialogueTextObject = GameObject.Find("DialogueText");
    public static GameObject NameTextObject = GameObject.Find("NameText");
    void Start()
    {
        Debug.Log("guh");
    }
    public static void ParseInputLine(string StringToParse)
    {
        string withOutTabs = StringToParse.Replace("\t", "");
        StringToParse = withOutTabs;
        if (StringToParse.StartsWith("\""))
        {
            Say(StringToParse);
        }
        string[] SeparatingString = { " ", "'", "\"", "(", ")" };
        string[] args = StringToParse.Split(SeparatingString, StringSplitOptions.RemoveEmptyEntries);
        foreach(Characters characters in CharacterList)
        {
            if (args[0] == characters.shortName)
                SplitToSay(StringToParse, characters);
        }
        if (args[0] == "show")
        {
            showImage(StringToParse);
        }
        else if (args[0] == "clrscr")
        {
            ClearScreen();
        }
    }
    public static void SplitToSay(string StringToParse, Characters characters)
    {
        int toQuote = StringToParse.IndexOf("\"") +1;
        int endQuote = StringToParse.Length - 1;
        string StringToOutput = StringToParse.Substring(toQuote, endQuote - toQuote);
        Say(characters.fullName, StringToOutput);
    }
    public static void Say(string what)
    {
        DialogueTextObject.GetComponent<TextMeshProUGUI>().text = what;
    }
    public static void Say(string who, string what)
    {
        DialogueTextObject.GetComponent<TextMeshProUGUI>().text = what;
        NameTextObject.GetComponent<TextMeshProUGUI>().text = who;
    }
// Background image changing stuff
    public static void showImage(string StringToParse)  // show - grabs background image and displays it
    {
        var ImageToUse = new Regex(@"show (?<ImageFileName>[^.]+)");
        var matches = ImageToUse.Match(StringToParse);
        string ImageToShow = matches.Groups["ImageFileName"].ToString();

        BackgroundImage.sprite = Resources.Load<Sprite>("Graphics/" + ImageToShow);
    }
    public static void ClearScreen()    //clrscr - Removes background image
    {
        BackgroundImage.sprite = null;
    }
}
