using UnityEngine;
using Characters;
using TMPro;
using UnityEngine.Rendering;

namespace Dialogue
{

    [CreateAssetMenu(fileName = "Dialogue System Configuration", menuName = "Dialogue System/Dialogue Configuration Asset")]
    public class DialogueSystemConfigSO : ScriptableObject
    {
        public CharacterConfigSO characterConfigurationAsset;

        public Color defaultTextColor = Color.white;    
        public TMP_FontAsset defaultFont;

        public float defaultNameFontSize = 22;
        public float defaultDialogueFontSize = 18;
        
    }
}