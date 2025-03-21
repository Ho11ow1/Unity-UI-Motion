using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/* --------------------------------------------------------
 * Unity UI Motion - Scale Animation Component
 * Created by Hollow1
 * 
 * A lightweight scaling component for creating smooth 
 * scaling transitions on Unity UI elements with support
 * for multiple easing functions and custom durations / delays.
 * 
 * Version: 2.0.0
 * GitHub: https://github.com/Hollow1/Unity-UI-Motion
 * -------------------------------------------------------- */

[AddComponentMenu("")]
internal class Scale
{
    private readonly TextMeshProUGUI textComponent;
    private readonly Image imageComponent;
    private readonly RectTransform panelTransform;
    private readonly MonoBehaviour monoBehaviour;

    private readonly Vector2[] originalScale = { Vector2.zero, Vector2.zero, Vector2.zero };
    private readonly bool[] storedScale = { false, false, false };

    private const float scalingTime = 0.5f;

    public Scale(TextMeshProUGUI text, Image image, RectTransform panel, MonoBehaviour runner)
    {
        textComponent = text;
        imageComponent = image;
        panelTransform = panel;
        monoBehaviour = runner;
    }

    /// <summary>
    /// Scales up the UI element using the default duration and delay
    /// </summary>
    /// <param name="target">Target component to scale (Text, Image, Panel, or All)</param>
    /// <param name="multiplier">Scale multiplier. Values greater than 1 increase size, must be greater than 0. (Scale is based on the local scale of the parent)</param>
    /// <param name="easing">Specifies the easing method the scaling should use</param>
    /// <param name="duration">Time in seconds the scaling animation should take</param>
    /// <param name="delay">Time in seconds to wait before starting the scaling</param>
    public void ScaleUp(Motion.TransitionTarget target, float multiplier, Motion.EasingType easing = Motion.EasingType.Linear, float duration = scalingTime, float delay = 0f)
    {
        switch (target)
        {
            case Motion.TransitionTarget.Text:
                monoBehaviour.StartCoroutine(TextScale(multiplier, duration, delay, easing));
                break;
            case Motion.TransitionTarget.Image:
                monoBehaviour.StartCoroutine(ImageScale(multiplier, duration, delay, easing));
                break;
            case Motion.TransitionTarget.Panel:
                monoBehaviour.StartCoroutine(PanelScale(multiplier, duration, delay, easing));
                break;
            case Motion.TransitionTarget.All:
                monoBehaviour.StartCoroutine(TextScale(multiplier, duration, delay, easing));
                monoBehaviour.StartCoroutine(ImageScale(multiplier, duration, delay, easing));
                monoBehaviour.StartCoroutine(PanelScale(multiplier, duration, delay, easing));
                break;
        }
    }

    /// <summary>
    /// Scales down the UI element using the default duration and delay
    /// </summary>
    /// <param name="target">Target component to scale (Text, Image, Panel, or All)</param>
    /// <param name="multiplier">Scale multiplier. Values greater than 1 decrease size, must be greater than 0. (Scale is based on the local scale of the parent)</param>
    /// <param name="easing">Specifies the easing method the scaling should use</param>
    /// <param name="duration">Time in seconds the scaling animation should take</param>
    /// <param name="delay">Time in seconds to wait before starting the scaling</param>
    public void ScaleDown(Motion.TransitionTarget target, float multiplier, Motion.EasingType easing = Motion.EasingType.Linear, float duration = scalingTime, float delay = 0f)
    {
        switch (target)
        {
            case Motion.TransitionTarget.Text:
                monoBehaviour.StartCoroutine(TextScale( 1/ multiplier, duration, delay, easing));
                break;
            case Motion.TransitionTarget.Image:
                monoBehaviour.StartCoroutine(ImageScale( 1 / multiplier, duration, delay, easing));
                break;
            case Motion.TransitionTarget.Panel:
                monoBehaviour.StartCoroutine(PanelScale(1 / multiplier, duration, delay, easing));
                break;
            case Motion.TransitionTarget.All:
                monoBehaviour.StartCoroutine(TextScale(1 / multiplier, duration, delay, easing));
                monoBehaviour.StartCoroutine(ImageScale(1 / multiplier, duration, delay, easing));
                monoBehaviour.StartCoroutine(PanelScale(1 / multiplier, duration, delay, easing));
                break;
        }
    }

    private IEnumerator TextScale(float scaleAmount, float duration, float delay, Motion.EasingType easing)
    {
        if (textComponent == null) { yield break; }
        if (scaleAmount <= 0) { throw new ArgumentOutOfRangeException(nameof(scaleAmount), "Scaling amount must be greater than 0"); }

        Vector2 startScale, targetScale;
        if (!storedScale[0])
        {
            startScale = textComponent.rectTransform.localScale;
            originalScale[0] = startScale;
            storedScale[0] = true;
        }
        else
        {
            startScale = originalScale[0];
        }

        float elapsedTime = 0f;
        targetScale = startScale * scaleAmount;
        if (delay > 0) { yield return new WaitForSeconds(delay); }

        while (elapsedTime < duration)
        {
            float time = elapsedTime / duration;
            float easedTime = Easing.SetEasingFunction(time, easing);

            textComponent.rectTransform.localScale = Vector2.Lerp(startScale, targetScale, easedTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        textComponent.rectTransform.localScale = targetScale;
    }

    private IEnumerator ImageScale(float scaleAmount, float duration, float delay, Motion.EasingType easing)
    {
        if (imageComponent == null) { yield break; }
        if (scaleAmount <= 0) { throw new ArgumentOutOfRangeException(nameof(scaleAmount), "Scaling amount must be greater than 0"); }

        Vector2 startScale, targetScale;
        if (!storedScale[1])
        {
            startScale = imageComponent.rectTransform.localScale;
            originalScale[1] = startScale;
            storedScale[1] = true;
        }
        else
        {
            startScale = originalScale[1];
        }

        float elapsedTime = 0f;
        targetScale = startScale * scaleAmount;
        if (delay > 0) { yield return new WaitForSeconds(delay); }

        while (elapsedTime < duration)
        {
            float time = elapsedTime / duration;
            float easedTime = Easing.SetEasingFunction(time, easing);

            imageComponent.rectTransform.localScale = Vector2.Lerp(startScale, targetScale, easedTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        imageComponent.rectTransform.localScale = targetScale;
    }

    private IEnumerator PanelScale(float scaleAmount, float duration, float delay, Motion.EasingType easing)
    {
        if (panelTransform == null) { yield break; }
        if (scaleAmount <= 0) { throw new ArgumentOutOfRangeException(nameof(scaleAmount), "Scaling amount must be greater than 0"); }

        Vector2 startScale, targetScale;
        if (!storedScale[2])
        {
            startScale = panelTransform.localScale;
            originalScale[2] = startScale;
            storedScale[2] = true;
        }
        else
        {
            startScale = originalScale[2];
        }

        float elapsedTime = 0f;
        targetScale = startScale * scaleAmount;
        if (delay > 0) { yield return new WaitForSeconds(delay); }
        
        while (elapsedTime < duration)
        {
            float time = elapsedTime / duration;
            float easedTime = Easing.SetEasingFunction(time, easing);

            panelTransform.localScale = Vector2.Lerp(startScale, targetScale, easedTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        panelTransform.localScale = targetScale;
    }

}
