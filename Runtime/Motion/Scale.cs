using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

using static Motion;

/* --------------------------------------------------------
 * Unity UI Motion - Scale Animation Component
 * Created by Hollow1
 * 
 * Applies a scaling animation to a UI component
 * 
 * Version: 2.2.1
 * GitHub: https://github.com/Hollow1/Unity-UI-Motion
 * -------------------------------------------------------- */

#pragma warning disable IDE0090 // Use 'new'
[AddComponentMenu("")]
internal class Scale
{
    private readonly TextMeshProUGUI[] _textComponent;
    private readonly Image[] _imageComponent;
    private readonly Button[] _buttonComponent;
    private readonly RectTransform _panelTransform;
    private readonly MonoBehaviour _monoBehaviour;

    private readonly List<Utils.AutoIncreaseList<Vector2>> originalScale = new List<Utils.AutoIncreaseList<Vector2>>()
    {
        new Utils.AutoIncreaseList<Vector2>(),
        new Utils.AutoIncreaseList<Vector2>(),
        new Utils.AutoIncreaseList<Vector2>(),
        new Utils.AutoIncreaseList<Vector2>()
    };

    private readonly List<Utils.AutoIncreaseList<bool>> storedScale = new List<Utils.AutoIncreaseList<bool>>()
    {
        new Utils.AutoIncreaseList<bool>(),
        new Utils.AutoIncreaseList<bool>(),
        new Utils.AutoIncreaseList<bool>(),
        new Utils.AutoIncreaseList<bool>()
    };

    public Scale(TextMeshProUGUI[] text, Image[] image, Button[] button, RectTransform panel, MonoBehaviour runner)
    {
        _textComponent = text;
        _imageComponent = image;
        _buttonComponent = button;
        _panelTransform = panel;
        _monoBehaviour = runner;
    }

    // ----------------------------------------------------- PUBLIC API -----------------------------------------------------

    public void ScaleUp(AnimationTarget target, int occurrence, float multiplier, EasingType easing = EasingType.Linear, float duration = defaultDuration, float delay = 0f, UnityAction scaleStart = null, UnityAction scaleEnd = null)
    {
        switch (target)
        {
            case AnimationTarget.Panel:
                _monoBehaviour.StartCoroutine(ScaleUi(_panelTransform, occurrence, multiplier, duration, delay, easing, scaleStart, scaleEnd));
                break;
            case AnimationTarget.Text:
                _monoBehaviour.StartCoroutine(ScaleUi(_textComponent[occurrence].rectTransform, occurrence, multiplier, duration, delay, easing, scaleStart, scaleEnd));
                break;
            case AnimationTarget.Image:
                _monoBehaviour.StartCoroutine(ScaleUi(_imageComponent[occurrence].rectTransform, occurrence, multiplier, duration, delay, easing, scaleStart, scaleEnd));
                break;
            case AnimationTarget.Button:
                _monoBehaviour.StartCoroutine(ScaleUi((RectTransform)_buttonComponent[occurrence].transform, occurrence, multiplier, duration, delay, easing, scaleStart, scaleEnd));
                break;
        }
    }

    public void ScaleDown(AnimationTarget target, int occurrence, float multiplier, EasingType easing = EasingType.Linear, float duration = defaultDuration, float delay = 0f, UnityAction scaleStart = null, UnityAction scaleEnd = null)
    {
        switch (target)
        {
            case AnimationTarget.Panel:
                _monoBehaviour.StartCoroutine(ScaleUi(_panelTransform, occurrence, 1 / multiplier, duration, delay, easing, scaleStart, scaleEnd));
                break;
            case AnimationTarget.Text:
                _monoBehaviour.StartCoroutine(ScaleUi(_textComponent[occurrence].rectTransform, occurrence, 1 / multiplier, duration, delay, easing, scaleStart, scaleEnd));
                break;
            case AnimationTarget.Image:
                _monoBehaviour.StartCoroutine(ScaleUi(_imageComponent[occurrence].rectTransform, occurrence, 1 / multiplier, duration, delay, easing, scaleStart, scaleEnd));
                break;
            case AnimationTarget.Button:
                _monoBehaviour.StartCoroutine(ScaleUi((RectTransform)_buttonComponent[occurrence].transform, occurrence, 1 / multiplier, duration, delay, easing, scaleStart, scaleEnd));
                break;
        }
    }

    // ----------------------------------------------------- SCALE ANIMATION -----------------------------------------------------

    private IEnumerator ScaleUi(RectTransform component, int occurrence, float scaleAmount, float duration, float delay, EasingType easing, UnityAction scaleStart, UnityAction scaleEnd)
    {
        if (component == null) { yield break; }

        Vector2 startScale = Vector2.zero;
        Vector2 targetScale;


        if (component == _panelTransform)
        {
            if (!storedScale[panelIndex][0])
            {
                startScale = _panelTransform.localScale;
                originalScale[panelIndex][0] = startScale;
                storedScale[panelIndex][0] = true;
            }
            else
            {
                startScale = originalScale[panelIndex][0];
            }
        }
        else if (component == _textComponent[occurrence].rectTransform)
        {
            if (!storedScale[textIndex][occurrence])
            {
                startScale = _textComponent[occurrence].rectTransform.localScale;
                originalScale[textIndex][occurrence] = startScale;
                storedScale[textIndex][occurrence] = true;
            }
            else
            {
                startScale = originalScale[textIndex][occurrence];
            }
        }
        else if (component == _imageComponent[occurrence].rectTransform)
        {
            if (!storedScale[imageIndex][occurrence])
            {
                startScale = _imageComponent[occurrence].rectTransform.localScale;
                originalScale[imageIndex][occurrence] = startScale;
                storedScale[imageIndex][occurrence] = true;
            }
            else
            {
                startScale = originalScale[imageIndex][occurrence];
            }
        }
        else if (component == (RectTransform)_buttonComponent[occurrence].transform)
        {
            if (!storedScale[buttonIndex][occurrence])
            {
                startScale = _buttonComponent[occurrence].transform.localScale;
                originalScale[buttonIndex][occurrence] = startScale;
                storedScale[buttonIndex][occurrence] = true;
            }
            else
            {
                startScale = originalScale[buttonIndex][occurrence];
            }
        }

        scaleStart?.Invoke();
        float elapsedTime = 0f;
        targetScale = startScale * scaleAmount;
        if (delay > 0) { yield return new WaitForSeconds(delay); }

        while (elapsedTime < duration)
        {
            float time = elapsedTime / duration;
            float easedTime = Utils.Easing.SetEasingFunction(time, easing);

            component.localScale = Vector2.Lerp(startScale, targetScale, easedTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        component.localScale = targetScale;
        scaleEnd?.Invoke();
    }


}
