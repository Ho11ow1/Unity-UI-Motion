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
 * Version: 2.3.1
 * GitHub: https://github.com/Hollow1/Unity-UI-Motion
 * -------------------------------------------------------- */

#pragma warning disable IDE0090 // Use 'new'
[AddComponentMenu("")]
internal class Transition
{
    private readonly TextMeshProUGUI[] _textComponent;
    private readonly Image[] _imageComponent;
    private readonly Button[] _buttonComponent;
    private readonly RectTransform _panelTransform;
    private readonly MonoBehaviour _monoBehaviour;

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

    public Transition(TextMeshProUGUI[] text, Image[] image, Button[] button, RectTransform panel, MonoBehaviour runner)
    {
        _textComponent = text;
        _imageComponent = image;
        _buttonComponent = button;
        _panelTransform = panel;
        _monoBehaviour = runner;
    }

    // ----------------------------------------------------- PUBLIC API -----------------------------------------------------

    public void TransitionFromUp(AnimationTarget target, int occurrence, float offset, EasingType easing = EasingType.Linear, float duration = defaultDuration, float delay = 0f, UnityAction transitionStart = null, UnityAction transitionEnd = null)
    {
        switch (target)
        {
            case AnimationTarget.Panel:
                _monoBehaviour.StartCoroutine(TransitionFrom(_panelTransform, new Vector2(0, -offset), duration, delay, easing, transitionStart, transitionEnd));
                break;
            case AnimationTarget.Text:
                _monoBehaviour.StartCoroutine(TransitionFrom(_textComponent[occurrence].rectTransform, new Vector2(0, -offset), duration, delay, easing, transitionStart, transitionEnd));
                break;
            case AnimationTarget.Image:
                _monoBehaviour.StartCoroutine(TransitionFrom(_imageComponent[occurrence].rectTransform, new Vector2(0, -offset), duration, delay, easing, transitionStart, transitionEnd));
                break;
            case AnimationTarget.Button:
                _monoBehaviour.StartCoroutine(TransitionFrom((RectTransform)_buttonComponent[occurrence].transform, new Vector2(0, -offset), duration, delay, easing, transitionStart, transitionEnd));
                break;
        }
    }

    public void TransitionFromDown(AnimationTarget target, int occurrence, float offset, EasingType easing = EasingType.Linear, float duration = defaultDuration, float delay = 0f, UnityAction transitionStart = null, UnityAction transitionEnd = null)
    {
        switch (target)
        {
            case AnimationTarget.Panel:
                _monoBehaviour.StartCoroutine(TransitionFrom(_panelTransform, new Vector2(0, -offset), duration, delay, easing, transitionStart, transitionEnd));
                break;
            case AnimationTarget.Text:
                _monoBehaviour.StartCoroutine(TransitionFrom(_textComponent[occurrence].rectTransform, new Vector2(0, offset), duration, delay, easing, transitionStart, transitionEnd));
                break;
            case AnimationTarget.Image:
                _monoBehaviour.StartCoroutine(TransitionFrom(_imageComponent[occurrence].rectTransform, new Vector2(0, offset), duration, delay, easing, transitionStart, transitionEnd));
                break;
            case AnimationTarget.Button:
                _monoBehaviour.StartCoroutine(TransitionFrom((RectTransform)_buttonComponent[occurrence].transform, new Vector2(0, offset), duration, delay, easing, transitionStart, transitionEnd));
                break;
        }
    }

    public void TransitionFromLeft(AnimationTarget target, int occurrence, float offset, EasingType easing = EasingType.Linear, float duration = defaultDuration, float delay = 0f, UnityAction transitionStart = null, UnityAction transitionEnd = null)
    {    
        switch (target)
        {
            case AnimationTarget.Panel:
                _monoBehaviour.StartCoroutine(TransitionFrom(_panelTransform, new Vector2(offset, 0), duration, delay, easing, transitionStart, transitionEnd));
                break;
            case AnimationTarget.Text:
                _monoBehaviour.StartCoroutine(TransitionFrom(_textComponent[occurrence].rectTransform, new Vector2(offset, 0), duration, delay, easing, transitionStart, transitionEnd));
                break;
            case AnimationTarget.Image:
                _monoBehaviour.StartCoroutine(TransitionFrom(_imageComponent[occurrence].rectTransform, new Vector2(offset, 0), duration, delay, easing, transitionStart, transitionEnd));
                break;
            case AnimationTarget.Button:
                _monoBehaviour.StartCoroutine(TransitionFrom((RectTransform)_buttonComponent[occurrence].transform, new Vector2(offset, 0), duration, delay, easing, transitionStart, transitionEnd));
                break;
        }
    }

    public void TransitionFromRight(AnimationTarget target, int occurrence, float offset, EasingType easing = EasingType.Linear, float duration = defaultDuration, float delay = 0f, UnityAction transitionStart = null, UnityAction transitionEnd = null)
    {
        switch (target)
        {
            case AnimationTarget.Panel:
                _monoBehaviour.StartCoroutine(TransitionFrom(_panelTransform, new Vector2(-offset, 0), duration, delay, easing, transitionStart, transitionEnd));
                break;
            case AnimationTarget.Text:
                _monoBehaviour.StartCoroutine(TransitionFrom(_textComponent[occurrence].rectTransform, new Vector2(-offset, 0), duration, delay, easing, transitionStart, transitionEnd));
                break;
            case AnimationTarget.Image:
                _monoBehaviour.StartCoroutine(TransitionFrom(_imageComponent[occurrence].rectTransform, new Vector2(-offset, 0), duration, delay, easing, transitionStart, transitionEnd));
                break;
            case AnimationTarget.Button:
                _monoBehaviour.StartCoroutine(TransitionFrom((RectTransform)_buttonComponent[occurrence].transform, new Vector2(-offset, 0), duration, delay, easing, transitionStart, transitionEnd));
                break;
        }
    }

    public void TransitionFromPosition(AnimationTarget target, int occurrence, Vector2 offset, EasingType easing = EasingType.Linear, float duration = defaultDuration, float delay = 0f, UnityAction transitionStart = null, UnityAction transitionEnd = null)
    {
        switch (target)
        {
            case AnimationTarget.Panel:
                _monoBehaviour.StartCoroutine(TransitionFrom(_panelTransform, -offset, duration, delay, easing, transitionStart, transitionEnd));
                break;
            case AnimationTarget.Text:
                _monoBehaviour.StartCoroutine(TransitionFrom(_textComponent[occurrence].rectTransform, -offset, duration, delay, easing, transitionStart, transitionEnd));
                break;
            case AnimationTarget.Image:
                _monoBehaviour.StartCoroutine(TransitionFrom(_imageComponent[occurrence].rectTransform, -offset, duration, delay, easing, transitionStart, transitionEnd));
                break;
            case AnimationTarget.Button:
                _monoBehaviour.StartCoroutine(TransitionFrom((RectTransform)_buttonComponent[occurrence].transform, -offset, duration, delay, easing, transitionStart, transitionEnd));
                break;
        }
    }

    public void TransitionToUp(AnimationTarget target, int occurrence, float offset, EasingType easing = EasingType.Linear, float duration = defaultDuration, float delay = 0f, UnityAction transitionStart = null, UnityAction transitionEnd = null)
    {
        switch (target)
        {
            case AnimationTarget.Panel:
                _monoBehaviour.StartCoroutine(TransitionTo(_panelTransform, occurrence, new Vector2(0, offset), duration, delay, easing, transitionStart, transitionEnd));
                break;
            case AnimationTarget.Text:
                _monoBehaviour.StartCoroutine(TransitionTo(_textComponent[occurrence].rectTransform, occurrence, new Vector2(0, offset), duration, delay, easing, transitionStart, transitionEnd));
                break;
            case AnimationTarget.Image:
                _monoBehaviour.StartCoroutine(TransitionTo(_imageComponent[occurrence].rectTransform, occurrence, new Vector2(0, offset), duration, delay, easing, transitionStart, transitionEnd));
                break;
            case AnimationTarget.Button:
                _monoBehaviour.StartCoroutine(TransitionTo((RectTransform)_buttonComponent[occurrence].transform, occurrence, new Vector2(0, offset), duration, delay, easing, transitionStart, transitionEnd));
                break;
        }
    }

    public void TransitionToDown(AnimationTarget target, int occurrence, float offset, EasingType easing = EasingType.Linear, float duration = defaultDuration, float delay = 0f, UnityAction transitionStart = null, UnityAction transitionEnd = null)
    {
        switch (target)
        {
            case AnimationTarget.Panel:
                _monoBehaviour.StartCoroutine(TransitionTo(_panelTransform, occurrence, new Vector2(0, -offset), duration, delay, easing, transitionStart, transitionEnd));
                break;
            case AnimationTarget.Text:
                _monoBehaviour.StartCoroutine(TransitionTo(_textComponent[occurrence].rectTransform, occurrence, new Vector2(0, -offset), duration, delay, easing, transitionStart, transitionEnd));
                break;
            case AnimationTarget.Image:
                _monoBehaviour.StartCoroutine(TransitionTo(_imageComponent[occurrence].rectTransform, occurrence, new Vector2(0, -offset), duration, delay, easing, transitionStart, transitionEnd));
                break;
            case AnimationTarget.Button:
                _monoBehaviour.StartCoroutine(TransitionTo((RectTransform)_buttonComponent[occurrence].transform, occurrence, new Vector2(0, -offset), duration, delay, easing, transitionStart, transitionEnd));
                break;
        }
    }

    public void TransitionToLeft(AnimationTarget target, int occurrence, float offset, EasingType easing = EasingType.Linear, float duration = defaultDuration, float delay = 0f, UnityAction transitionStart = null, UnityAction transitionEnd = null)
    {
        switch (target)
        {
            case AnimationTarget.Panel:
                _monoBehaviour.StartCoroutine(TransitionTo(_panelTransform, occurrence, new Vector2(-offset, 0), duration, delay, easing, transitionStart, transitionEnd));
                break;
            case AnimationTarget.Text:
                _monoBehaviour.StartCoroutine(TransitionTo(_textComponent[occurrence].rectTransform, occurrence, new Vector2(-offset, 0), duration, delay, easing, transitionStart, transitionEnd));
                break;
            case AnimationTarget.Image:
                _monoBehaviour.StartCoroutine(TransitionTo(_imageComponent[occurrence].rectTransform, occurrence, new Vector2(-offset, 0), duration, delay, easing, transitionStart, transitionEnd));
                break;
            case AnimationTarget.Button:
                _monoBehaviour.StartCoroutine(TransitionTo((RectTransform)_buttonComponent[occurrence].transform, occurrence, new Vector2(-offset, 0), duration, delay, easing, transitionStart, transitionEnd));
                break;
        }
    }

    public void TransitionToRight(AnimationTarget target, int occurrence, float offset, EasingType easing = EasingType.Linear, float duration = defaultDuration, float delay = 0f, UnityAction transitionStart = null, UnityAction transitionEnd = null)
    {
        switch (target)
        {
            case AnimationTarget.Panel:
                _monoBehaviour.StartCoroutine(TransitionTo(_panelTransform, occurrence, new Vector2(offset, 0), duration, delay, easing, transitionStart, transitionEnd));
                break;
            case AnimationTarget.Text:
                _monoBehaviour.StartCoroutine(TransitionTo(_textComponent[occurrence].rectTransform, occurrence, new Vector2(offset, 0), duration, delay, easing, transitionStart, transitionEnd));
                break;
            case AnimationTarget.Image:
                _monoBehaviour.StartCoroutine(TransitionTo(_imageComponent[occurrence].rectTransform, occurrence, new Vector2(offset, 0), duration, delay, easing, transitionStart, transitionEnd));
                break;
            case AnimationTarget.Button:
                _monoBehaviour.StartCoroutine(TransitionTo((RectTransform)_buttonComponent[occurrence].transform, occurrence, new Vector2(offset, 0), duration, delay, easing, transitionStart, transitionEnd));
                break;
        }
    }

    public void TransitionToPosition(AnimationTarget target, int occurrence, Vector2 offset, EasingType easing = EasingType.Linear, float duration = defaultDuration, float delay = 0f, UnityAction transitionStart = null, UnityAction transitionEnd = null)
    {
        switch (target)
        {
            case AnimationTarget.Panel:
                _monoBehaviour.StartCoroutine(TransitionTo(_panelTransform, occurrence, offset, duration, delay, easing, transitionStart, transitionEnd));
                break;
            case AnimationTarget.Text:
                _monoBehaviour.StartCoroutine(TransitionTo(_textComponent[occurrence].rectTransform, occurrence, offset, duration, delay, easing, transitionStart, transitionEnd));
                break;
            case AnimationTarget.Image:
                _monoBehaviour.StartCoroutine(TransitionTo(_imageComponent[occurrence].rectTransform, occurrence, offset, duration, delay, easing, transitionStart, transitionEnd));
                break;
            case AnimationTarget.Button:
                _monoBehaviour.StartCoroutine(TransitionTo((RectTransform)_buttonComponent[occurrence].transform, occurrence, offset, duration, delay, easing, transitionStart, transitionEnd));
                break;
        }
    }

    // ----------------------------------------------------- FROM TRANSITION -----------------------------------------------------

    private IEnumerator TransitionFrom(RectTransform component, Vector2 offset, float duration, float delay, EasingType easing, UnityAction transitionStart, UnityAction transitionEnd)
    {
        if (component == null) { yield break; }

        Vector2 startPos, targetPos;

        transitionStart?.Invoke();
        float elapsedTime = 0f;

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

        if (component == _panelTransform)
        {
            if (!storedPosition[panelIndex][0])
            {
                startPos = _panelTransform.anchoredPosition;
                originalPosition[panelIndex][0] = startPos;
                storedPosition[panelIndex][0] = true;
            }
            else
            {
                startPos = originalPosition[panelIndex][0];
            }
        }
        else if (component == _textComponent[occurrence].rectTransform)
        {
            if (!storedPosition[textIndex][occurrence]) 
            {
                startPos = _textComponent[occurrence].rectTransform.anchoredPosition;
                originalPosition[textIndex][occurrence] = startPos;
                storedPosition[textIndex][occurrence] = true; 
            }
            else 
            {
                startPos = originalPosition[textIndex][occurrence];
            }
        }
        else if (component == _imageComponent[occurrence].rectTransform)
        {
            if (!storedPosition[imageIndex][occurrence]) 
            {
                startPos = _imageComponent[occurrence].rectTransform.anchoredPosition;
                originalPosition[imageIndex][occurrence] = startPos;
                storedPosition[imageIndex][occurrence] = true;
            }
            else 
            {
                startPos = originalPosition[imageIndex][occurrence];
            }
        }
        else if (component == (RectTransform)_buttonComponent[occurrence].transform)
        {
            if (!storedPosition[buttonIndex][occurrence])
            {
                startPos = ((RectTransform)_buttonComponent[occurrence].transform).anchoredPosition;
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
