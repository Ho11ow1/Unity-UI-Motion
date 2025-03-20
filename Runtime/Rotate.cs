using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/* --------------------------------------------------------
 * Unity UI Motion - Rotation Animation Component
 * Created by Hollow1
 * 
 * A lightweight component for creating smooth rotation
 * animations on UI elements with support for multiple 
 * angles and easing functions.
 * 
 * Version: 2.0.0
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

    /// <summary>
    /// Rotate the UI element with a custom delay and duration
    /// </summary>
    /// <param name="target">Target component to transition (Text, Image, Panel, or All)</param>
    /// <param name="degrees">Degrees the rotation should rotate, positive values go clockwise, negative counter-clockwise</param>
    /// <param name="easing">Specifies the easing method the transition should use</param>
    /// <param name="duration">Time in seconds for the rotation duration</param>
    /// <param name="delay">Time in seconds to wait before starting the transition</param>
    public void RotateUi(Motion.TransitionTarget target, float degrees, Motion.EasingType easing = Motion.EasingType.Linear, float duration = rotationDuration, float delay = 0f)
    {
        switch (target)
        {
            case Motion.TransitionTarget.Text:
                monoBehaviour.StartCoroutine(TextRotation(degrees, duration, delay, easing));
                break;
            case Motion.TransitionTarget.Image:
                monoBehaviour.StartCoroutine(ImageRotation(degrees, duration, delay, easing));
                break;
            case Motion.TransitionTarget.Panel:
                monoBehaviour.StartCoroutine(PanelRotation(degrees, duration, delay, easing));
                break;
            case Motion.TransitionTarget.All:
                monoBehaviour.StartCoroutine(TextRotation(degrees, duration, delay, easing));
                monoBehaviour.StartCoroutine(ImageRotation(degrees, duration, delay, easing));
                monoBehaviour.StartCoroutine(PanelRotation(degrees, duration, delay, easing));
                break;
        }
    }

    private IEnumerator TextRotation(float degrees, float duration, float delay, Motion.EasingType easing)
    {
        if (textComponent == null) { yield break; }

        Quaternion startRotation, targetRotation;
        if (storedRotation[0] == false)
        {
            startRotation = textComponent.rectTransform.localRotation;
            originalRotation[0] = startRotation;
            storedRotation[0] = true;
        }
        else
        {
            startRotation = originalRotation[0];
        }

        float elapsedTime = 0f;
        targetRotation = Quaternion.Euler(0, 0, degrees);
        if (delay > 0) { yield return new WaitForSeconds(delay); }

        while (elapsedTime < duration)
        {
            float time = elapsedTime / duration;
            float easedTime = Easing.SetEasingFunction(time, easing);

            textComponent.rectTransform.localRotation = Quaternion.Lerp(startRotation, targetRotation, easedTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        textComponent.rectTransform.localRotation = targetRotation;
    }

    private IEnumerator ImageRotation(float degrees, float duration, float delay, Motion.EasingType easing)
    {
        if (imageComponent == null) { yield break; }

        Quaternion startRotation, targetRotation;
        if (storedRotation[1] == false)
        {
            startRotation = imageComponent.rectTransform.localRotation;
            originalRotation[1] = startRotation;
            storedRotation[1] = true;
        }
        else
        {
            startRotation = originalRotation[1];
        }

        float elapsedTime = 0f;
        targetRotation = Quaternion.Euler(0, 0, degrees);
        if (delay > 0) { yield return new WaitForSeconds(delay); }

        while (elapsedTime < duration)
        {
            float time = elapsedTime / duration;
            float easedTime = Easing.SetEasingFunction(time, easing);

            imageComponent.rectTransform.localRotation = Quaternion.Lerp(startRotation, targetRotation, easedTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        imageComponent.rectTransform.localRotation = targetRotation;
    }

    private IEnumerator PanelRotation(float degrees, float duration, float delay, Motion.EasingType easing)
    {
        if (panelTransform == null) {  yield break;  }

        Quaternion startRotation, targetRotation;
        if (storedRotation[2] == false)
        {
            startRotation = panelTransform.localRotation;
            originalRotation[2] = startRotation;
            storedRotation[2] = true;
        }
        else
        {
            startRotation = originalRotation[2];
        }

        float elapsedTime = 0f;
        targetRotation = Quaternion.Euler(0, 0, degrees);
        if (delay > 0) { yield return new WaitForSeconds(delay); }

        while (elapsedTime < duration)
        {
            float time = elapsedTime / duration;
            float easedTime = Easing.SetEasingFunction(time, easing);

            panelTransform.localRotation = Quaternion.Lerp(startRotation, targetRotation, easedTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        panelTransform.localRotation = targetRotation;
    }
}
