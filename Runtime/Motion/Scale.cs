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
    private readonly TextMeshProUGUI[] textComponent;
    private readonly Image[] imageComponent;
    private readonly RectTransform panelTransform;
    private readonly MonoBehaviour monoBehaviour;

    private readonly Vector2[][] originalScale = new Vector2[3][] { new Vector2[10], new Vector2[10], new Vector2[10] };
    private readonly bool[][] storedScale = new bool[3][] { new bool[10], new bool[10], new bool[10] };

    private const float scalingTime = 0.5f;

    public Scale(TextMeshProUGUI[] text, Image[] image, RectTransform panel, MonoBehaviour runner)
    {
        textComponent = text;
        imageComponent = image;
        panelTransform = panel;
        monoBehaviour = runner;
    }

    // ----------------------------------------------------- PUBLIC API -----------------------------------------------------

    public void ScaleUp(Motion.TransitionTarget target, int occurrence, float multiplier, Motion.EasingType easing = Motion.EasingType.Linear, float duration = scalingTime, float delay = 0f)
    {
        switch (target)
        {
            case Motion.TransitionTarget.Text:
                monoBehaviour.StartCoroutine(ScaleUi(textComponent[occurrence].rectTransform, occurrence, multiplier, duration, delay, easing));
                break;
            case Motion.TransitionTarget.Image:
                monoBehaviour.StartCoroutine(ScaleUi(imageComponent[occurrence].rectTransform, occurrence, multiplier, duration, delay, easing));
                break;
            case Motion.TransitionTarget.Panel:
                monoBehaviour.StartCoroutine(ScaleUi(panelTransform, occurrence, multiplier, duration, delay, easing));
                break;
            case Motion.TransitionTarget.All:
                monoBehaviour.StartCoroutine(ScaleUi(textComponent[occurrence].rectTransform, occurrence, multiplier, duration, delay, easing));
                monoBehaviour.StartCoroutine(ScaleUi(imageComponent[occurrence].rectTransform, occurrence, multiplier, duration, delay, easing));
                monoBehaviour.StartCoroutine(ScaleUi(panelTransform, occurrence, multiplier, duration, delay, easing));
                break;
        }
    }

    public void ScaleDown(Motion.TransitionTarget target, int occurrence, float multiplier, Motion.EasingType easing = Motion.EasingType.Linear, float duration = scalingTime, float delay = 0f)
    {
        switch (target)
        {
            case Motion.TransitionTarget.Text:
                monoBehaviour.StartCoroutine(ScaleUi(textComponent[occurrence].rectTransform, occurrence, 1/ multiplier, duration, delay, easing));
                break;
            case Motion.TransitionTarget.Image:
                monoBehaviour.StartCoroutine(ScaleUi(imageComponent[occurrence].rectTransform, occurrence, 1 / multiplier, duration, delay, easing));
                break;
            case Motion.TransitionTarget.Panel:
                monoBehaviour.StartCoroutine(ScaleUi(panelTransform, occurrence, 1 / multiplier, duration, delay, easing));
                break;
            case Motion.TransitionTarget.All:
                monoBehaviour.StartCoroutine(ScaleUi(textComponent[occurrence].rectTransform, occurrence, 1 / multiplier, duration, delay, easing));
                monoBehaviour.StartCoroutine(ScaleUi(imageComponent[occurrence].rectTransform, occurrence, 1 / multiplier, duration, delay, easing));
                monoBehaviour.StartCoroutine(ScaleUi(panelTransform, occurrence, 1 / multiplier, duration, delay, easing));
                break;
        }
    }

    // ----------------------------------------------------- SCALE ANIMATION -----------------------------------------------------

    private IEnumerator ScaleUi(RectTransform component, int occurrence, float scaleAmount, float duration, float delay, Motion.EasingType easing)
    {
        if (component == null) { yield break; }
        // if (scaleAmount <= 0) { throw new ArgumentOutOfRangeException(nameof(scaleAmount), "Scaling amount must be greater than 0"); }

        Vector2 startScale = Vector2.zero;
        Vector2 targetScale;

        if (component == textComponent[occurrence].rectTransform)
        {
            if (!storedScale[0][occurrence])
            {
                startScale = textComponent[occurrence].rectTransform.localScale;
                originalScale[0][occurrence] = startScale;
                storedScale[0][occurrence] = true;
            }
            else
            {
                startScale = originalScale[0][occurrence];
            }
        }
        else if (component == imageComponent[occurrence].rectTransform)
        {
            if (!storedScale[1][occurrence])
            {
                startScale = imageComponent[occurrence].rectTransform.localScale;
                originalScale[1][occurrence] = startScale;
                storedScale[1][occurrence] = true;
            }
            else
            {
                startScale = originalScale[1][occurrence];
            }
        }
        else if (component == panelTransform)
        {
            if (!storedScale[2][0])
            {
                startScale = panelTransform.localScale;
                originalScale[2][0] = startScale;
                storedScale[2][0] = true;
            }
            else
            {
                startScale = originalScale[2][0];
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
