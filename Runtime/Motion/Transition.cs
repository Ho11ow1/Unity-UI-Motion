using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using static Motion;

/* --------------------------------------------------------
 * Unity UI Motion - Position Animation Component
 * Created by Hollow1
 * 
 * Applies a position transition to a UI component
 * 
 * Version: 2.1.0
 * GitHub: https://github.com/Hollow1/Unity-UI-Motion
 * -------------------------------------------------------- */

[AddComponentMenu("")]
internal class Transition
{
    private readonly TextMeshProUGUI[] textComponent;
    private readonly Image[] imageComponent;
    private readonly RectTransform panelTransform;
    private readonly MonoBehaviour monoBehaviour;

    private readonly Vector2[][] originalPosition = { new Vector2[10], new Vector2[10], new Vector2[1] };
    private readonly bool[][] storedPosition = { new bool[10], new bool[10], new bool[1] };

    private const float transitionTime = 0.5f;

    public Transition(TextMeshProUGUI[] text, Image[] image, RectTransform panel, MonoBehaviour runner)
    {
        textComponent = text;
        imageComponent = image;
        panelTransform = panel;
        monoBehaviour = runner;
    }

    // ----------------------------------------------------- PUBLIC API -----------------------------------------------------

    public void TransitionFromUp(TransitionTarget target, int occurrence, float offset, EasingType easing = EasingType.Linear, float duration = transitionTime, float delay = 0f)
    {
        switch (target)
        {
            case TransitionTarget.Text:
                monoBehaviour.StartCoroutine(TransitionFrom(textComponent[occurrence].rectTransform, new Vector2(0, -offset), duration, delay, easing));
                break;
            case TransitionTarget.Image:
                monoBehaviour.StartCoroutine(TransitionFrom(imageComponent[occurrence].rectTransform, new Vector2(0, -offset), duration, delay, easing));
                break;
            case TransitionTarget.Panel:
                monoBehaviour.StartCoroutine(TransitionFrom(panelTransform, new Vector2(0, -offset), duration, delay, easing));
                break;
            case TransitionTarget.All:
                monoBehaviour.StartCoroutine(TransitionFrom(textComponent[occurrence].rectTransform, new Vector2(0, -offset), duration, delay, easing));
                monoBehaviour.StartCoroutine(TransitionFrom(imageComponent[occurrence].rectTransform, new Vector2(0, -offset), duration, delay, easing));
                monoBehaviour.StartCoroutine(TransitionFrom(panelTransform, new Vector2(0, -offset), duration, delay, easing));
                break;
        }
    }

    public void TransitionFromDown(TransitionTarget target, int occurrence, float offset, EasingType easing = EasingType.Linear, float duration = transitionTime, float delay = 0f)
    {
        switch (target)
        {
            case TransitionTarget.Text:
                monoBehaviour.StartCoroutine(TransitionFrom(textComponent[occurrence].rectTransform, new Vector2(0, offset), duration, delay, easing));
                break;
            case TransitionTarget.Image:
                monoBehaviour.StartCoroutine(TransitionFrom(imageComponent[occurrence].rectTransform, new Vector2(0, offset), duration, delay, easing));
                break;
            case TransitionTarget.Panel:
                monoBehaviour.StartCoroutine(TransitionFrom(panelTransform, new Vector2(0, -offset), duration, delay, easing));
                break;
            case TransitionTarget.All:
                monoBehaviour.StartCoroutine(TransitionFrom(textComponent[occurrence].rectTransform, new Vector2(0, offset), duration, delay, easing));
                monoBehaviour.StartCoroutine(TransitionFrom(imageComponent[occurrence].rectTransform, new Vector2(0, offset), duration, delay, easing));
                monoBehaviour.StartCoroutine(TransitionFrom(panelTransform, new Vector2(0, offset), duration, delay, easing));
                break;
        }
    }

    public void TransitionFromLeft(TransitionTarget target, int occurrence, float offset, EasingType easing = EasingType.Linear, float duration = transitionTime, float delay = 0f)
    {
        switch (target)
        {
            case TransitionTarget.Text:
                monoBehaviour.StartCoroutine(TransitionFrom(textComponent[occurrence].rectTransform, new Vector2(offset, 0), duration, delay, easing));
                break;
            case TransitionTarget.Image:
                monoBehaviour.StartCoroutine(TransitionFrom(imageComponent[occurrence].rectTransform, new Vector2(offset, 0), duration, delay, easing));
                break;
            case TransitionTarget.Panel:
                monoBehaviour.StartCoroutine(TransitionFrom(panelTransform, new Vector2(offset, 0), duration, delay, easing));
                break;
            case TransitionTarget.All:
                monoBehaviour.StartCoroutine(TransitionFrom(textComponent[occurrence].rectTransform, new Vector2(offset, 0), duration, delay, easing));
                monoBehaviour.StartCoroutine(TransitionFrom(imageComponent[occurrence].rectTransform, new Vector2(offset, 0), duration, delay, easing));
                monoBehaviour.StartCoroutine(TransitionFrom(panelTransform, new Vector2(offset, 0), duration, delay, easing));
                break;
        }
    }

    public void TransitionFromRight(TransitionTarget target, int occurrence, float offset, EasingType easing = EasingType.Linear, float duration = transitionTime, float delay = 0f)
    {
        switch (target)
        {
            case TransitionTarget.Text:
                monoBehaviour.StartCoroutine(TransitionFrom(textComponent[occurrence].rectTransform, new Vector2(-offset, 0), duration, delay, easing));
                break;
            case TransitionTarget.Image:
                monoBehaviour.StartCoroutine(TransitionFrom(imageComponent[occurrence].rectTransform, new Vector2(-offset, 0), duration, delay, easing));
                break;
            case TransitionTarget.Panel:
                monoBehaviour.StartCoroutine(TransitionFrom(panelTransform, new Vector2(-offset, 0), duration, delay, easing));
                break;
            case TransitionTarget.All:
                monoBehaviour.StartCoroutine(TransitionFrom(textComponent[occurrence].rectTransform, new Vector2(-offset, 0), duration, delay, easing));
                monoBehaviour.StartCoroutine(TransitionFrom(imageComponent[occurrence].rectTransform, new Vector2(-offset, 0), duration, delay, easing));
                monoBehaviour.StartCoroutine(TransitionFrom(panelTransform, new Vector2(-offset, 0), duration, delay, easing));
                break;
        }
    }

    public void TransitionFromPosition(TransitionTarget target, int occurrence, Vector2 offset, EasingType easing = EasingType.Linear, float duration = transitionTime, float delay = 0f)
    {
        switch (target)
        {
            case TransitionTarget.Text:
                monoBehaviour.StartCoroutine(TransitionFrom(textComponent[occurrence].rectTransform, -offset, duration, delay, easing));
                break;
            case TransitionTarget.Image:
                monoBehaviour.StartCoroutine(TransitionFrom(imageComponent[occurrence].rectTransform, -offset, duration, delay, easing));
                break;
            case TransitionTarget.Panel:
                monoBehaviour.StartCoroutine(TransitionFrom(panelTransform, -offset, duration, delay, easing));
                break;
            case TransitionTarget.All:
                monoBehaviour.StartCoroutine(TransitionFrom(textComponent[occurrence].rectTransform, -offset, duration, delay, easing));
                monoBehaviour.StartCoroutine(TransitionFrom(imageComponent[occurrence].rectTransform, -offset, duration, delay, easing));
                monoBehaviour.StartCoroutine(TransitionFrom(panelTransform, -offset, duration, delay, easing));
                break;
        }
    }

    public void TransitionToUp(TransitionTarget target, int occurrence, float offset, EasingType easing = EasingType.Linear, float duration = transitionTime, float delay = 0f)
    {
        switch (target)
        {
            case TransitionTarget.Text:
                monoBehaviour.StartCoroutine(TransitionTo(textComponent[occurrence].rectTransform, occurrence, new Vector2(0, offset), duration, delay, easing));
                break;
            case TransitionTarget.Image:
                monoBehaviour.StartCoroutine(TransitionTo(imageComponent[occurrence].rectTransform, occurrence, new Vector2(0, offset), duration, delay, easing));
                break;
            case TransitionTarget.Panel:
                monoBehaviour.StartCoroutine(TransitionTo(panelTransform, occurrence, new Vector2(0, offset), duration, delay, easing));
                break;
            case TransitionTarget.All:
                monoBehaviour.StartCoroutine(TransitionTo(textComponent[occurrence].rectTransform, occurrence, new Vector2(0, offset), duration, delay, easing));
                monoBehaviour.StartCoroutine(TransitionTo(imageComponent[occurrence].rectTransform, occurrence, new Vector2(0, offset), duration, delay, easing));
                monoBehaviour.StartCoroutine(TransitionTo(panelTransform, occurrence, new Vector2(0, offset), duration, delay, easing));
                break;
        }
    }

    public void TransitionToDown(TransitionTarget target, int occurrence, float offset, EasingType easing = EasingType.Linear, float duration = transitionTime, float delay = 0f)
    {
        switch (target)
        {
            case TransitionTarget.Text:
                monoBehaviour.StartCoroutine(TransitionTo(textComponent[occurrence].rectTransform, occurrence, new Vector2(0, -offset), duration, delay, easing));
                break;
            case TransitionTarget.Image:
                monoBehaviour.StartCoroutine(TransitionTo(imageComponent[occurrence].rectTransform, occurrence, new Vector2(0, -offset), duration, delay, easing));
                break;
            case TransitionTarget.Panel:
                monoBehaviour.StartCoroutine(TransitionTo(panelTransform, occurrence, new Vector2(0, -offset), duration, delay, easing));
                break;
            case TransitionTarget.All:
                monoBehaviour.StartCoroutine(TransitionTo(textComponent[occurrence].rectTransform, occurrence, new Vector2(0, -offset), duration, delay, easing));
                monoBehaviour.StartCoroutine(TransitionTo(imageComponent[occurrence].rectTransform, occurrence, new Vector2(0, -offset), duration, delay, easing));
                monoBehaviour.StartCoroutine(TransitionTo(panelTransform, occurrence, new Vector2(0, -offset), duration, delay, easing));
                break;
        }
    }

    public void TransitionToLeft(TransitionTarget target, int occurrence, float offset, EasingType easing = EasingType.Linear, float duration = transitionTime, float delay = 0f)
    {
        switch (target)
        {
            case TransitionTarget.Text:
                monoBehaviour.StartCoroutine(TransitionTo(textComponent[occurrence].rectTransform, occurrence, new Vector2(-offset, 0), duration, delay, easing));
                break;
            case TransitionTarget.Image:
                monoBehaviour.StartCoroutine(TransitionTo(imageComponent[occurrence].rectTransform, occurrence, new Vector2(-offset, 0), duration, delay, easing));
                break;
            case TransitionTarget.Panel:
                monoBehaviour.StartCoroutine(TransitionTo(panelTransform, occurrence, new Vector2(-offset, 0), duration, delay, easing));
                break;
            case TransitionTarget.All:
                monoBehaviour.StartCoroutine(TransitionTo(textComponent[occurrence].rectTransform, occurrence, new Vector2(-offset, 0), duration, delay, easing));
                monoBehaviour.StartCoroutine(TransitionTo(imageComponent[occurrence].rectTransform, occurrence, new Vector2(-offset, 0), duration, delay, easing));
                monoBehaviour.StartCoroutine(TransitionTo(panelTransform, occurrence, new Vector2(-offset, 0), duration, delay, easing));
                break;
        }
    }

    public void TransitionToRight(TransitionTarget target, int occurrence, float offset, EasingType easing = EasingType.Linear, float duration = transitionTime, float delay = 0f)
    {
        switch (target)
        {
            case TransitionTarget.Text:
                monoBehaviour.StartCoroutine(TransitionTo(textComponent[occurrence].rectTransform, occurrence, new Vector2(offset, 0), duration, delay, easing));
                break;
            case TransitionTarget.Image:
                monoBehaviour.StartCoroutine(TransitionTo(imageComponent[occurrence].rectTransform, occurrence, new Vector2(offset, 0), duration, delay, easing));
                break;
            case TransitionTarget.Panel:
                monoBehaviour.StartCoroutine(TransitionTo(panelTransform, occurrence, new Vector2(offset, 0), duration, delay, easing));
                break;
            case TransitionTarget.All:
                monoBehaviour.StartCoroutine(TransitionTo(textComponent[occurrence].rectTransform, occurrence, new Vector2(offset, 0), duration, delay, easing));
                monoBehaviour.StartCoroutine(TransitionTo(imageComponent[occurrence].rectTransform, occurrence, new Vector2(offset, 0), duration, delay, easing));
                monoBehaviour.StartCoroutine(TransitionTo(panelTransform, occurrence, new Vector2(offset, 0), duration, delay, easing));
                break;
        }
    }

    public void TransitionToPosition(TransitionTarget target, int occurrence, Vector2 offset, EasingType easing = EasingType.Linear, float duration = transitionTime, float delay = 0f)
    {
        switch (target)
        {
            case TransitionTarget.Text:
                monoBehaviour.StartCoroutine(TransitionTo(textComponent[occurrence].rectTransform, occurrence, offset, duration, delay, easing));
                break;
            case TransitionTarget.Image:
                monoBehaviour.StartCoroutine(TransitionTo(imageComponent[occurrence].rectTransform, occurrence, offset, duration, delay, easing));
                break;
            case TransitionTarget.Panel:
                monoBehaviour.StartCoroutine(TransitionTo(panelTransform, occurrence, offset, duration, delay, easing));
                break;
            case TransitionTarget.All:
                monoBehaviour.StartCoroutine(TransitionTo(textComponent[occurrence].rectTransform, occurrence, offset, duration, delay, easing));
                monoBehaviour.StartCoroutine(TransitionTo(imageComponent[occurrence].rectTransform, occurrence, offset, duration, delay, easing));
                monoBehaviour.StartCoroutine(TransitionTo(panelTransform, occurrence, offset, duration, delay, easing));
                break;
        }
    }

    // ----------------------------------------------------- FROM TRANSITION -----------------------------------------------------

    private IEnumerator TransitionFrom(RectTransform component, Vector2 offset, float duration, float delay, EasingType easing)
    {
        if (component == null) { yield break; }

        float elapsedTime = 0f;
        Vector2 startPos, targetPos;

        targetPos = component.anchoredPosition;
        startPos = targetPos - offset;
        component.anchoredPosition = startPos;
        if (delay > 0) { yield return new WaitForSeconds(delay); }

        while (elapsedTime < duration)
        {
            float time = elapsedTime / duration;
            float easedTime = Easing.SetEasingFunction(time, easing);

            component.anchoredPosition = Vector2.Lerp(startPos, targetPos, easedTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        component.anchoredPosition = targetPos;
    }

    // ----------------------------------------------------- TO TRANSITION -----------------------------------------------------

    private IEnumerator TransitionTo(RectTransform component, int occurrence, Vector2 offset, float duration, float delay, EasingType easing)
    {
        if (component == null) { yield break; }

        Vector2 startPos = Vector2.zero;
        Vector2 targetPos;

        if (component == textComponent[occurrence].rectTransform)
        {
            if (!storedPosition[textIndex][occurrence]) 
            {
                startPos = textComponent[occurrence].rectTransform.anchoredPosition;
                originalPosition[textIndex][occurrence] = startPos;
                storedPosition[textIndex][occurrence] = true; 
            }
            else 
            {
                startPos = originalPosition[textIndex][occurrence];
            }
        }
        else if (component == imageComponent[occurrence].rectTransform)
        {
            if (!storedPosition[imageIndex][occurrence]) 
            {
                startPos = imageComponent[occurrence].rectTransform.anchoredPosition;
                originalPosition[imageIndex][occurrence] = startPos;
                storedPosition[imageIndex][occurrence] = true;
            }
            else 
            {
                startPos = originalPosition[imageIndex][occurrence];
            }
        }
        else if (component == panelTransform)
        {
            if (!storedPosition[panelIndex][0]) 
            {
                startPos = panelTransform.anchoredPosition;
                originalPosition[panelIndex][0] = startPos;
                storedPosition[panelIndex][0] = true;
            }
            else 
            {
                startPos = originalPosition[panelIndex][0];
            }
        }

        float elapsedTime = 0f;
        targetPos = startPos + offset;
        if (delay > 0) { yield return new WaitForSeconds(delay); }

        while (elapsedTime < duration)
        {
            float time = elapsedTime / duration;
            float easedTime = Easing.SetEasingFunction(time, easing);

            component.anchoredPosition = Vector2.Lerp(startPos, targetPos, easedTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        component.anchoredPosition = targetPos;
    }


}
