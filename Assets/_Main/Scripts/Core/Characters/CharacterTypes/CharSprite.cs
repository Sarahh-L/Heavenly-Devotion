using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

namespace stuff
{
    public class CharSprite : Character
    {
        private CanvasGroup rootCG => root.GetComponent<CanvasGroup>();
        public CharSprite(string name, CharacterConfigData config, GameObject prefab) : base(name, config, prefab)
        {
            rootCG.alpha = 0;
            Debug.Log($"Created Sprite Character: '{name}'");
        }

        public override IEnumerator ShowingOrHiding(bool show)
        {
            float targetAlpha = show ? 1f : 0;
            CanvasGroup self = rootCG;

            while (self.alpha != targetAlpha)
            {
                self.alpha = Mathf.MoveTowards(self.alpha, targetAlpha, 3f * Time.deltaTime);
                yield return null;
            }

            co_revealing = null;
            co_hiding = null;
        }
    }
}