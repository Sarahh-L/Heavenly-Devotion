using System.Collections;
using System.Collections.Generic;
using Testing;
using UnityEngine;

namespace Stats
{
    /*public enum StatNames
    {
        Charisma, Swagginess, Rhythm, Dexterity
    }*/

    [CreateAssetMenu(fileName = "Stat Configuration", menuName = "Dialogue System/Stat Configuration Asset")]

    public class StatSO : ScriptableObject
    {
        [SerializeField] public VariableStats[] stats;

        public StatSO()
        {
            stats = new VariableStats[4];
            stats[0] = new VariableStats("Charisma", 0);
            stats[1] = new VariableStats("Swagginess", 0);
            stats[2] = new VariableStats("Rhythm", 0);
            stats[3] = new VariableStats("Dexterity", 0);
        }
    }
}