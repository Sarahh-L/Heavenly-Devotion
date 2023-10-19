using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


namespace UnityEditor.UI
{
    public class _MainMenu : MonoBehaviour
    {

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
        }

        public void Quit()
        {
            Application.Quit();
        }

        private string LoadScene;
    }
}
