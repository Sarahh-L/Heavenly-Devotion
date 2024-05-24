using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace History 
{
    [System.Serializable]
    public class HistoryState
    {
        public string SceneName = "HeavenlyDevotion";
        public DialogueData dialogue;
        public List<CharacterData> characters;
        public List<AudioTrackData> audio;
        //public List<AudioSFXData> sfx;
        public List<GraphicData> graphics;

        public static HistoryState Capture()
        {
            HistoryState state = new HistoryState();
            state.SceneName = SceneManager.GetActiveScene().name;
            state.dialogue = DialogueData.Capture();
            state.characters = CharacterData.Capture();
            state.audio = AudioTrackData.Capture();
            //state.sfx = AudioSFXData.Capture();
            state.graphics = GraphicData.Capture();

            return state;
        }
        public void Load()
        {
            Debug.Log($"Load:  Active Scene: {SceneManager.GetActiveScene().name} Loading Scene: {SceneName}");
            if (SceneManager.GetActiveScene().name != SceneName){
                SceneManager.LoadScene(SceneName);
            }
            DialogueData.Apply(dialogue);
            CharacterData.Apply(characters);
            AudioTrackData.Apply(audio);
            GraphicData.Apply(graphics);
        }
   
    }
}
