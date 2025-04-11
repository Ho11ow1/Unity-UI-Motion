using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

using static Motion;

/* --------------------------------------------------------
 * Unity UI Motion - Rotation Animation Component
 * Created by Hollow1
 * 
 * Applies a Rotation animation to a UI component
 * 
 * Version: 2.2.1
 * GitHub: https://github.com/Hollow1/Unity-UI-Motion
 * -------------------------------------------------------- */

#pragma warning disable IDE0090 // Use 'new'
[AddComponentMenu("")]
internal class Rotate
{
    private readonly TextMeshProUGUI[] _textComponent;
    private readonly Image[] _imageComponent;
    private readonly Button[] _buttonComponent;
    private readonly RectTransform _panelTransform;
    private readonly MonoBehaviour _monoBehaviour;

    private readonly List<Utils.AutoIncreaseList<Quaternion>> originalRotation = new List<Utils.AutoIncreaseList<Quaternion>>()
    {
        new Utils.AutoIncreaseList<Quaternion>(),
        new Utils.AutoIncreaseList<Quaternion>(),
        new Utils.AutoIncreaseList<Quaternion>(),
        new Utils.AutoIncreaseList<Quaternion>()
    };

    private readonly List<Utils.AutoIncreaseList<bool>> storedRotation = new List<Utils.AutoIncreaseList<bool>>()
    {
        new Utils.AutoIncreaseList<bool>(),
        new Utils.AutoIncreaseList<bool>(),
        new Utils.AutoIncreaseList<bool>(),
        new Utils.AutoIncreaseList<bool>()
    };

    public Rotate(TextMeshProUGUI[] text, Image[] image, Button[] button, RectTransform panel, MonoBehaviour runner)
    {
        _textComponent = text;
        _imageComponent = image;
        _buttonComponent = button;
        _panelTransform = panel;
        _monoBehaviour = runner;
    }

    // ----------------------------------------------------- PUBLIC API -----------------------------------------------------

    public void Rotation(AnimationTarget target, int occurrence, float degrees, EasingType easing = EasingType.Linear, float duration = defaultDuration, float delay = 0f, UnityAction rotateStart = null, UnityAction rotateEnd = null)
    {
        switch (target)
        {
            case AnimationTarget.Panel:
                _monoBehaviour.StartCoroutine(RotateUi(_panelTransform, occurrence, degrees, duration, delay, easing, rotateStart, rotateEnd));
                break;
            case AnimationTarget.Text:
                _monoBehaviour.StartCoroutine(RotateUi(_textComponent[occurrence].rectTransform, occurrence, degrees, duration, delay, easing, rotateStart, rotateEnd));
                break;
            case AnimationTarget.Image:
                _monoBehaviour.StartCoroutine(RotateUi(_imageComponent[occurrence].rectTransform, occurrence, degrees, duration, delay, easing, rotateStart, rotateEnd));
                break;
            case AnimationTarget.Button:
                _monoBehaviour.StartCoroutine(RotateUi((RectTransform)_buttonComponent[occurrence].transform, occurrence, degrees, duration, delay, easing, rotateStart, rotateEnd));
                break;
        }
    }

    // ----------------------------------------------------- ROTATE ANIMATION -----------------------------------------------------

    private IEnumerator RotateUi(RectTransform component, int occurrence, float degrees, float duration, float delay, EasingType easing, UnityAction rotateStart, UnityAction rotateEnd)
    {
        if (component == null) { yield break; }

        Quaternion startRotation = Quaternion.identity; 
        Quaternion targetRotation;

        if (component == _panelTransform)
        {
            if (!storedRotation[panelIndex][0])
            {
                startRotation = _panelTransform.localRotation;
                originalRotation[panelIndex][0] = startRotation;
                storedRotation[panelIndex][0] = true;
            }
            else
            {
                startRotation = originalRotation[panelIndex][0];
            }
        }
        else if (component == _textComponent[occurrence].rectTransform)
        {
            if (!storedRotation[textIndex][occurrence])
            {
                startRotation = _textComponent[occurrence].rectTransform.localRotation;
                originalRotation[textIndex][occurrence] = startRotation;
                storedRotation[textIndex][occurrence] = true;
            }
            else
            {
                startRotation = originalRotation[textIndex][occurrence];
            }
        }
        else if (component == _imageComponent[occurrence].rectTransform)
        {
            if (!storedRotation[imageIndex][occurrence])
            {
                startRotation = _imageComponent[occurrence].rectTransform.localRotation;
                originalRotation[imageIndex][occurrence] = startRotation;
                storedRotation[imageIndex][occurrence] = true;
            }
            else
            {
                startRotation = originalRotation[imageIndex][occurrence];
            }
        }
        else if (component == (RectTransform)_buttonComponent[occurrence].transform)
        {
            if (!storedRotation[buttonIndex][occurrence])
            {
                startRotation = _buttonComponent[occurrence].transform.localRotation;
                originalRotation[buttonIndex][occurrence] = startRotation;
                storedRotation[buttonIndex][occurrence] = true;
            }
            else
            {
                startRotation = originalRotation[buttonIndex][occurrence];
            }
        }

        rotateStart?.Invoke();
        float elapsedTime = 0f;
        targetRotation = Quaternion.Euler(0, 0, degrees);
        if (delay > 0) { yield return new WaitForSeconds(delay); }

        while (elapsedTime < duration)
        {
            float time = elapsedTime / duration;
            float easedTime = Utils.Easing.SetEasingFunction(time, easing);

            component.localRotation = Quaternion.Lerp(startRotation, targetRotation, easedTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        component.localRotation = targetRotation;
        rotateEnd?.Invoke();
    }


}
