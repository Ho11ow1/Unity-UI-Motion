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
 * Applies a scaling animation to a UI component
 * 
 * Version: 2.1.0
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

    // ----------------------------------------------------- PUBLIC API -----------------------------------------------------

    public void ScaleUp(Motion.TransitionTarget target, float multiplier, Motion.EasingType easing = Motion.EasingType.Linear, float duration = scalingTime, float delay = 0f)
    {
        switch (target)
        {
            case Motion.TransitionTarget.Text:
                monoBehaviour.StartCoroutine(ScaleUi(textComponent.rectTransform, multiplier, duration, delay, easing));
                break;
            case Motion.TransitionTarget.Image:
                monoBehaviour.StartCoroutine(ScaleUi(imageComponent.rectTransform, multiplier, duration, delay, easing));
                break;
            case Motion.TransitionTarget.Panel:
                monoBehaviour.StartCoroutine(ScaleUi(panelTransform, multiplier, duration, delay, easing));
                break;
            case Motion.TransitionTarget.All:
                monoBehaviour.StartCoroutine(ScaleUi(textComponent.rectTransform, multiplier, duration, delay, easing));
                monoBehaviour.StartCoroutine(ScaleUi(imageComponent.rectTransform, multiplier, duration, delay, easing));
                monoBehaviour.StartCoroutine(ScaleUi(panelTransform, multiplier, duration, delay, easing));
                break;
        }
    }

    public void ScaleDown(Motion.TransitionTarget target, float multiplier, Motion.EasingType easing = Motion.EasingType.Linear, float duration = scalingTime, float delay = 0f)
    {
        switch (target)
        {
            case Motion.TransitionTarget.Text:
                monoBehaviour.StartCoroutine(ScaleUi(textComponent.rectTransform, 1/ multiplier, duration, delay, easing));
                break;
            case Motion.TransitionTarget.Image:
                monoBehaviour.StartCoroutine(ScaleUi(imageComponent.rectTransform, 1 / multiplier, duration, delay, easing));
                break;
            case Motion.TransitionTarget.Panel:
                monoBehaviour.StartCoroutine(ScaleUi(panelTransform, 1 / multiplier, duration, delay, easing));
                break;
            case Motion.TransitionTarget.All:
                monoBehaviour.StartCoroutine(ScaleUi(textComponent.rectTransform, 1 / multiplier, duration, delay, easing));
                monoBehaviour.StartCoroutine(ScaleUi(imageComponent.rectTransform, 1 / multiplier, duration, delay, easing));
                monoBehaviour.StartCoroutine(ScaleUi(panelTransform, 1 / multiplier, duration, delay, easing));
                break;
        }
    }

    // ----------------------------------------------------- SCALE ANIMATION -----------------------------------------------------

    private IEnumerator ScaleUi(RectTransform component, float scaleAmount, float duration, float delay, Motion.EasingType easing)
    {
        if (component == null) { yield break; }
        if (scaleAmount <= 0) { throw new ArgumentOutOfRangeException(nameof(scaleAmount), "Scaling amount must be greater than 0"); }

        Vector2 startScale = Vector2.zero;
        Vector2 targetScale;

        if (component == textComponent.rectTransform)
        {
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
        }
        else if (component == imageComponent.rectTransform)
        {
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
        }
        else if (component == panelTransform)
        {
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
        }

        float elapsedTime = 0f;
        targetScale = startScale * scaleAmount;
        if (delay > 0) { yield return new WaitForSeconds(delay); }

        while (elapsedTime < duration)
        {
            float time = elapsedTime / duration;
            float easedTime = Easing.SetEasingFunction(time, easing);

            component.localScale = Vector2.Lerp(startScale, targetScale, easedTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        component.localScale = targetScale;
    }


}
