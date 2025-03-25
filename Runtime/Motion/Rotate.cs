using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    private readonly TextMeshProUGUI textComponent;
    private readonly Image imageComponent;
    private readonly RectTransform panelTransform;
    private readonly MonoBehaviour monoBehaviour;

    private readonly Quaternion[] originalRotation = { Quaternion.identity, Quaternion.identity, Quaternion.identity };
    private readonly bool[] storedRotation = { false, false, false };

    private const float rotationDuration = 1.5f;

    public Rotate(TextMeshProUGUI text, Image image, RectTransform panel, MonoBehaviour runner)
    {
        textComponent = text;
        imageComponent = image;
        panelTransform = panel;
        monoBehaviour = runner;
    }

    // ----------------------------------------------------- PUBLIC API -----------------------------------------------------

    public void Rotation(Motion.TransitionTarget target, float degrees, Motion.EasingType easing = Motion.EasingType.Linear, float duration = rotationDuration, float delay = 0f)
    {
        switch (target)
        {
            case Motion.TransitionTarget.Text:
                monoBehaviour.StartCoroutine(RotateUi(textComponent.rectTransform, degrees, duration, delay, easing));
                break;
            case Motion.TransitionTarget.Image:
                monoBehaviour.StartCoroutine(RotateUi(imageComponent.rectTransform, degrees, duration, delay, easing));
                break;
            case Motion.TransitionTarget.Panel:
                monoBehaviour.StartCoroutine(RotateUi(panelTransform, degrees, duration, delay, easing));
                break;
            case Motion.TransitionTarget.All:
                monoBehaviour.StartCoroutine(RotateUi(textComponent.rectTransform, degrees, duration, delay, easing));
                monoBehaviour.StartCoroutine(RotateUi(imageComponent.rectTransform, degrees, duration, delay, easing));
                monoBehaviour.StartCoroutine(RotateUi(panelTransform, degrees, duration, delay, easing));
                break;
        }
    }

    // ----------------------------------------------------- ROTATE ANIMATION -----------------------------------------------------

    private IEnumerator RotateUi(RectTransform component, float degrees, float duration, float delay, Motion.EasingType easing)
    {
        if (component == null) { yield break; }

        Quaternion startRotation = Quaternion.identity; 
        Quaternion targetRotation;

        if (component == textComponent.rectTransform)
        {
            if (!storedRotation[0])
            {
                startRotation = textComponent.rectTransform.localRotation;
                originalRotation[0] = startRotation;
                storedRotation[0] = true;
            }
            else
            {
                startRotation = originalRotation[0];
            }
        }
        else if (component == imageComponent.rectTransform)
        {
            if (!storedRotation[1])
            {
                startRotation = imageComponent.rectTransform.localRotation;
                originalRotation[1] = startRotation;
                storedRotation[1] = true;
            }
            else
            {
                startRotation = originalRotation[1];
            }
        }
        else if (component == panelTransform)
        {
            if (!storedRotation[2])
            {
                startRotation = panelTransform.localRotation;
                originalRotation[2] = startRotation;
                storedRotation[2] = true;
            }
            else
            {
                startRotation = originalRotation[2];
            }
        }

        float elapsedTime = 0f;
        targetRotation = Quaternion.Euler(0, 0, degrees);
        if (delay > 0) { yield return new WaitForSeconds(delay); }

        while (elapsedTime < duration)
        {
            float time = elapsedTime / duration;
            float easedTime = Easing.SetEasingFunction(time, easing);

            component.localRotation = Quaternion.Lerp(startRotation, targetRotation, easedTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        component.localRotation = targetRotation;
    }


}
