using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* --------------------------------------------------------
 * Unity UI Motion - Fade Animation Component
 * Created by Hollow1
 * 
 * Applies a Fade animation to a UI panel
 * 
 * Version: 2.1.0
 * GitHub: https://github.com/Hollow1/Unity-UI-Motion
 * -------------------------------------------------------- */

[AddComponentMenu("")]
internal class Fade
{
    private readonly CanvasGroup canvasGroup;
    private readonly MonoBehaviour monoBehaviour;

    private const float transparent = 0f;
    private const float visible = 1.0f;
    private const float fadeDuration = 0.5f;

    public Fade(CanvasGroup cg, MonoBehaviour runner)
    {
        canvasGroup = cg;
        monoBehaviour = runner;
    }

    // ----------------------------------------------------- PUBLIC API -----------------------------------------------------

    public void TurnInvisible()
    {
        if (canvasGroup == null) { return; }

        canvasGroup.alpha = transparent;
    }

    public void TurnVisible()
    {
        if (canvasGroup == null) { return; }

        canvasGroup.alpha = visible;
    }

    public void FadeIn(float delay = 0f, float duration = fadeDuration)
    {
        monoBehaviour.StartCoroutine(FadeUiIn(delay, duration));
    }

    public void FadeOut(float delay = 0f, float duration = fadeDuration)
    {
        monoBehaviour.StartCoroutine(FadeUiOut(delay, duration));
    }

    // ----------------------------------------------------- FADE IN ANIMATION -----------------------------------------------------

    private IEnumerator FadeUiIn(float delay, float duration)
    {
        if (canvasGroup == null) { yield break; }

        float elapsedTime = 0f;
        canvasGroup.alpha = transparent;
        if (delay > 0) { yield return new WaitForSeconds(delay); }

        while (elapsedTime < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(transparent, visible, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = visible;
    }

    // ----------------------------------------------------- FADE OUT ANIMATION -----------------------------------------------------

    private IEnumerator FadeUiOut(float delay, float duration)
    {
        if (canvasGroup == null) { yield break; }

        float elapsedTime = 0f;
        canvasGroup.alpha = visible;
        if (delay > 0) { yield return new WaitForSeconds(delay); }

        while (elapsedTime < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(visible, transparent, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = transparent;
    }


}