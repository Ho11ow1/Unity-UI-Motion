using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

using static Motion;

/* --------------------------------------------------------
 * Unity UI Motion - Position Animation Component
 * Created by Hollow1
 * 
 * Applies a position transition to a UI component
 * 
 * Version: 2.2.1
 * GitHub: https://github.com/Hollow1/Unity-UI-Motion
 * -------------------------------------------------------- */

#pragma warning disable IDE0090 // Use 'new'
[AddComponentMenu("")]
internal class Transition
{
    private readonly TextMeshProUGUI[] textComponent;
    private readonly Image[] imageComponent;
    private readonly Button[] buttonComponent;
    private readonly RectTransform panelTransform;
    private readonly MonoBehaviour monoBehaviour;

    private readonly List<Utils.AutoIncreaseList<Vector2>> originalPosition = new List<Utils.AutoIncreaseList<Vector2>>()
    {
        new Utils.AutoIncreaseList<Vector2>(),
        new Utils.AutoIncreaseList<Vector2>(),
        new Utils.AutoIncreaseList<Vector2>(),
        new Utils.AutoIncreaseList<Vector2>()
    };

    private readonly List<Utils.AutoIncreaseList<bool>> storedPosition = new List<Utils.AutoIncreaseList<bool>>()
    {
        new Utils.AutoIncreaseList<bool>(),
        new Utils.AutoIncreaseList<bool>(),
        new Utils.AutoIncreaseList<bool>(),
        new Utils.AutoIncreaseList<bool>()
    };

    private const float transitionTime = 0.5f;

    public Transition(TextMeshProUGUI[] text, Image[] image, Button[] button, RectTransform panel, MonoBehaviour runner)
    {
        textComponent = text;
        imageComponent = image;
        buttonComponent = button;
        panelTransform = panel;
        monoBehaviour = runner;
    }

    // ----------------------------------------------------- PUBLIC API -----------------------------------------------------

    public void TransitionFromUp(AnimationTarget target, int occurrence, float offset, EasingType easing = EasingType.Linear, float duration = transitionTime, float delay = 0f, UnityAction transitionStart = null, UnityAction transitionEnd = null)
    {
        switch (target)
        {
            case AnimationTarget.Panel:
                monoBehaviour.StartCoroutine(TransitionFrom(panelTransform, new Vector2(0, -offset), duration, delay, easing, transitionStart, transitionEnd));
                break;
            case AnimationTarget.Text:
                monoBehaviour.StartCoroutine(TransitionFrom(textComponent[occurrence].rectTransform, new Vector2(0, -offset), duration, delay, easing, transitionStart, transitionEnd));
                break;
            case AnimationTarget.Image:
                monoBehaviour.StartCoroutine(TransitionFrom(imageComponent[occurrence].rectTransform, new Vector2(0, -offset), duration, delay, easing, transitionStart, transitionEnd));
                break;
            case AnimationTarget.Button:
                monoBehaviour.StartCoroutine(TransitionFrom((RectTransform)buttonComponent[occurrence].transform, new Vector2(0, -offset), duration, delay, easing, transitionStart, transitionEnd));
                break;
        }
    }

    public void TransitionFromDown(AnimationTarget target, int occurrence, float offset, EasingType easing = EasingType.Linear, float duration = transitionTime, float delay = 0f, UnityAction transitionStart = null, UnityAction transitionEnd = null)
    {
        switch (target)
        {
            case AnimationTarget.Panel:
                monoBehaviour.StartCoroutine(TransitionFrom(panelTransform, new Vector2(0, -offset), duration, delay, easing, transitionStart, transitionEnd));
                break;
            case AnimationTarget.Text:
                monoBehaviour.StartCoroutine(TransitionFrom(textComponent[occurrence].rectTransform, new Vector2(0, offset), duration, delay, easing, transitionStart, transitionEnd));
                break;
            case AnimationTarget.Image:
                monoBehaviour.StartCoroutine(TransitionFrom(imageComponent[occurrence].rectTransform, new Vector2(0, offset), duration, delay, easing, transitionStart, transitionEnd));
                break;
            case AnimationTarget.Button:
                monoBehaviour.StartCoroutine(TransitionFrom((RectTransform)buttonComponent[occurrence].transform, new Vector2(0, offset), duration, delay, easing, transitionStart, transitionEnd));
                break;
        }
    }

    public void TransitionFromLeft(AnimationTarget target, int occurrence, float offset, EasingType easing = EasingType.Linear, float duration = transitionTime, float delay = 0f, UnityAction transitionStart = null, UnityAction transitionEnd = null)
    {    
        switch (target)
        {
            case AnimationTarget.Panel:
                monoBehaviour.StartCoroutine(TransitionFrom(panelTransform, new Vector2(offset, 0), duration, delay, easing, transitionStart, transitionEnd));
                break;
            case AnimationTarget.Text:
                monoBehaviour.StartCoroutine(TransitionFrom(textComponent[occurrence].rectTransform, new Vector2(offset, 0), duration, delay, easing, transitionStart, transitionEnd));
                break;
            case AnimationTarget.Image:
                monoBehaviour.StartCoroutine(TransitionFrom(imageComponent[occurrence].rectTransform, new Vector2(offset, 0), duration, delay, easing, transitionStart, transitionEnd));
                break;
            case AnimationTarget.Button:
                monoBehaviour.StartCoroutine(TransitionFrom((RectTransform)buttonComponent[occurrence].transform, new Vector2(offset, 0), duration, delay, easing, transitionStart, transitionEnd));
                break;
        }
    }

    public void TransitionFromRight(AnimationTarget target, int occurrence, float offset, EasingType easing = EasingType.Linear, float duration = transitionTime, float delay = 0f, UnityAction transitionStart = null, UnityAction transitionEnd = null)
    {
        switch (target)
        {
            case AnimationTarget.Panel:
                monoBehaviour.StartCoroutine(TransitionFrom(panelTransform, new Vector2(-offset, 0), duration, delay, easing, transitionStart, transitionEnd));
                break;
            case AnimationTarget.Text:
                monoBehaviour.StartCoroutine(TransitionFrom(textComponent[occurrence].rectTransform, new Vector2(-offset, 0), duration, delay, easing, transitionStart, transitionEnd));
                break;
            case AnimationTarget.Image:
                monoBehaviour.StartCoroutine(TransitionFrom(imageComponent[occurrence].rectTransform, new Vector2(-offset, 0), duration, delay, easing, transitionStart, transitionEnd));
                break;
            case AnimationTarget.Button:
                monoBehaviour.StartCoroutine(TransitionFrom((RectTransform)buttonComponent[occurrence].transform, new Vector2(-offset, 0), duration, delay, easing, transitionStart, transitionEnd));
                break;
        }
    }

    public void TransitionFromPosition(AnimationTarget target, int occurrence, Vector2 offset, EasingType easing = EasingType.Linear, float duration = transitionTime, float delay = 0f, UnityAction transitionStart = null, UnityAction transitionEnd = null)
    {
        switch (target)
        {
            case AnimationTarget.Panel:
                monoBehaviour.StartCoroutine(TransitionFrom(panelTransform, -offset, duration, delay, easing, transitionStart, transitionEnd));
                break;
            case AnimationTarget.Text:
                monoBehaviour.StartCoroutine(TransitionFrom(textComponent[occurrence].rectTransform, -offset, duration, delay, easing, transitionStart, transitionEnd));
                break;
            case AnimationTarget.Image:
                monoBehaviour.StartCoroutine(TransitionFrom(imageComponent[occurrence].rectTransform, -offset, duration, delay, easing, transitionStart, transitionEnd));
                break;
            case AnimationTarget.Button:
                monoBehaviour.StartCoroutine(TransitionFrom((RectTransform)buttonComponent[occurrence].transform, -offset, duration, delay, easing, transitionStart, transitionEnd));
                break;
        }
    }

    public void TransitionToUp(AnimationTarget target, int occurrence, float offset, EasingType easing = EasingType.Linear, float duration = transitionTime, float delay = 0f, UnityAction transitionStart = null, UnityAction transitionEnd = null)
    {
        switch (target)
        {
            case AnimationTarget.Panel:
                monoBehaviour.StartCoroutine(TransitionTo(panelTransform, occurrence, new Vector2(0, offset), duration, delay, easing, transitionStart, transitionEnd));
                break;
            case AnimationTarget.Text:
                monoBehaviour.StartCoroutine(TransitionTo(textComponent[occurrence].rectTransform, occurrence, new Vector2(0, offset), duration, delay, easing, transitionStart, transitionEnd));
                break;
            case AnimationTarget.Image:
                monoBehaviour.StartCoroutine(TransitionTo(imageComponent[occurrence].rectTransform, occurrence, new Vector2(0, offset), duration, delay, easing, transitionStart, transitionEnd));
                break;
            case AnimationTarget.Button:
                monoBehaviour.StartCoroutine(TransitionTo((RectTransform)buttonComponent[occurrence].transform, occurrence, new Vector2(0, offset), duration, delay, easing, transitionStart, transitionEnd));
                break;
        }
    }

    public void TransitionToDown(AnimationTarget target, int occurrence, float offset, EasingType easing = EasingType.Linear, float duration = transitionTime, float delay = 0f, UnityAction transitionStart = null, UnityAction transitionEnd = null)
    {
        switch (target)
        {
            case AnimationTarget.Panel:
                monoBehaviour.StartCoroutine(TransitionTo(panelTransform, occurrence, new Vector2(0, -offset), duration, delay, easing, transitionStart, transitionEnd));
                break;
            case AnimationTarget.Text:
                monoBehaviour.StartCoroutine(TransitionTo(textComponent[occurrence].rectTransform, occurrence, new Vector2(0, -offset), duration, delay, easing, transitionStart, transitionEnd));
                break;
            case AnimationTarget.Image:
                monoBehaviour.StartCoroutine(TransitionTo(imageComponent[occurrence].rectTransform, occurrence, new Vector2(0, -offset), duration, delay, easing, transitionStart, transitionEnd));
                break;
            case AnimationTarget.Button:
                monoBehaviour.StartCoroutine(TransitionTo((RectTransform)buttonComponent[occurrence].transform, occurrence, new Vector2(0, -offset), duration, delay, easing, transitionStart, transitionEnd));
                break;
        }
    }

    public void TransitionToLeft(AnimationTarget target, int occurrence, float offset, EasingType easing = EasingType.Linear, float duration = transitionTime, float delay = 0f, UnityAction transitionStart = null, UnityAction transitionEnd = null)
    {
        switch (target)
        {
            case AnimationTarget.Panel:
                monoBehaviour.StartCoroutine(TransitionTo(panelTransform, occurrence, new Vector2(-offset, 0), duration, delay, easing, transitionStart, transitionEnd));
                break;
            case AnimationTarget.Text:
                monoBehaviour.StartCoroutine(TransitionTo(textComponent[occurrence].rectTransform, occurrence, new Vector2(-offset, 0), duration, delay, easing, transitionStart, transitionEnd));
                break;
            case AnimationTarget.Image:
                monoBehaviour.StartCoroutine(TransitionTo(imageComponent[occurrence].rectTransform, occurrence, new Vector2(-offset, 0), duration, delay, easing, transitionStart, transitionEnd));
                break;
            case AnimationTarget.Button:
                monoBehaviour.StartCoroutine(TransitionTo((RectTransform)buttonComponent[occurrence].transform, occurrence, new Vector2(-offset, 0), duration, delay, easing, transitionStart, transitionEnd));
                break;
        }
    }

    public void TransitionToRight(AnimationTarget target, int occurrence, float offset, EasingType easing = EasingType.Linear, float duration = transitionTime, float delay = 0f, UnityAction transitionStart = null, UnityAction transitionEnd = null)
    {
        switch (target)
        {
            case AnimationTarget.Panel:
                monoBehaviour.StartCoroutine(TransitionTo(panelTransform, occurrence, new Vector2(offset, 0), duration, delay, easing, transitionStart, transitionEnd));
                break;
            case AnimationTarget.Text:
                monoBehaviour.StartCoroutine(TransitionTo(textComponent[occurrence].rectTransform, occurrence, new Vector2(offset, 0), duration, delay, easing, transitionStart, transitionEnd));
                break;
            case AnimationTarget.Image:
                monoBehaviour.StartCoroutine(TransitionTo(imageComponent[occurrence].rectTransform, occurrence, new Vector2(offset, 0), duration, delay, easing, transitionStart, transitionEnd));
                break;
            case AnimationTarget.Button:
                monoBehaviour.StartCoroutine(TransitionTo((RectTransform)buttonComponent[occurrence].transform, occurrence, new Vector2(offset, 0), duration, delay, easing, transitionStart, transitionEnd));
                break;
        }
    }

    public void TransitionToPosition(AnimationTarget target, int occurrence, Vector2 offset, EasingType easing = EasingType.Linear, float duration = transitionTime, float delay = 0f, UnityAction transitionStart = null, UnityAction transitionEnd = null)
    {
        switch (target)
        {
            case AnimationTarget.Panel:
                monoBehaviour.StartCoroutine(TransitionTo(panelTransform, occurrence, offset, duration, delay, easing, transitionStart, transitionEnd));
                break;
            case AnimationTarget.Text:
                monoBehaviour.StartCoroutine(TransitionTo(textComponent[occurrence].rectTransform, occurrence, offset, duration, delay, easing, transitionStart, transitionEnd));
                break;
            case AnimationTarget.Image:
                monoBehaviour.StartCoroutine(TransitionTo(imageComponent[occurrence].rectTransform, occurrence, offset, duration, delay, easing, transitionStart, transitionEnd));
                break;
            case AnimationTarget.Button:
                monoBehaviour.StartCoroutine(TransitionTo((RectTransform)buttonComponent[occurrence].transform, occurrence, offset, duration, delay, easing, transitionStart, transitionEnd));
                break;
        }
    }

    // ----------------------------------------------------- FROM TRANSITION -----------------------------------------------------

    private IEnumerator TransitionFrom(RectTransform component, Vector2 offset, float duration, float delay, EasingType easing, UnityAction transitionStart, UnityAction transitionEnd)
    {
        if (component == null) { yield break; }

        transitionStart?.Invoke();
        float elapsedTime = 0f;
        Vector2 startPos, targetPos;

        targetPos = component.anchoredPosition;
        startPos = targetPos - offset;
        component.anchoredPosition = startPos;
        if (delay > 0) { yield return new WaitForSeconds(delay); }

        while (elapsedTime < duration)
        {
            float time = elapsedTime / duration;
            float easedTime = Utils.Easing.SetEasingFunction(time, easing);

            component.anchoredPosition = Vector2.Lerp(startPos, targetPos, easedTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        component.anchoredPosition = targetPos;
        transitionEnd?.Invoke();
    }

    // ----------------------------------------------------- TO TRANSITION -----------------------------------------------------

    private IEnumerator TransitionTo(RectTransform component, int occurrence, Vector2 offset, float duration, float delay, EasingType easing, UnityAction transitionStart, UnityAction transitionEnd)
    {
        if (component == null) { yield break; }

        Vector2 startPos = Vector2.zero;
        Vector2 targetPos;

        if (component == panelTransform)
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
        else if (component == textComponent[occurrence].rectTransform)
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
        else if (component == (RectTransform)buttonComponent[occurrence].transform)
        {
            if (!storedPosition[buttonIndex][occurrence])
            {
                startPos = ((RectTransform)buttonComponent[occurrence].transform).anchoredPosition;
                originalPosition[buttonIndex][occurrence] = startPos;
                storedPosition[buttonIndex][occurrence] = true;
            }
            else
            {
                startPos = originalPosition[buttonIndex][occurrence];
            }
        }

        transitionStart?.Invoke();
        float elapsedTime = 0f;
        targetPos = startPos + offset;
        if (delay > 0) { yield return new WaitForSeconds(delay); }

        while (elapsedTime < duration)
        {
            float time = elapsedTime / duration;
            float easedTime = Utils.Easing.SetEasingFunction(time, easing);

            component.anchoredPosition = Vector2.Lerp(startPos, targetPos, easedTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        component.anchoredPosition = targetPos;
        transitionEnd?.Invoke();
    }


}
