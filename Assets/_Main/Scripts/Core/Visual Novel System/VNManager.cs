using Dialogue;
using Stats;
using System.Collections;
using System.Collections.Generic;
using Testing;
using Unity.VisualScripting;
using UnityEngine;

namespace VisualNovel
{
    public class VNManager : MonoBehaviour
    {
        public static VNManager instance { get; private set; }
        [SerializeField] private VisualNovelSO config;
        public Camera mainCamera;
        [SerializeField] private TextAsset startingFile;
        [SerializeField] private GameObject StatManager;

        public StatSO playerStats => new StatSO();

        private void Awake()
        {
            instance = this;

            VNDatabaseLinkSetup linkSetup = GetComponent<VNDatabaseLinkSetup>();
            linkSetup.SetupExternalLinks();

            if (VNGameSave.activeFile == null) 
                VNGameSave.activeFile = new VNGameSave();

            
        }

        private void Start()
        {
            LoadGame();

            //StatManager.GetComponent<Stats.Stats>().UpdateStats();

            VNGameSave.SetStat();
        }

        private void LoadGame()
        {
            if (VNGameSave.activeFile.newGame)
            {
                List<string> lines = FileManager.ReadTextAsset(config.startingFile);
                Conversation start = new Conversation(lines);
                DialogueSystem.instance.Say(start);
            }
            else
            {
                VNGameSave.activeFile.Activate();
            }
        }
       
    }
}