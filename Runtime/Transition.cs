using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
 * Version: 2.0.0
 * GitHub: https://github.com/Hollow1/Unity-UI-Motion
 * -------------------------------------------------------- */

[AddComponentMenu("")]
internal class Transition
{
    private readonly TextMeshProUGUI textComponent;
    private readonly Image imageComponent;
    private readonly RectTransform panelTransform;
    private readonly MonoBehaviour monoBehaviour;

    private const float transitionTime = 0.5f;

    public Transition(TextMeshProUGUI text, Image image, RectTransform panel, MonoBehaviour runner)
    {
        textComponent = text;
        imageComponent = image;
        panelTransform = panel;
        monoBehaviour = runner;
    }

    // ----------------------------------------------------- PUBLIC API -----------------------------------------------------

    /// <summary>
    /// Transitions the UI element up using the default duration with delay
    /// </summary>
    /// <param name="target">Target component to transition (Text, Image, Panel, or All)</param>
    /// <param name="offset">Offset in pixels (or units depending on canvas scaling). Positive values move the element down by this amount before animating to its original position.</param>
    /// <param name="easing">Specifies the easing method the transition should use</param>
    /// <param name="duration">Time in seconds for the transition duration</param>
    /// <param name="delay">Time in seconds to wait before starting the transition</param>
    public void TransitionUp(Motion.TransitionTarget target, float offset, Motion.EasingType easing = Motion.EasingType.Linear, float duration = transitionTime, float delay = 0f)
    {
        switch (target)
        {
            case Motion.TransitionTarget.Text:
                monoBehaviour.StartCoroutine(TextTransition(new Vector2(0, offset), duration, delay, easing));
                break;
            case Motion.TransitionTarget.Image:
                monoBehaviour.StartCoroutine(ImageTransition(new Vector2(0, offset), duration, delay, easing));
                break;
            case Motion.TransitionTarget.Panel:
                monoBehaviour.StartCoroutine(PanelTransition(new Vector2(0, offset), duration, delay, easing));
                break;
            case Motion.TransitionTarget.All:
                monoBehaviour.StartCoroutine(TextTransition(new Vector2(0, offset), duration, delay, easing));
                monoBehaviour.StartCoroutine(ImageTransition(new Vector2(0, offset), duration, delay, easing));
                monoBehaviour.StartCoroutine(PanelTransition(new Vector2(0, offset), duration, delay, easing));
                break;
        }
    }

    /// <summary>
    /// Transitions the UI element down using the default duration with delay
    /// </summary>
    /// <param name="target">Target component to transition (Text, Image, Panel, or All)</param>
    /// <param name="offset">Offset in pixels (or units depending on canvas scaling). Positive values move the element up by this amount before animating to its original position.</param>
    /// <param name="easing">Specifies the easing method the transition should use</param>
    /// <param name="duration">Time in seconds for the transition duration</param>
    /// <param name="delay">Time in seconds to wait before starting the transition</param>
    public void TransitionDown(Motion.TransitionTarget target, float offset, Motion.EasingType easing = Motion.EasingType.Linear, float duration = transitionTime, float delay = 0f)
    {
        switch (target)
        {
            case Motion.TransitionTarget.Text:
                monoBehaviour.StartCoroutine(TextTransition(new Vector2(0, -offset), duration, delay, easing));
                break;
            case Motion.TransitionTarget.Image:
                monoBehaviour.StartCoroutine(ImageTransition(new Vector2(0, -offset), duration, delay, easing));
                break;
            case Motion.TransitionTarget.Panel:
                monoBehaviour.StartCoroutine(PanelTransition(new Vector2(0, -offset), duration, delay, easing));
                break;
            case Motion.TransitionTarget.All:
                monoBehaviour.StartCoroutine(TextTransition(new Vector2(0, -offset), duration, delay, easing));
                monoBehaviour.StartCoroutine(ImageTransition(new Vector2(0, -offset), duration, delay, easing));
                monoBehaviour.StartCoroutine(PanelTransition(new Vector2(0, -offset), duration, delay, easing));
                break;
        }
    }

    /// <summary>
    /// Transitions the UI element from right to left using the default duration with delay
    /// </summary>
    /// <param name="target">Target component to transition (Text, Image, Panel, or All)</param>
    /// <param name="offset">Offset in pixels (or units depending on canvas scaling). Positive values move the element to the right by this amount before animating to its original position.</param>
    /// <param name="easing">Specifies the easing method the transition should use</param>
    /// <param name="duration">Time in seconds for the transition duration</param>
    /// <param name="delay">Time in seconds to wait before starting the transition</param>
    public void TransitionLeft(Motion.TransitionTarget target, float offset, Motion.EasingType easing = Motion.EasingType.Linear, float duration = transitionTime, float delay = 0f)
    {
        switch (target)
        {
            case Motion.TransitionTarget.Text:
                monoBehaviour.StartCoroutine(TextTransition(new Vector2(-offset, 0), duration, delay, easing));
                break;
            case Motion.TransitionTarget.Image:
                monoBehaviour.StartCoroutine(ImageTransition(new Vector2(-offset, 0), duration, delay, easing));
                break;
            case Motion.TransitionTarget.Panel:
                monoBehaviour.StartCoroutine(PanelTransition(new Vector2(-offset, 0), duration, delay, easing));
                break;
            case Motion.TransitionTarget.All:
                monoBehaviour.StartCoroutine(TextTransition(new Vector2(-offset, 0), duration, delay, easing));
                monoBehaviour.StartCoroutine(ImageTransition(new Vector2(-offset, 0), duration, delay, easing));
                monoBehaviour.StartCoroutine(PanelTransition(new Vector2(-offset, 0), duration, delay, easing));
                break;
        }
    }

    /// <summary>
    /// Transitions the UI element from left to right using the default duration with delay
    /// </summary>
    /// <param name="target">Target component to transition (Text, Image, Panel, or All)</param>
    /// <param name="offset">Offset in pixels (or units depending on canvas scaling). Positive values move the element to the left by this amount before animating to its original position.</param>
    /// <param name="easing">Specifies the easing method the transition should use</param>
    /// <param name="duration">Time in seconds for the transition duration</param>
    /// <param name="delay">Time in seconds to wait before starting the transition</param>
    public void TransitionRight(Motion.TransitionTarget target, float offset, Motion.EasingType easing = Motion.EasingType.Linear, float duration = transitionTime, float delay = 0f)
    {
        switch (target)
        {
            case Motion.TransitionTarget.Text:
                monoBehaviour.StartCoroutine(TextTransition(new Vector2(offset, 0), duration, delay, easing));
                break;
            case Motion.TransitionTarget.Image:
                monoBehaviour.StartCoroutine(ImageTransition(new Vector2(offset, 0), duration, delay, easing));
                break;
            case Motion.TransitionTarget.Panel:
                monoBehaviour.StartCoroutine(PanelTransition(new Vector2(offset, 0), duration, delay, easing));
                break;
            case Motion.TransitionTarget.All:
                monoBehaviour.StartCoroutine(TextTransition(new Vector2(offset, 0), duration, delay, easing));
                monoBehaviour.StartCoroutine(ImageTransition(new Vector2(offset, 0), duration, delay, easing));
                monoBehaviour.StartCoroutine(PanelTransition(new Vector2(offset, 0), duration, delay, easing));
                break;
        }
    }

    /// <summary>
    /// Transitions the UI element on both axis based on the offset
    /// </summary>
    /// <param name="target">Target component to transition (Text, Image, Panel, or All)</param>
    /// <param name="offset">Offset in pixels (or units depending on canvas scaling). Positive values move the element to the right and up by this amount before animating to its original position.</param>
    /// <param name="easing">Specifies the easing method the transition should use</param>
    /// <param name="duration">Time in seconds for the transition duration</param>
    /// <param name="delay">Time in seconds to wait before starting the transition</param>
    public void TransitionPosition(Motion.TransitionTarget target, Vector2 offset, Motion.EasingType easing = Motion.EasingType.Linear, float duration = transitionTime, float delay = 0f)
    {
        switch (target)
        {
            case Motion.TransitionTarget.Text:
                monoBehaviour.StartCoroutine(TextTransition(-offset, duration, delay, easing));
                break;
            case Motion.TransitionTarget.Image:
                monoBehaviour.StartCoroutine(ImageTransition(-offset, duration, delay, easing));
                break;
            case Motion.TransitionTarget.Panel:
                monoBehaviour.StartCoroutine(PanelTransition(-offset, duration, delay, easing));
                break;
            case Motion.TransitionTarget.All:
                monoBehaviour.StartCoroutine(TextTransition(-offset, duration, delay, easing));
                monoBehaviour.StartCoroutine(ImageTransition(-offset, duration, delay, easing));
                monoBehaviour.StartCoroutine(PanelTransition(-offset, duration, delay, easing));
                break;
        }
    }

    // ----------------------------------------------------- TEXT BASED TRANSITIONS -----------------------------------------------------

    private IEnumerator TextTransition(Vector2 offset, float duration, float delay, Motion.EasingType easing)
    {
        if (textComponent == null) { yield break; }

        float elapsedTime = 0f;
        Vector2 startPos, targetPos;

        targetPos = textComponent.rectTransform.anchoredPosition;
        startPos = targetPos - offset;
        textComponent.rectTransform.anchoredPosition = startPos;
        if (delay > 0) { yield return new WaitForSeconds(delay); }

        while (elapsedTime < duration)
        {
            float time = elapsedTime / duration;
            float easedTime = Easing.SetEasingFunction(time, easing);

            textComponent.rectTransform.anchoredPosition = Vector2.Lerp(startPos, targetPos, easedTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        textComponent.rectTransform.anchoredPosition = targetPos;
    }

    // ----------------------------------------------------- IMAGE BASED TRANSITIONS -----------------------------------------------------

    private IEnumerator ImageTransition(Vector2 offset, float duration, float delay, Motion.EasingType easing)
    {
        if (imageComponent == null) { yield break; }

        float elapsedTime = 0f;
        Vector2 startPos, targetPos;

        targetPos = imageComponent.rectTransform.anchoredPosition;
        startPos = targetPos - offset;
        imageComponent.rectTransform.anchoredPosition = startPos;
        if (delay > 0) { yield return new WaitForSeconds(delay); }

        while (elapsedTime < duration)
        {
            float time = elapsedTime / duration;
            float easedTime = Easing.SetEasingFunction(time, easing);

            imageComponent.rectTransform.anchoredPosition = Vector2.Lerp(startPos, targetPos, easedTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        imageComponent.rectTransform.anchoredPosition = targetPos;
    }

    // ----------------------------------------------------- PANEL BASED TRANSITIONS -----------------------------------------------------

    private IEnumerator PanelTransition(Vector2 offset, float duration, float delay, Motion.EasingType easing)
    {
        if (panelTransform == null) { yield break; }

        float elapsedTime = 0f;
        Vector2 startPos, targetPos;

        targetPos = panelTransform.anchoredPosition;
        startPos = targetPos - offset;
        panelTransform.anchoredPosition = startPos;
        if (delay > 0) { yield return new WaitForSeconds(delay); }

        while (elapsedTime < duration)
        {
            float time = elapsedTime / duration;
            float easedTime = Easing.SetEasingFunction(time, easing);

            panelTransform.anchoredPosition = Vector2.Lerp(startPos, targetPos, easedTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        panelTransform.anchoredPosition = targetPos;
    }

}