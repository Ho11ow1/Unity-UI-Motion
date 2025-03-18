using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;

/* --------------------------------------------------------
 * Unity UI Motion - Position Animation Component
 * Created by Hollow1
 * 
 * A lightweight animation component for creating smooth
 * position transitions on Unity UI elements with support
 * for multiple easing functions and directions.
 * 
 * Version: 1.0.0
 * GitHub: https://github.com/Hollow1/Unity-UI-Motion
 * -------------------------------------------------------- */
public class Transition : MonoBehaviour
{
    public enum TransitionTarget
    {
        Text,
        Image,
        Panel,
        All
    }

    public enum EasingType
    {
        Linear,
        Cubic,
        EaseIn, // Quadratic
        EaseOut, // Quadratic
        EaseInOut // Quadratic
    }

    private TextMeshProUGUI textComponent;
    private Image imageComponent;
    private RectTransform panelTransform;

    private Vector2 startPos;
    private Vector2 targetPos;
    private const float transitionTime = 0.5f;

    void Start()
    {
        textComponent = GetComponentInChildren<TextMeshProUGUI>();
        if (textComponent == null)
        {
            Debug.LogWarning($"[{gameObject.name}] No Text component found in children. Parent: [{transform.parent.name ?? "none"}]");
        }

        imageComponent = GetComponentsInChildren<Image>().FirstOrDefault((img) => img.gameObject != gameObject);
        if (imageComponent == null)
        {
            Debug.LogWarning($"[{gameObject.name}] No Image component found in children. Parent: [{transform.parent.name ?? "none"}]");
        }

        panelTransform = GetComponent<RectTransform>();
        if (panelTransform == null)
        {
            Debug.LogWarning($"[{gameObject.name}] No RectTransform component found.");
        }
    }

    // ----------------------------------------------------- PUBLIC API -----------------------------------------------------

    /// <summary>
    /// Transitions the UI element up using the default duration with delay
    /// </summary>
    /// <param name="target">Target component to transition (Text, Image, Panel, or All)</param>
    /// <param name="offset">Offset in pixels (or units depending on canvas scaling). Positive values move the element down by this amount before animating to its final position.</param>
    /// <param name="easing">Specifies the easing method the transition should use</param>
    /// <param name="duration">Time in seconds for the transition duration</param>
    /// <param name="delay">Time in seconds to wait before starting the transition</param>
    public void TransitionUp(TransitionTarget target, float offset, EasingType easing = EasingType.Linear, float duration = transitionTime, float delay = 0f)
    {
        switch (target)
        {
            case TransitionTarget.Text:
                StartCoroutine(TextTransitionUp(offset, duration, delay, easing));
                break;
            case TransitionTarget.Image:
                StartCoroutine(ImageTransitionUp(offset, duration, delay, easing));
                break;
            case TransitionTarget.Panel:
                StartCoroutine(PanelTransitionUp(offset, duration, delay, easing));
                break;
            case TransitionTarget.All:
                StartCoroutine(TextTransitionUp(offset, duration, delay, easing));
                StartCoroutine(ImageTransitionUp(offset, duration, delay, easing));
                StartCoroutine(PanelTransitionUp(offset, duration, delay, easing));
                break;
        }
    }

    /// <summary>
    /// Transitions the UI element down using the default duration with delay
    /// </summary>
    /// <param name="target">Target component to transition (Text, Image, Panel, or All)</param>
    /// <param name="offset">Offset in pixels (or units depending on canvas scaling). Positive values move the element up by this amount before animating to its final position.</param>
    /// <param name="easing">Specifies the easing method the transition should use</param>
    /// <param name="duration">Time in seconds for the transition duration</param>
    /// <param name="delay">Time in seconds to wait before starting the transition</param>
    public void TransitionDown(TransitionTarget target, float offset, EasingType easing = EasingType.Linear, float duration = transitionTime, float delay = 0f)
    {
        switch (target)
        {
            case TransitionTarget.Text:
                StartCoroutine(TextTransitionDown(offset, duration, delay, easing));
                break;
            case TransitionTarget.Image:
                StartCoroutine(ImageTransitionDown(offset, duration, delay, easing));
                break;
            case TransitionTarget.Panel:
                StartCoroutine(PanelTransitionDown(offset, duration, delay, easing));
                break;
            case TransitionTarget.All:
                StartCoroutine(TextTransitionDown(offset, duration, delay, easing));
                StartCoroutine(ImageTransitionDown(offset, duration, delay, easing));
                StartCoroutine(PanelTransitionDown(offset, duration, delay, easing));
                break;
        }
    }

    /// <summary>
    /// Transitions the UI element from right to left using the default duration with delay
    /// </summary>
    /// <param name="target">Target component to transition (Text, Image, Panel, or All)</param>
    /// <param name="offset">Offset in pixels (or units depending on canvas scaling). Positive values move the element to the right by this amount before animating to its final position.</param>
    /// <param name="easing">Specifies the easing method the transition should use</param>
    /// <param name="duration">Time in seconds for the transition duration</param>
    /// <param name="delay">Time in seconds to wait before starting the transition</param>
    public void TransitionLeft(TransitionTarget target, float offset, EasingType easing = EasingType.Linear, float duration = transitionTime, float delay = 0f)
    {
        switch (target)
        {
            case TransitionTarget.Text:
                StartCoroutine(TextTransitionLeft(offset, duration, delay, easing));
                break;
            case TransitionTarget.Image:
                StartCoroutine(ImageTransitionLeft(offset, duration, delay, easing));
                break;
            case TransitionTarget.Panel:
                StartCoroutine(PanelTransitionLeft(offset, duration, delay, easing));
                break;
            case TransitionTarget.All:
                StartCoroutine(TextTransitionLeft(offset, duration, delay, easing));
                StartCoroutine(ImageTransitionLeft(offset, duration, delay, easing));
                StartCoroutine(PanelTransitionLeft(offset, duration, delay, easing));
                break;
        }
    }

    /// <summary>
    /// Transitions the UI element from left to right using the default duration with delay
    /// </summary>
    /// <param name="target">Target component to transition (Text, Image, Panel, or All)</param>
    /// <param name="offset">Offset in pixels (or units depending on canvas scaling). Positive values move the element to the left by this amount before animating to its final position.</param>
    /// <param name="easing">Specifies the easing method the transition should use</param>
    /// <param name="duration">Time in seconds for the transition duration</param>
    /// <param name="delay">Time in seconds to wait before starting the transition</param>
    public void TransitionRight(TransitionTarget target, float offset, EasingType easing = EasingType.Linear, float duration = transitionTime, float delay = 0f)
    {
        switch (target)
        {
            case TransitionTarget.Text:
                StartCoroutine(TextTransitionRight(offset, duration, delay, easing));
                break;
            case TransitionTarget.Image:
                StartCoroutine(ImageTransitionRight(offset, duration, delay, easing));
                break;
            case TransitionTarget.Panel:
                StartCoroutine(PanelTransitionRight(offset, duration, delay, easing));
                break;
            case TransitionTarget.All:
                StartCoroutine(TextTransitionRight(offset, duration, delay, easing));
                StartCoroutine(ImageTransitionRight(offset, duration, delay, easing));
                StartCoroutine(PanelTransitionRight(offset, duration, delay, easing));
                break;
        }
    }

    // ----------------------------------------------------- TEXT BASED TRANSITIONS -----------------------------------------------------

    private IEnumerator TextTransitionUp(float offset, float duration, float delay, EasingType easing)
    {
        if (textComponent == null) { yield break; }

        float elapsedTime = 0f;
        targetPos = textComponent.rectTransform.anchoredPosition;
        startPos = new Vector2(targetPos.x, targetPos.y - offset);
        textComponent.rectTransform.anchoredPosition = startPos;
        if (delay > 0) { yield return new WaitForSeconds(delay); }

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            float easedT = SetEasingFunction(t, easing);

            textComponent.rectTransform.anchoredPosition = Vector2.Lerp(startPos, targetPos, easedT);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        textComponent.rectTransform.anchoredPosition = targetPos;
    }

    private IEnumerator TextTransitionDown(float offset, float duration, float delay, EasingType easing)
    {
        if (textComponent == null) { yield break; }

        float elapsedTime = 0f;
        targetPos = textComponent.rectTransform.anchoredPosition;
        startPos = new Vector2(targetPos.x, targetPos.y + offset);
        textComponent.rectTransform.anchoredPosition = startPos;
        if (delay > 0) { yield return new WaitForSeconds(delay); }

        while (elapsedTime < duration)
        {
            float time = elapsedTime / duration;
            float easedTime = SetEasingFunction(time, easing);

            textComponent.rectTransform.anchoredPosition = Vector2.Lerp(startPos, targetPos, easedTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        textComponent.rectTransform.anchoredPosition = targetPos;
    }

    private IEnumerator TextTransitionLeft(float offset, float duration, float delay, EasingType easing)
    {
        if (textComponent == null) { yield break; }

        float elapsedTime = 0f;
        targetPos = textComponent.rectTransform.anchoredPosition;
        startPos = new Vector2(targetPos.x + offset, targetPos.y);
        textComponent.rectTransform.anchoredPosition = startPos;
        if (delay > 0) { yield return new WaitForSeconds(delay); }

        while (elapsedTime < duration)
        {
            float time = elapsedTime / duration;
            float easedTime = SetEasingFunction(time, easing);

            textComponent.rectTransform.anchoredPosition = Vector2.Lerp(startPos, targetPos, easedTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        textComponent.rectTransform.anchoredPosition = targetPos;
    }

    private IEnumerator TextTransitionRight(float offset, float duration, float delay, EasingType easing)
    {
        if (textComponent == null) { yield break; }

        float elapsedTime = 0f;
        targetPos = textComponent.rectTransform.anchoredPosition;
        startPos = new Vector2(targetPos.x - offset, targetPos.y);
        textComponent.rectTransform.anchoredPosition = startPos;
        if (delay > 0) { yield return new WaitForSeconds(delay); }

        while (elapsedTime < duration)
        {
            float time = elapsedTime / duration;
            float easedTime = SetEasingFunction(time, easing);

            textComponent.rectTransform.anchoredPosition = Vector2.Lerp(startPos, targetPos, easedTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        textComponent.rectTransform.anchoredPosition = targetPos;
    }

    // ----------------------------------------------------- IMAGE BASED TRANSITIONS -----------------------------------------------------

    private IEnumerator ImageTransitionUp(float offset, float duration, float delay, EasingType easing)
    {
        if (imageComponent == null) { yield break; }

        float elapsedTime = 0f;
        targetPos = imageComponent.rectTransform.anchoredPosition;
        startPos = new Vector2(targetPos.x, targetPos.y - offset);
        imageComponent.rectTransform.anchoredPosition = startPos;
        if (delay > 0) { yield return new WaitForSeconds(delay); }

        while (elapsedTime < duration)
        {
            float time = elapsedTime / duration;
            float easedTime = SetEasingFunction(time, easing);

            imageComponent.rectTransform.anchoredPosition = Vector2.Lerp(startPos, targetPos, easedTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        imageComponent.rectTransform.anchoredPosition = targetPos;
    }

    private IEnumerator ImageTransitionDown(float offset, float duration, float delay, EasingType easing)
    {
        if (imageComponent == null) { yield break; }

        float elapsedTime = 0f;
        targetPos = imageComponent.rectTransform.anchoredPosition;
        startPos = new Vector2(targetPos.x, targetPos.y + offset);
        imageComponent.rectTransform.anchoredPosition = startPos;
        if (delay > 0) { yield return new WaitForSeconds(delay); }

        while (elapsedTime < duration)
        {
            float time = elapsedTime / duration;
            float easedTime = SetEasingFunction(time, easing);

            imageComponent.rectTransform.anchoredPosition = Vector2.Lerp(startPos, targetPos, easedTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        imageComponent.rectTransform.anchoredPosition = targetPos;
    }

    private IEnumerator ImageTransitionLeft(float offset, float duration, float delay, EasingType easing)
    {
        if (imageComponent == null) { yield break; }

        float elapsedTime = 0f;
        targetPos = imageComponent.rectTransform.anchoredPosition;
        startPos = new Vector2(targetPos.x + offset, targetPos.y);
        imageComponent.rectTransform.anchoredPosition = startPos;
        if (delay > 0) { yield return new WaitForSeconds(delay); }

        while (elapsedTime < duration)
        {
            float time = elapsedTime / duration;
            float easedTime = SetEasingFunction(time, easing);

            imageComponent.rectTransform.anchoredPosition = Vector2.Lerp(startPos, targetPos, easedTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        imageComponent.rectTransform.anchoredPosition = targetPos;
    }

    private IEnumerator ImageTransitionRight(float offset, float duration, float delay, EasingType easing)
    {
        if (imageComponent == null) { yield break; }

        float elapsedTime = 0f;
        targetPos = imageComponent.rectTransform.anchoredPosition;
        startPos = new Vector2(targetPos.x - offset, targetPos.y);
        imageComponent.rectTransform.anchoredPosition = startPos;
        if (delay > 0) { yield return new WaitForSeconds(delay); }

        while (elapsedTime < duration)
        {
            float time = elapsedTime / duration;
            float easedTime = SetEasingFunction(time, easing);

            imageComponent.rectTransform.anchoredPosition = Vector2.Lerp(startPos, targetPos, easedTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        imageComponent.rectTransform.anchoredPosition = targetPos;
    }

    // ----------------------------------------------------- PANEL BASED TRANSITIONS -----------------------------------------------------

    private IEnumerator PanelTransitionUp(float offset, float duration, float delay, EasingType easing)
    {
        if (panelTransform == null) { yield break; }

        float elapsedTime = 0f;
        targetPos = panelTransform.anchoredPosition;
        startPos = new Vector2(targetPos.x, targetPos.y - offset);
        panelTransform.anchoredPosition = startPos;
        if (delay > 0) { yield return new WaitForSeconds(delay); }

        while (elapsedTime < duration)
        {
            float time = elapsedTime / duration;
            float easedTime = SetEasingFunction(time, easing);

            panelTransform.anchoredPosition = Vector2.Lerp(startPos, targetPos, easedTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        panelTransform.anchoredPosition = targetPos;
    }

    private IEnumerator PanelTransitionDown(float offset, float duration, float delay, EasingType easing)
    {
        if (panelTransform == null) { yield break; }

        float elapsedTime = 0f;
        targetPos = panelTransform.anchoredPosition;
        startPos = new Vector2(targetPos.x, targetPos.y + offset);
        panelTransform.anchoredPosition = startPos;
        if (delay > 0) { yield return new WaitForSeconds(delay); }

        while (elapsedTime < duration)
        {
            float time = elapsedTime / duration;
            float easedTime = SetEasingFunction(time, easing);

            panelTransform.anchoredPosition = Vector2.Lerp(startPos, targetPos, easedTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        panelTransform.anchoredPosition = targetPos;
    }

    private IEnumerator PanelTransitionLeft(float offset, float duration, float delay, EasingType easing)
    {
        if (panelTransform == null) { yield break; }

        float elapsedTime = 0f;
        targetPos = panelTransform.anchoredPosition;
        startPos = new Vector2(targetPos.x + offset, targetPos.y);
        panelTransform.anchoredPosition = startPos;
        if (delay > 0) { yield return new WaitForSeconds(delay); }

        while (elapsedTime < duration)
        {
            float time = elapsedTime / duration;
            float easedTime = SetEasingFunction(time, easing);

            panelTransform.anchoredPosition = Vector2.Lerp(startPos, targetPos, easedTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        panelTransform.anchoredPosition = targetPos;
    }

    private IEnumerator PanelTransitionRight(float offset, float duration, float delay, EasingType easing)
    {
        if (panelTransform == null) { yield break; }

        float elapsedTime = 0f;
        targetPos = panelTransform.anchoredPosition;
        startPos = new Vector2(targetPos.x - offset, targetPos.y);
        panelTransform.anchoredPosition = startPos;
        if (delay > 0) { yield return new WaitForSeconds(delay); }

        while (elapsedTime < duration)
        {
            float time = elapsedTime / duration;
            float easedTime = SetEasingFunction(time, easing);

            panelTransform.anchoredPosition = Vector2.Lerp(startPos, targetPos, easedTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        panelTransform.anchoredPosition = targetPos;
    }

    private float SetEasingFunction(float time, EasingType easing)
    {
        switch (easing)
        {
            case EasingType.Linear:
                return time;
            case EasingType.Cubic:
                return time * time * time;
            case EasingType.EaseIn:
                return time * time;
            case EasingType.EaseOut:
                return time * (2 - time);
            case EasingType.EaseInOut:
                return time < 0.5f ? 2 * time * time : -1 + (4 - 2 * time) * time;
            default:
                return time;
        }
    }
}
