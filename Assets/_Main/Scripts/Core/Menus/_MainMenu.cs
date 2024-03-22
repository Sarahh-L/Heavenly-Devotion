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

    void Start()
    {
        mainCG = new CanvasGroupController(this, mainPanel);
        AudioManager.instance.PlayTrack(menuMusic, channel: 0, startingVolume: 1);
    }

    void Awake()
    {
        instance = this;
    }
    public void StartNewGame()
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

        UnityEngine.SceneManagement.SceneManager.LoadScene("HeavenlyDevotion");
    }
}
