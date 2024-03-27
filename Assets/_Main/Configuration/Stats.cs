using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Stats
{
    public class Stats : MonoBehaviour
    {
        [SerializeField] private StatSO _statSO;

        public StatSO config => _statSO;

        [SerializeField] public CanvasGroup statBox;
        [SerializeField] public LayoutGroup root;
        [SerializeField] public TextMeshProUGUI statNameText;
        [SerializeField] public TextMeshProUGUI[] statList;

        public VariableStats[] stats;

        private void Start()
        { 

            UpdateStats();
        }

        public void UpdateStats()
        {
            Debug.Log("UpdateStats");
            int i = 0;
            foreach(VariableStats varStat in stats)
            {
                Transform statTransform = statList[i].transform;
                statTransform.gameObject.SetActive(true);
                statTransform.gameObject.GetComponent<Text>().text = $"{varStat.stat}: {varStat.var_int}";

                i++;
            }

        }
    }
}
