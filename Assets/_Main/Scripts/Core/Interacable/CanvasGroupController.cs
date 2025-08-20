

using System.Collections;
using DIALOGUE;
using UnityEngine;

/// <summary>
/// Show or Hide CanvasGroups
/// Fading
/// Set Active or not
/// </summary>
public class CanvasGroupController
{
    private MonoBehaviour owner;
    private CanvasGroup rootCG;

    private static float DEFAULT_FADE_SPEED = 3f;
    
    public float alpha
    {
        get { return rootCG.alpha; }
        set { rootCG.alpha = value; }
    }
    
    private Coroutine co_showing = null;
    private Coroutine co_hiding = null;

    public bool isShowing => co_showing != null;
    public bool isHiding => co_hiding != null;

    public bool isFading => isShowing || isHiding;
    
    public bool isVisible => co_showing != null || rootCG.alpha > 0;

    /// <summary>
    /// Creates a new CanvasGroupController
    /// </summary>
    /// <param name="owner"></param>
    /// <param name="rootCG"></param>
    /// <param name="fadeSpeed"></param>
    public CanvasGroupController(MonoBehaviour owner, CanvasGroup rootCG, float fadeSpeed = 2f)
    {
        this.owner = owner;
        this.rootCG = rootCG;
        DEFAULT_FADE_SPEED = fadeSpeed;

    }

    public void FadeInAndOut(float time)
    {
        owner.StartCoroutine(FadingInOut(time));
    }
    
    public Coroutine Show()
    {
        if (isShowing)
        {
            return co_showing;
        }
        else if (isHiding)
        {
            owner.StopCoroutine(co_hiding);
            co_hiding = null;
        }

        co_showing = owner.StartCoroutine(Fading(1));
        return co_showing;
    }
    
    public Coroutine ShowToSet(float count)
    {
        if (isShowing)
        {
            return co_showing;
        }
        else if (isHiding)
        {
            owner.StopCoroutine(co_hiding);
            co_hiding = null;
        }

        co_showing = owner.StartCoroutine(Fading(count));
        return co_showing;
    }

    public Coroutine Hide()
    {
        if (isHiding)
        {
            return co_hiding;
        }
        else if (isShowing)
        {
            owner.StopCoroutine(co_showing);
            co_showing = null;
        }

        co_hiding = owner.StartCoroutine(Fading(0));
        return co_hiding;
    }

    private IEnumerator Fading(float alpha)
    {
        CanvasGroup cg = rootCG;

        while (cg.alpha != alpha)
        {
            cg.alpha = Mathf.MoveTowards(cg.alpha, alpha, Time.deltaTime * DEFAULT_FADE_SPEED);
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
    
    private IEnumerator FadingInOut(float time)
    {
        Show();
        PlayerMovement.lockMovement = true;
        yield return new WaitForSeconds(time);
        PlayerMovement.lockMovement = false;
        Hide();
    }
    
    
}
