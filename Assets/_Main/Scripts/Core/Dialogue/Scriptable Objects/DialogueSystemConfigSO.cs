using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Characters;
using TMPro;

namespace Dialogue
{

    [CreateAssetMenu(fileName = "Dialogue System Configuration", menuName = "Dialogue System/Dialogue Configuration Asset")]
    public class DialogueSystemConfigSO : ScriptableObject
    {
        public CharacterConfigSO characterConfigurationAsset;

        public Color defaultTextColor = Color.white;    
        public TMP_FontAsset defaultFont;
    }
}