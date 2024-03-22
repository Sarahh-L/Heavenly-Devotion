using Dialogue;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CanvasGroupController
{
    private const float default_fade_speed = 3f;

    private MonoBehaviour owner;
    private CanvasGroup rootCG;

    private Coroutine co_showing = null;
    private Coroutine co_hiding = null;

    public bool isShowing => co_showing != null;
    public bool isHiding => co_hiding != null;
    public bool isFading => isShowing || isHiding;

    public bool isVisible => co_showing != null || rootCG.alpha > 0;
    public float alpha { get { return rootCG.alpha; } set { rootCG.alpha = value; } }

    public CanvasGroupController(MonoBehaviour owner, CanvasGroup rootCG)
    {
        this.owner = owner;
        this.rootCG = rootCG;
    }

    public Coroutine Show(float speed = 1f, bool immediate = false)
    {
        if (isShowing)
            return co_showing;

        else if (isHiding)
        {
            owner.StopCoroutine(co_hiding);
            co_hiding = null;
        }

        co_showing = owner.StartCoroutine(Fading(1, speed, immediate));

        return co_showing;
    }

    public Coroutine Hide(float speed = 1f, bool immediate = false)
    {
        if (isHiding)
            return co_hiding;

        else if (isShowing)
        {
            owner.StopCoroutine(co_showing);
            co_showing = null;
        }

        co_hiding = owner.StartCoroutine(Fading(0, speed, immediate));

        return co_hiding;
    }

    private IEnumerator Fading(float alpha, float speed, bool immediate)
    {
        CanvasGroup cg = rootCG;

        if (immediate)
            cg.alpha = alpha;

        while (cg.alpha != alpha)
        {
            cg.alpha = Mathf.MoveTowards(cg.alpha, alpha, Time.deltaTime * default_fade_speed * speed);
            yield return null;
        }

        co_showing = null;
        co_hiding = null;
    }

    public void SetInteractableState(bool active)
    {
        rootCG.interactable = active;
        rootCG.blocksRaycasts = active;
    }
}
