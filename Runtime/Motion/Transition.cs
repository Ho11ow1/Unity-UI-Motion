using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    private readonly Vector2[][] originalPosition = new Vector2[3][] { new Vector2[10], new Vector2[10], new Vector2[10] };
    private readonly bool[][] storedPosition = new bool[3][] { new bool[10], new bool[10], new bool[10] };

    private const float transitionTime = 0.5f;

    public Transition(TextMeshProUGUI[] text, Image[] image, RectTransform panel, MonoBehaviour runner)
    {
        textComponent = text;
        imageComponent = image;
        panelTransform = panel;
        monoBehaviour = runner;
    }

    // ----------------------------------------------------- PUBLIC API -----------------------------------------------------

    public void TransitionFromUp(Motion.TransitionTarget target, int occurrence, float offset, Motion.EasingType easing = Motion.EasingType.Linear, float duration = transitionTime, float delay = 0f)
    {
        switch (target)
        {
            case Motion.TransitionTarget.Text:
                monoBehaviour.StartCoroutine(TransitionFrom(textComponent[occurrence].rectTransform, new Vector2(0, -offset), duration, delay, easing));
                break;
            case Motion.TransitionTarget.Image:
                monoBehaviour.StartCoroutine(TransitionFrom(imageComponent[occurrence].rectTransform, new Vector2(0, -offset), duration, delay, easing));
                break;
            case Motion.TransitionTarget.Panel:
                monoBehaviour.StartCoroutine(TransitionFrom(panelTransform, new Vector2(0, -offset), duration, delay, easing));
                break;
            case Motion.TransitionTarget.All:
                monoBehaviour.StartCoroutine(TransitionFrom(textComponent[occurrence].rectTransform, new Vector2(0, -offset), duration, delay, easing));
                monoBehaviour.StartCoroutine(TransitionFrom(imageComponent[occurrence].rectTransform, new Vector2(0, -offset), duration, delay, easing));
                monoBehaviour.StartCoroutine(TransitionFrom(panelTransform, new Vector2(0, -offset), duration, delay, easing));
                break;
        }
    }

    public void TransitionFromDown(Motion.TransitionTarget target, int occurrence, float offset, Motion.EasingType easing = Motion.EasingType.Linear, float duration = transitionTime, float delay = 0f)
    {
        switch (target)
        {
            case Motion.TransitionTarget.Text:
                monoBehaviour.StartCoroutine(TransitionFrom(textComponent[occurrence].rectTransform, new Vector2(0, offset), duration, delay, easing));
                break;
            case Motion.TransitionTarget.Image:
                monoBehaviour.StartCoroutine(TransitionFrom(imageComponent[occurrence].rectTransform, new Vector2(0, offset), duration, delay, easing));
                break;
            case Motion.TransitionTarget.Panel:
                monoBehaviour.StartCoroutine(TransitionFrom(panelTransform, new Vector2(0, -offset), duration, delay, easing));
                break;
            case Motion.TransitionTarget.All:
                monoBehaviour.StartCoroutine(TransitionFrom(textComponent[occurrence].rectTransform, new Vector2(0, offset), duration, delay, easing));
                monoBehaviour.StartCoroutine(TransitionFrom(imageComponent[occurrence].rectTransform, new Vector2(0, offset), duration, delay, easing));
                monoBehaviour.StartCoroutine(TransitionFrom(panelTransform, new Vector2(0, offset), duration, delay, easing));
                break;
        }
    }

    public void TransitionFromLeft(Motion.TransitionTarget target, int occurrence, float offset, Motion.EasingType easing = Motion.EasingType.Linear, float duration = transitionTime, float delay = 0f)
    {
        switch (target)
        {
            case Motion.TransitionTarget.Text:
                monoBehaviour.StartCoroutine(TransitionFrom(textComponent[occurrence].rectTransform, new Vector2(offset, 0), duration, delay, easing));
                break;
            case Motion.TransitionTarget.Image:
                monoBehaviour.StartCoroutine(TransitionFrom(imageComponent[occurrence].rectTransform, new Vector2(offset, 0), duration, delay, easing));
                break;
            case Motion.TransitionTarget.Panel:
                monoBehaviour.StartCoroutine(TransitionFrom(panelTransform, new Vector2(offset, 0), duration, delay, easing));
                break;
            case Motion.TransitionTarget.All:
                monoBehaviour.StartCoroutine(TransitionFrom(textComponent[occurrence].rectTransform, new Vector2(offset, 0), duration, delay, easing));
                monoBehaviour.StartCoroutine(TransitionFrom(imageComponent[occurrence].rectTransform, new Vector2(offset, 0), duration, delay, easing));
                monoBehaviour.StartCoroutine(TransitionFrom(panelTransform, new Vector2(offset, 0), duration, delay, easing));
                break;
        }
    }

    public void TransitionFromRight(Motion.TransitionTarget target, int occurrence, float offset, Motion.EasingType easing = Motion.EasingType.Linear, float duration = transitionTime, float delay = 0f)
    {
        switch (target)
        {
            case Motion.TransitionTarget.Text:
                monoBehaviour.StartCoroutine(TransitionFrom(textComponent[occurrence].rectTransform, new Vector2(-offset, 0), duration, delay, easing));
                break;
            case Motion.TransitionTarget.Image:
                monoBehaviour.StartCoroutine(TransitionFrom(imageComponent[occurrence].rectTransform, new Vector2(-offset, 0), duration, delay, easing));
                break;
            case Motion.TransitionTarget.Panel:
                monoBehaviour.StartCoroutine(TransitionFrom(panelTransform, new Vector2(-offset, 0), duration, delay, easing));
                break;
            case Motion.TransitionTarget.All:
                monoBehaviour.StartCoroutine(TransitionFrom(textComponent[occurrence].rectTransform, new Vector2(-offset, 0), duration, delay, easing));
                monoBehaviour.StartCoroutine(TransitionFrom(imageComponent[occurrence].rectTransform, new Vector2(-offset, 0), duration, delay, easing));
                monoBehaviour.StartCoroutine(TransitionFrom(panelTransform, new Vector2(-offset, 0), duration, delay, easing));
                break;
        }
    }

    public void TransitionFromPosition(Motion.TransitionTarget target, int occurrence, Vector2 offset, Motion.EasingType easing = Motion.EasingType.Linear, float duration = transitionTime, float delay = 0f)
    {
        switch (target)
        {
            case Motion.TransitionTarget.Text:
                monoBehaviour.StartCoroutine(TransitionFrom(textComponent[occurrence].rectTransform, -offset, duration, delay, easing));
                break;
            case Motion.TransitionTarget.Image:
                monoBehaviour.StartCoroutine(TransitionFrom(imageComponent[occurrence].rectTransform, -offset, duration, delay, easing));
                break;
            case Motion.TransitionTarget.Panel:
                monoBehaviour.StartCoroutine(TransitionFrom(panelTransform, -offset, duration, delay, easing));
                break;
            case Motion.TransitionTarget.All:
                monoBehaviour.StartCoroutine(TransitionFrom(textComponent[occurrence].rectTransform, -offset, duration, delay, easing));
                monoBehaviour.StartCoroutine(TransitionFrom(imageComponent[occurrence].rectTransform, -offset, duration, delay, easing));
                monoBehaviour.StartCoroutine(TransitionFrom(panelTransform, -offset, duration, delay, easing));
                break;
        }
    }

    public void TransitionToUp(Motion.TransitionTarget target, int occurrence, float offset, Motion.EasingType easing = Motion.EasingType.Linear, float duration = transitionTime, float delay = 0f)
    {
        switch (target)
        {
            case Motion.TransitionTarget.Text:
                monoBehaviour.StartCoroutine(TransitionTo(textComponent[occurrence].rectTransform, occurrence, new Vector2(0, offset), duration, delay, easing));
                break;
            case Motion.TransitionTarget.Image:
                monoBehaviour.StartCoroutine(TransitionTo(imageComponent[occurrence].rectTransform, occurrence, new Vector2(0, offset), duration, delay, easing));
                break;
            case Motion.TransitionTarget.Panel:
                monoBehaviour.StartCoroutine(TransitionTo(panelTransform, occurrence, new Vector2(0, offset), duration, delay, easing));
                break;
            case Motion.TransitionTarget.All:
                monoBehaviour.StartCoroutine(TransitionTo(textComponent[occurrence].rectTransform, occurrence, new Vector2(0, offset), duration, delay, easing));
                monoBehaviour.StartCoroutine(TransitionTo(imageComponent[occurrence].rectTransform, occurrence, new Vector2(0, offset), duration, delay, easing));
                monoBehaviour.StartCoroutine(TransitionTo(panelTransform, occurrence, new Vector2(0, offset), duration, delay, easing));
                break;
        }
    }

    public void TransitionToDown(Motion.TransitionTarget target, int occurrence, float offset, Motion.EasingType easing = Motion.EasingType.Linear, float duration = transitionTime, float delay = 0f)
    {
        switch (target)
        {
            case Motion.TransitionTarget.Text:
                monoBehaviour.StartCoroutine(TransitionTo(textComponent[occurrence].rectTransform, occurrence, new Vector2(0, -offset), duration, delay, easing));
                break;
            case Motion.TransitionTarget.Image:
                monoBehaviour.StartCoroutine(TransitionTo(imageComponent[occurrence].rectTransform, occurrence, new Vector2(0, -offset), duration, delay, easing));
                break;
            case Motion.TransitionTarget.Panel:
                monoBehaviour.StartCoroutine(TransitionTo(panelTransform, occurrence, new Vector2(0, -offset), duration, delay, easing));
                break;
            case Motion.TransitionTarget.All:
                monoBehaviour.StartCoroutine(TransitionTo(textComponent[occurrence].rectTransform, occurrence, new Vector2(0, -offset), duration, delay, easing));
                monoBehaviour.StartCoroutine(TransitionTo(imageComponent[occurrence].rectTransform, occurrence, new Vector2(0, -offset), duration, delay, easing));
                monoBehaviour.StartCoroutine(TransitionTo(panelTransform, occurrence, new Vector2(0, -offset), duration, delay, easing));
                break;
        }
    }

    public void TransitionToLeft(Motion.TransitionTarget target, int occurrence, float offset, Motion.EasingType easing = Motion.EasingType.Linear, float duration = transitionTime, float delay = 0f)
    {
        switch (target)
        {
            case Motion.TransitionTarget.Text:
                monoBehaviour.StartCoroutine(TransitionTo(textComponent[occurrence].rectTransform, occurrence, new Vector2(-offset, 0), duration, delay, easing));
                break;
            case Motion.TransitionTarget.Image:
                monoBehaviour.StartCoroutine(TransitionTo(imageComponent[occurrence].rectTransform, occurrence, new Vector2(-offset, 0), duration, delay, easing));
                break;
            case Motion.TransitionTarget.Panel:
                monoBehaviour.StartCoroutine(TransitionTo(panelTransform, occurrence, new Vector2(-offset, 0), duration, delay, easing));
                break;
            case Motion.TransitionTarget.All:
                monoBehaviour.StartCoroutine(TransitionTo(textComponent[occurrence].rectTransform, occurrence, new Vector2(-offset, 0), duration, delay, easing));
                monoBehaviour.StartCoroutine(TransitionTo(imageComponent[occurrence].rectTransform, occurrence, new Vector2(-offset, 0), duration, delay, easing));
                monoBehaviour.StartCoroutine(TransitionTo(panelTransform, occurrence, new Vector2(-offset, 0), duration, delay, easing));
                break;
        }
    }

    public void TransitionToRight(Motion.TransitionTarget target, int occurrence, float offset, Motion.EasingType easing = Motion.EasingType.Linear, float duration = transitionTime, float delay = 0f)
    {
        switch (target)
        {
            case Motion.TransitionTarget.Text:
                monoBehaviour.StartCoroutine(TransitionTo(textComponent[occurrence].rectTransform, occurrence, new Vector2(offset, 0), duration, delay, easing));
                break;
            case Motion.TransitionTarget.Image:
                monoBehaviour.StartCoroutine(TransitionTo(imageComponent[occurrence].rectTransform, occurrence, new Vector2(offset, 0), duration, delay, easing));
                break;
            case Motion.TransitionTarget.Panel:
                monoBehaviour.StartCoroutine(TransitionTo(panelTransform, occurrence, new Vector2(offset, 0), duration, delay, easing));
                break;
            case Motion.TransitionTarget.All:
                monoBehaviour.StartCoroutine(TransitionTo(textComponent[occurrence].rectTransform, occurrence, new Vector2(offset, 0), duration, delay, easing));
                monoBehaviour.StartCoroutine(TransitionTo(imageComponent[occurrence].rectTransform, occurrence, new Vector2(offset, 0), duration, delay, easing));
                monoBehaviour.StartCoroutine(TransitionTo(panelTransform, occurrence, new Vector2(offset, 0), duration, delay, easing));
                break;
        }
    }

    public void TransitionToPosition(Motion.TransitionTarget target, int occurrence, Vector2 offset, Motion.EasingType easing = Motion.EasingType.Linear, float duration = transitionTime, float delay = 0f)
    {
        switch (target)
        {
            case Motion.TransitionTarget.Text:
                monoBehaviour.StartCoroutine(TransitionTo(textComponent[occurrence].rectTransform, occurrence, offset, duration, delay, easing));
                break;
            case Motion.TransitionTarget.Image:
                monoBehaviour.StartCoroutine(TransitionTo(imageComponent[occurrence].rectTransform, occurrence, offset, duration, delay, easing));
                break;
            case Motion.TransitionTarget.Panel:
                monoBehaviour.StartCoroutine(TransitionTo(panelTransform, occurrence, offset, duration, delay, easing));
                break;
            case Motion.TransitionTarget.All:
                monoBehaviour.StartCoroutine(TransitionTo(textComponent[occurrence].rectTransform, occurrence, offset, duration, delay, easing));
                monoBehaviour.StartCoroutine(TransitionTo(imageComponent[occurrence].rectTransform, occurrence, offset, duration, delay, easing));
                monoBehaviour.StartCoroutine(TransitionTo(panelTransform, occurrence, offset, duration, delay, easing));
                break;
        }
    }

    // ----------------------------------------------------- FROM TRANSITION -----------------------------------------------------

    private IEnumerator TransitionFrom(RectTransform component, Vector2 offset, float duration, float delay, Motion.EasingType easing)
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

    private IEnumerator TransitionTo(RectTransform component, int occurrence, Vector2 offset, float duration, float delay, Motion.EasingType easing)
    {
        if (component == null) { yield break; }

        Vector2 startPos = Vector2.zero;
        Vector2 targetPos;

        if (component == textComponent[occurrence].rectTransform)
        {
            if (!storedPosition[0][occurrence]) 
            {
                startPos = textComponent[occurrence].rectTransform.anchoredPosition;
                originalPosition[0][occurrence] = startPos;
                storedPosition[0][occurrence] = true; 
            }
            else 
            {
                startPos = originalPosition[0][occurrence];
            }
        }
        else if (component == imageComponent[occurrence].rectTransform)
        {
            if (!storedPosition[1][occurrence]) 
            {
                startPos = imageComponent[occurrence].rectTransform.anchoredPosition;
                originalPosition[1][occurrence] = startPos;
                storedPosition[1][occurrence] = true;
            }
            else 
            {
                startPos = originalPosition[1][occurrence];
            }
        }
        else if (component == panelTransform)
        {
            if (!storedPosition[2][0]) 
            {
                startPos = panelTransform.anchoredPosition;
                originalPosition[2][0] = startPos;
                storedPosition[2][0] = true;
            }
            else 
            {
                startPos = originalPosition[2][0];
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
