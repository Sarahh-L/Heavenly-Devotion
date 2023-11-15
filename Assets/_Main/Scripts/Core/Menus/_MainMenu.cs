using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace UnityEditor.UI
{
    public class _MainMenu : MonoBehaviour
    {
        public Animator transition;

        public float transitionTime = 1f;

        void OnMouseOver()
        {
            Debug.Log ("hee hoo peanut");
        }

        // Update is called once per frame
        void OnMouseExit()
        {
            Debug.Log ("nuh uh");
        }

        public void Play()
        {
            LoadScene = "HeavenlyDevotion";
            SceneManager.LoadScene(LoadScene);
            OnMouseOver();
            LoadNextLevel();
        }

        public void LoadNextLevel()
        {
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
        }

        IEnumerator LoadLevel(int levelIndex)
        {
            transition.SetTrigger("Start");

            yield return new WaitForSeconds(transitionTime);

            SceneManager.LoadScene(levelIndex);
        }

        public void Quit()
        {
            Application.Quit();
        }

        private string LoadScene;
    }
}
