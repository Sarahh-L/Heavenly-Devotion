using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace stuff
{ 
    public class CharacterSpriteLayer
    {

    #region Variables
        private CharacterManager characterManager => CharacterManager.instance;

        private const float defaultTransitionSpeed = 3f;
        private float transitionSpeedMultiplier = 1;
        public int layer { get; private set; } = 0;
        public Image renderer { get; private set; } = null;

        public CanvasGroup rendererCG => renderer.GetComponent<CanvasGroup>();

        private List<CanvasGroup> oldRenderers = new List<CanvasGroup>();

        //private Coroutine co_transitioninglayer = null;
        //private Coroutine co_levelingAlpha = null;
        private Coroutine co_changingColor = null;

        //private bool isTransitioningLayer => co_transitioninglayer != null;
        //private bool islevelingAlpha => co_levelingAlpha != null;
        public bool isChangingColor => co_changingColor != null;
        #endregion

    #region Set sprite & render layer
        public CharacterSpriteLayer(Image defaultRenderer, int layer = 0) 
        {
            renderer = defaultRenderer;
            this.layer = layer;
        }

        public void SetSprite(Sprite sprite)
        {
            renderer.sprite = sprite;
        }
        #endregion

    #region Sprite Transitioning (spritesheet) ((unused))
        /*public Coroutine TransitionSprite(Sprite sprite, float speed = 1)
        {
            if (sprite = renderer.sprite)
                return null;

            if (isTransitioningLayer)
                characterManager.StopCoroutine(co_transitioninglayer);

            co_transitioninglayer = characterManager.StartCoroutine(TransitioningSprite(sprite, speed));

            return co_transitioninglayer;
        }

        public IEnumerator TransitioningSprite(Sprite sprite, float speedmultiplier) 
        {
            transitionSpeedMultiplier = speedmultiplier;

            Image newRenderer = CreateRenderer(renderer.transform.parent);
            newRenderer.sprite = sprite;

            yield return TryStartLevelingAlphas();

            co_transitioninglayer = null;
        }

        private Image CreateRenderer(Transform parent)
        {
            Image newRenderer = Object.Instantiate(renderer, parent);
            oldRenderers.Add(rendererCG);

            newRenderer.name = renderer.name;
            renderer = newRenderer;
            renderer.gameObject.SetActive(true);
            rendererCG.alpha = 0;

            return newRenderer;

        }

        private Coroutine TryStartLevelingAlphas()
        {
            if (islevelingAlpha)
                return co_levelingAlpha;

            co_levelingAlpha = characterManager.StartCoroutine(RunAlphaLeveling());

            return co_levelingAlpha;

        }

        private IEnumerator RunAlphaLeveling()
        {
            while (rendererCG.alpha < 1 || oldRenderers.Any(oldCG => oldCG.alpha > 0))
            {
                float speed = defaultTransitionSpeed * transitionSpeedMultiplier * Time.deltaTime;
                rendererCG.alpha = Mathf.MoveTowards(rendererCG.alpha, 1, speed);
            
                for (int i = oldRenderers.Count - 1; i >= 0; i--)
                {
                    CanvasGroup oldCG = oldRenderers[i];
                    oldCG.alpha = Mathf.MoveTowards(oldCG.alpha, 0, speed);

                    if (oldCG.alpha <= 0)
                    {
                        oldRenderers.RemoveAt(i);
                        Object.Destroy(oldCG.gameObject);
                    }
                }

                yield return null;
            }

            co_transitioninglayer = null;
        }*/
        #endregion

    #region Character color change
        public void SetColor(Color color)
        {
            renderer.color = color;

            foreach (CanvasGroup oldCG in oldRenderers)
            {
                oldCG.GetComponent<Image>().color = color;
            }
        }

        public Coroutine TransitionColor(Color color, float speed)
        {
            if (isChangingColor)
                characterManager.StopCoroutine(co_changingColor);

            co_changingColor = characterManager.StartCoroutine(ChangingColor(color, speed));

            return co_changingColor;
        }

        private IEnumerator ChangingColor(Color color, float speedMultiplier)
        {
            Color oldColor = renderer.color;
            List<Image> oldImages = new List<Image>();

            foreach(var oldCG in oldRenderers)
                oldImages.Add(oldCG.GetComponent<Image>());
            

            float colorPercent = 0;
            while(colorPercent < 1)
            {
                colorPercent += defaultTransitionSpeed * speedMultiplier * Time.deltaTime;

                renderer.color = Color.Lerp(oldColor, color, colorPercent);

                foreach(Image oldImage in oldImages)
                    oldImage.color = renderer.color;
                

                yield return null;
            }

            co_changingColor = null;
        }
        #endregion
    }
}
