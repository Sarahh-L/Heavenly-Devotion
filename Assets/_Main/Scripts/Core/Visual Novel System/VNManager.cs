using Dialogue;
using System.Collections;
using System.Collections.Generic;
using Testing;
using TMPro;
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

        [SerializeField] private GameObject StatBox;
        [SerializeField] private TextMeshProUGUI Stat1;
        [SerializeField] private TextMeshProUGUI Stat2;
        [SerializeField] private TextMeshProUGUI Stat3;
        [SerializeField] private TextMeshProUGUI Stat4;

        [SerializeField] private int CurrentCharisma;


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

        }

        private void Update()
        {
            //DisplayStats();
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
            LoadStats();
            DisplayStats();
        }

        private void DisplayStats()
        {
            // Need to load stats from database to local variables and update the display

            Stat1.text = $"Charisma: {VNGameSave.activeFile.charVal}"; 
            
        }

        public void LoadStats()
        {
            CurrentCharisma = VNGameSave.activeFile.charVal;
        }
       
    }
}