using System.Collections;
using UnityEngine;

/* --------------------------------------------------------
 * Unity UI Motion - Fade Animation Component
 * Created by Hollow1
 * 
 * A lightweight animation component for creating smooth 
 * fade transitions on Unity UI elements with support for
 * custom durations and delays.
 * 
 * Version: 1.0.0
 * GitHub: https://github.com/Hollow1/Unity-UI-Motion
 * -------------------------------------------------------- */
public class Fade : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    private const float transparent = 0f;
    private const float visible = 1.0f;
    private const float fadeDuration = 0.5f;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            Debug.LogWarning($"[{gameObject.name}] No CanvasGroup component found");
        }
    }

    // ----------------------------------------------------- PUBLIC API -----------------------------------------------------

    /// <summary>
    /// Immediately sets the UI element to be invisible without an animation
    /// </summary>
    public void TurnInvisible()
    {
        if (canvasGroup == null) { return; }

        canvasGroup.alpha = transparent;
    }

    /// <summary>
    /// Immediately sets the UI element to be visible without an animation
    /// </summary>
    public void TurnVisible()
    {
        if (canvasGroup == null) { return; }

        canvasGroup.alpha = visible;
    }

    /// <summary>
    /// Fades in the UI element with a custom delay and duration
    /// </summary>
    /// <param name="delay">Time in seconds to wait before starting the fade</param>
    /// <param name="duration">Time in seconds the fade animation should take</param>
    public void FadeIn(float delay = 0f, float duration = fadeDuration)
    {
        StartCoroutine(FadeUiIn(delay, duration));
    }

    /// <summary>
    /// Fades out the UI element with a custom delay and duration
    /// </summary>
    /// <param name="delay">Time in seconds to wait before starting the fade</param>
    /// <param name="duration">Time in seconds the fade animation should take</param>
    public void FadeOut(float delay = 0f, float duration = fadeDuration)
    {
        StartCoroutine(FadeUiOut(delay, duration));
    }

    private IEnumerator FadeUiIn(float delay, float duration)
    {
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

    public IEnumerator FadeUiOut(float delay, float duration)
    {
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
