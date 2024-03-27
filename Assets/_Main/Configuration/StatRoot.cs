using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;
using TMPro;

namespace Stats
{
    public class StatRoot : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI statName;

        public void SetStatSO(StatSO _statSO)
        {
            //statName.text = _statSO.stats.statN;

            //IconAttribute icon = _statSO.StatIcon;
        }
    }
}
