using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/* --------------------------------------------------------
 * Unity UI Motion - Fade Animation Component
 * Created by Hollow1
 * 
 * Applies a Fade animation to a UI panel
 * 
 * Version: 2.2.1
 * GitHub: https://github.com/Hollow1/Unity-UI-Motion
 * -------------------------------------------------------- */

[AddComponentMenu("")]
internal class Fade
{
    private readonly CanvasGroup _canvasGroup;
    private readonly MonoBehaviour _monoBehaviour;

    private const float transparent = 0f;
    private const float visible = 1.0f;

    public Fade(CanvasGroup cg, MonoBehaviour runner)
    {
        _canvasGroup = cg;
        _monoBehaviour = runner;
    }

    // ----------------------------------------------------- PUBLIC API -----------------------------------------------------

    public void TurnInvisible()
    {
        if (_canvasGroup == null) { return; }

        _canvasGroup.alpha = transparent;
    }

    public void TurnVisible()
    {
        if (_canvasGroup == null) { return; }

        _canvasGroup.alpha = visible;
    }

    public void FadeIn(float delay = 0f, float duration = Motion.defaultDuration, UnityAction fadeStart = null, UnityAction fadeEnd = null)
    {
        _monoBehaviour.StartCoroutine(FadeUiIn(delay, duration, fadeStart, fadeEnd));
    }

    public void FadeOut(float delay = 0f, float duration = Motion.defaultDuration, UnityAction fadeStart = null, UnityAction fadeEnd = null)
    {
        _monoBehaviour.StartCoroutine(FadeUiOut(delay, duration, fadeStart, fadeEnd));
    }

    // ----------------------------------------------------- FADE IN ANIMATION -----------------------------------------------------

    private IEnumerator FadeUiIn(float delay, float duration, UnityAction fadeStart, UnityAction fadeEnd)
    {
        if (_canvasGroup == null) { yield break; }

        fadeStart?.Invoke();
        float elapsedTime = 0f;
        _canvasGroup.alpha = transparent;
        if (delay > 0) { yield return new WaitForSeconds(delay); }

        while (elapsedTime < duration)
        {
            _canvasGroup.alpha = Mathf.Lerp(transparent, visible, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _canvasGroup.alpha = visible;
        fadeEnd?.Invoke();
    }

    // ----------------------------------------------------- FADE OUT ANIMATION -----------------------------------------------------

    private IEnumerator FadeUiOut(float delay, float duration, UnityAction fadeStart, UnityAction fadeEnd)
    {
        if (_canvasGroup == null) { yield break; }

        fadeStart?.Invoke();
        float elapsedTime = 0f;
        _canvasGroup.alpha = visible;
        if (delay > 0) { yield return new WaitForSeconds(delay); }

        while (elapsedTime < duration)
        {
            _canvasGroup.alpha = Mathf.Lerp(visible, transparent, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _canvasGroup.alpha = transparent;
        fadeEnd?.Invoke();
    }


}