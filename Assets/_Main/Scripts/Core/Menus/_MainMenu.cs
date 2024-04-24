using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor.UI;
using VisualNovel;

public class _MainMenu : MonoBehaviour
{
    public const string main_menu_scene = "Main Menu";

    public static _MainMenu instance { get; private set; }

    public AudioClip menuMusic;
    public CanvasGroup mainPanel;
    private CanvasGroupController mainCG;

    [SerializeField] UIConfirmationMenu uiChoiceMenu => UIConfirmationMenu.instance;

    void Start()
    {
        mainCG = new CanvasGroupController(this, mainPanel, null);

        AudioManager.instance.StopAllSoundEffects();
        AudioManager.instance.StopAllTracks();
        AudioManager.instance.PlayTrack(menuMusic, channel: 0, startingVolume: 1);
    }

    void Awake()
    {
        instance = this;
    }

    public void Click_StartNewGame()
    {
        uiChoiceMenu.Show(
            // Title
            "Start a new game?", 
            // Yes
            new UIConfirmationMenu.ConfirmationButton("Yes", StartNewGame), 
            // No
            new UIConfirmationMenu.ConfirmationButton("No", null));
    } 

    private void StartNewGame()
    {
        VNGameSave.activeFile = new VNGameSave();
        StartCoroutine(StartingGame());
    }

    public void LoadGame(VNGameSave file)
    {
        VNGameSave.activeFile = file;
        StartCoroutine(StartingGame());
    }

    private IEnumerator StartingGame()
    {
        mainCG.Hide(speed: 0.3f);
        AudioManager.instance.StopTrack(0);

        while (mainCG.isVisible)
            yield return null;

        VN_Configuration.activeConfig.Save();
        UnityEngine.SceneManagement.SceneManager.LoadScene("HeavenlyDevotion");
    }
}
