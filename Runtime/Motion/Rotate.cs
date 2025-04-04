using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using static Motion;

/* --------------------------------------------------------
 * Unity UI Motion - Rotation Animation Component
 * Created by Hollow1
 * 
 * Applies a Rotation animation to a UI component
 * 
 * Version: 2.1.0
 * GitHub: https://github.com/Hollow1/Unity-UI-Motion
 * -------------------------------------------------------- */

[AddComponentMenu("")]
internal class Rotate
{
    private readonly TextMeshProUGUI[] textComponent;
    private readonly Image[] imageComponent;
    private readonly Button[] buttonComponent;
    private readonly RectTransform panelTransform;
    private readonly MonoBehaviour monoBehaviour;

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

    private const float rotationDuration = 1.5f;

    public Rotate(TextMeshProUGUI[] text, Image[] image, Button[] button, RectTransform panel, MonoBehaviour runner)
    {
        textComponent = text;
        imageComponent = image;
        buttonComponent = button;
        panelTransform = panel;
        monoBehaviour = runner;
    }

    // ----------------------------------------------------- PUBLIC API -----------------------------------------------------

    public void Rotation(AnimationTarget target, int occurrence, float degrees, EasingType easing = EasingType.Linear, float duration = rotationDuration, float delay = 0f)
    {
        switch (target)
        {
            case AnimationTarget.Panel:
                monoBehaviour.StartCoroutine(RotateUi(panelTransform, occurrence, degrees, duration, delay, easing));
                break;
            case AnimationTarget.Text:
                monoBehaviour.StartCoroutine(RotateUi(textComponent[occurrence].rectTransform, occurrence, degrees, duration, delay, easing));
                break;
            case AnimationTarget.Image:
                monoBehaviour.StartCoroutine(RotateUi(imageComponent[occurrence].rectTransform, occurrence, degrees, duration, delay, easing));
                break;
            case AnimationTarget.Button:
                monoBehaviour.StartCoroutine(RotateUi((RectTransform)buttonComponent[occurrence].transform, occurrence, degrees, duration, delay, easing));
                break;
        }
    }

    // ----------------------------------------------------- ROTATE ANIMATION -----------------------------------------------------

    private IEnumerator RotateUi(RectTransform component, int occurrence, float degrees, float duration, float delay, EasingType easing)
    {
        if (component == null) { yield break; }

        Quaternion startRotation = Quaternion.identity; 
        Quaternion targetRotation;

        if (component == panelTransform)
        {
            if (!storedRotation[panelIndex][0])
            {
                startRotation = panelTransform.localRotation;
                originalRotation[panelIndex][0] = startRotation;
                storedRotation[panelIndex][0] = true;
            }
            else
            {
                startRotation = originalRotation[panelIndex][0];
            }
        }
        else if (component == textComponent[occurrence].rectTransform)
        {
            if (!storedRotation[textIndex][occurrence])
            {
                startRotation = textComponent[occurrence].rectTransform.localRotation;
                originalRotation[textIndex][occurrence] = startRotation;
                storedRotation[textIndex][occurrence] = true;
            }
            else
            {
                startRotation = originalRotation[textIndex][occurrence];
            }
        }
        else if (component == imageComponent[occurrence].rectTransform)
        {
            if (!storedRotation[imageIndex][occurrence])
            {
                startRotation = imageComponent[occurrence].rectTransform.localRotation;
                originalRotation[imageIndex][occurrence] = startRotation;
                storedRotation[imageIndex][occurrence] = true;
            }
            else
            {
                startRotation = originalRotation[imageIndex][occurrence];
            }
        }
        else if (component == (RectTransform)buttonComponent[occurrence].transform)
        {
            if (!storedRotation[buttonIndex][occurrence])
            {
                startRotation = buttonComponent[occurrence].transform.localRotation;
                originalRotation[buttonIndex][occurrence] = startRotation;
                storedRotation[buttonIndex][occurrence] = true;
            }
            else
            {
                startRotation = originalRotation[buttonIndex][occurrence];
            }
        }

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
    }


}
