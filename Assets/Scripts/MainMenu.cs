using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;
    public string LevelToLoad = "Level1";
    
   public void Play()
   {
        LoadLevel();
   }

   public void Quit()
   {
        Debug.Log("Exciting...");
        Application.Quit();
   }

   public void LoadLevel()
   {
        StartCoroutine(Load(SceneManager.GetActiveScene().buildIndex + 1));
   }

   IEnumerator Load(int levelIndex)
   {
        //play
        transition.SetTrigger("Start");
        //wait
        yield return new WaitForSeconds(2);
        //load
        SceneManager.LoadScene(levelIndex);
   }
}
