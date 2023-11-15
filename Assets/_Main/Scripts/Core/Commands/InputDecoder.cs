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
using stuff;
using UnityEngine.TextCore.Text;


public class InputDecoder
{
    #region Parsing
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
        /*foreach (Characters characters in CharacterList)
        {
            if (args[0] == characters.shortName)
                SplitToSay(StringToParse, characters);
        }*/
        #endregion

        #region Commands
        if (args[0] == "show")
            showImage(StringToParse);

        if (args[0] == "clrscr")
            ClearScreen();

        //if (args[0] == "Character")
        //CreateNewCharacter(StringToParse);

        if (args[0] == "screen")
            ScreenClear(StringToParse);

        if (args[0] == "jump")
            jumpTo(StringToParse);

        //if (args[0] == "emotion")
        //Emotion(StringToParse);

    }
    #endregion

    #region Dialogue creation/spawning
   public static void SplitToSay(string StringToParse, Characters characters)
    {
        int toQuote = StringToParse.IndexOf("\"") + 1;
        int endQuote = StringToParse.Length - 1;
        string StringToOutput = StringToParse.Substring(toQuote, endQuote - toQuote);
        Say(characters.fullName, StringToOutput);
    }

    //public static GameObject InterfaceElements = GameObject.Find("UI_Elements");
    //public static GameObject DialogueTextObject = GameObject.Find("DialogueText");
    //public static GameObject NameTextObject = GameObject.Find("NameText");
    public static bool PausedHere = false;

    public static void Say(string what)
    {
        //if (!InterfaceElements.activeInHierarchy) InterfaceElements.SetActive(true);
        //DialogueTextObject.GetComponent<TextMeshProUGUI>().text = what;
        PausedHere = true;
    }
    public static void Say(string who, string what)
    {
        //if (!InterfaceElements.activeInHierarchy) InterfaceElements.SetActive(true);
        //DialogueTextObject.GetComponent<TextMeshProUGUI>().text = what;
        //NameTextObject.GetComponent<TextMeshProUGUI>().text = who;
        PausedHere = true;
    }
    #endregion

    #region Background creation/spawning
    // defines and finds background image
    private static GameObject Background = GameObject.Find("Background");
    private static Image BackgroundImage = Background.GetComponent<Image>();

    // objects created for transition effect
    private static GameObject canvas = GameObject.Find("Transition");
    private static GameObject ImageInst = Resources.Load("Prefabs/Background") as GameObject;

    public static void showImage(string StringToParse)  // show - grabs background image and displays it
    {
        string ImageToShow = null;
        bool FadeEffect = false;

        var ImageToUse = new Regex(@"show (?<ImageFileName>[^.]+)");
        var ImageToUseTransition = new Regex(@"show (?<ImageFileName>[^.]+) with (?<TransitionName>[^.]+)");

        var matches = ImageToUse.Match(StringToParse);
        var altMatches = ImageToUseTransition.Match(StringToParse);

        if (altMatches.Success)
        {
            ImageToShow = altMatches.Groups["ImageFileName"].ToString();
            FadeEffect = true;
        }
        else if (matches.Success)
        {
            ImageToShow = matches.Groups["ImageFileName"].ToString();
        }


        GameObject PictureInstance = GameObject.Instantiate(ImageInst);
        PictureInstance.transform.SetParent(canvas.transform, false);
        PictureInstance.GetComponent<ImageInstance>().FadeIn = FadeEffect;
        PictureInstance.GetComponent<Image>().color = Color.white;
        PictureInstance.GetComponent<Image>().sprite = Resources.Load<Sprite>("Graphics/" + ImageToShow);
    }

    #endregion


    #region Character creation stuffs

    /*public static List<Characters> CharacterList = new List<Characters>();
    public static void CreateNewCharacter(string StringToParse)
    {
        string newCharacterShortName = null;
        string newCharacterFullName = null;
        Color newCharacterColor = Color.white;
        string newCharacterSideImage = null;

        var characterExpression = new Regex(@"Character\((?<shortName>[a-zA-Z0-9_]+), (?<fullName>[a-zA-Z0-9_]+), color=(?<characterColor>[a-zA-Z0-9_]+), image=(?<image>[a-zA-Z0-9_]+)\)"); // Setup for character creator calling
        var characterExpressionA = new Regex(@"Character\((?<shortName>[a-zA-Z0-9_]+), (?<fullName>[a-zA-Z0-9_]+), color=(?<characterColor>[a-zA-Z0-9_]+)\)");   // char with no image
        var characterExpressionB = new Regex(@"Character\((?<shortName>[a-zA-Z0-9_]+), (?<fullName>[a-zA-Z0-9_]+)\)");                                           // char with no image or color
        var characterExpressionC = new Regex(@"Character\((?<shortName>[a-zA-Z0-9_]+), (?<fullName>[a-zA-Z0-9_]+), image=(?<image>[a-zA-Z0-9_]+)\)");            // char with no color
    
        if (characterExpression.IsMatch(StringToParse))
        {
            var matches = characterExpression.Match(StringToParse);
            newCharacterShortName = matches.Groups["shortName"].ToString();
            newCharacterFullName = matches.Groups["fullName"].ToString();
            newCharacterColor = Color.clear; ColorUtility.TryParseHtmlString(matches.Groups["characterColor"].ToString(), out newCharacterColor);
            newCharacterSideImage = matches.Groups["image"].ToString();
        }

        else if (characterExpressionA.IsMatch(StringToParse))
        {
            var matches = characterExpression.Match(StringToParse);
            newCharacterShortName = matches.Groups["shortName"].ToString();
            newCharacterFullName = matches.Groups["fullName"].ToString();
            newCharacterColor = Color.clear; ColorUtility.TryParseHtmlString(matches.Groups["characterColor"].ToString(), out newCharacterColor);
        }

        else if (characterExpressionB.IsMatch(StringToParse))
        {
            var matches = characterExpression.Match(StringToParse);
            newCharacterShortName = matches.Groups["shortName"].ToString();
            newCharacterFullName = matches.Groups["fullName"].ToString();
        }

        else if (characterExpressionC.IsMatch(StringToParse))
        {
            var matches = characterExpression.Match(StringToParse);
            newCharacterShortName = matches.Groups["shortName"].ToString();
            newCharacterFullName = matches.Groups["fullName"].ToString();
            newCharacterSideImage = matches.Groups["image"].ToString();
        }

        CharacterList.Add(new Characters(newCharacterShortName, newCharacterFullName, newCharacterColor, newCharacterSideImage));
    }*/
    #endregion


    // here's the deal with this
    // you created a function that allows the user to see the emotion being called (like shortname)
    // you just need to call the emotions from the emotion array
    // you don't know how so ask around maybe

    #region Emotion
    /*public static List<stuff.Character> emotionList = new List<stuff.Character>();
    public static void Emotion(string StringToParse)
    {
        string[] emotionCommand = { " ", " " };
        string[] args = StringToParse.Split(emotionCommand, StringSplitOptions.RemoveEmptyEntries);

        foreach (stuff.Character emotion in emotionList)
        {
            if (args[1] == emotion.emotionName)
                SplitToSay(StringToParse, stuff.Character.emotion);
        }

    }*/

    #endregion

    #region Clear screen function

    public static void ClearScreen()    //clrscr - Removes background image
    {
        foreach (Transform t in canvas.transform)
            MonoBehaviour.Destroy(t.gameObject);

        //InterfaceElements.SetActive(false);
    }

    public static void ScreenClear(string StringToParse)
    {

        string ImageToShow = null;
        bool FadeEffect = false;

        var ImageToUse = new Regex(@"screen (?<ImageFileName>[^.]+)");
        var ImageToUseTransition = new Regex(@"screen (?<ImageFileName>[^.]+) with (?<TransitionName>[^.]+)");

        var matches = ImageToUse.Match(StringToParse);
        var altMatches = ImageToUseTransition.Match(StringToParse);

        if (altMatches.Success)
        {
            ImageToShow = altMatches.Groups["ImageFileName"].ToString();
            FadeEffect = true;
        }
        else if (matches.Success)
        {
            ImageToShow = matches.Groups["ImageFileName"].ToString();
        }


        GameObject PictureInstance = GameObject.Instantiate(ImageInst);
        PictureInstance.transform.SetParent(canvas.transform, false);
        PictureInstance.GetComponent<ImageInstance>().FadeIn = FadeEffect;
        PictureInstance.GetComponent<Image>().color = Color.white;
        PictureInstance.GetComponent<Image>().sprite = Resources.Load<Sprite>("Graphics/" + ImageToShow);

        foreach (Transform t in canvas.transform)
        {
            if (t != PictureInstance.transform)
                MonoBehaviour.Destroy(t.gameObject, 3f);

            //InterfaceElements.SetActive(false);
        }
    }
    #endregion

    #region Labels/Jumping

    [NonSerialized]

    public string inputLine;
    public static int CommandLine = 0;
    public static string lastCommand = "";

    public static List<Label> labels = new List<Label>();

    public static void jumpTo(string StringToParse)
    {
        var tempStringSplit = StringToParse.Split(' ');
        foreach (Label l in labels)
        {
            if (l.LabelName == tempStringSplit[1])
            {
                CommandLine = l.LabelIndex;
                //PausedHere = false;
            }
        }
    }

    #endregion

    #region LoadingScript

    public static List<string> Commands = new List<string>();
    public static void readScript(UnityEngine.TextAsset asset)
    {
        UnityEngine.TextAsset commandFile = Resources.Load(asset.text) as UnityEngine.TextAsset;
        //var commandArray = commandFile.text.Split('\n');
       // foreach (var line in commandArray)
           // Commands.Add(line);

        /*for (int x = 0; x < Commands.Count; x++)
        {
            if (Commands[x].StartsWith("label"))
            {
                var labelSplit = Commands[x].Split(' ');
                labels.Add(new Label(labelSplit[1], x));
                Debug.Log("Label created" + x);
            }
        }*/
    }

    /*public static List<string> ReadTextAsset(TextAsset asset, bool includeBlankLines = true)
    {
        List<string> lines = new List<string>();
        using (StringReader sr = new StringReader(asset.text))
        {
            // if line available, read next line
            while (sr.Peek() > -1)
            {
                string line = sr.ReadLine();
                if (includeBlankLines || !string.IsNullOrWhiteSpace(line))
                {
                    lines.Add(line);
                }
            }
        }

        return lines;
    }*/

    #endregion
}