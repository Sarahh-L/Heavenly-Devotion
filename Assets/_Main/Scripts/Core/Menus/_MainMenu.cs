using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class _MainMenu : MonoBehaviour
{
    public void Play()
    {
        LoadScene = "HeavenlyDevotion";
        SceneManager.LoadScene(LoadScene);
    }

    public void Quit()
    {
        Application.Quit();
    }

    private string LoadScene;
}
