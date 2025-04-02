using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using static Motion;

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
    private readonly Button[] buttonComponent;
    private readonly RectTransform panelTransform;
    private readonly MonoBehaviour monoBehaviour;

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

    private const float scalingTime = 0.5f;

    public Scale(TextMeshProUGUI[] text, Image[] image, Button[] button, RectTransform panel, MonoBehaviour runner)
    {
        textComponent = text;
        imageComponent = image;
        buttonComponent = button;
        panelTransform = panel;
        monoBehaviour = runner;
    }

    // ----------------------------------------------------- PUBLIC API -----------------------------------------------------

    public void ScaleUp(TransitionTarget target, int occurrence, float multiplier, EasingType easing = EasingType.Linear, float duration = scalingTime, float delay = 0f)
    {
        switch (target)
        {
            case TransitionTarget.Panel:
                monoBehaviour.StartCoroutine(ScaleUi(panelTransform, occurrence, multiplier, duration, delay, easing));
                break;
            case TransitionTarget.Text:
                monoBehaviour.StartCoroutine(ScaleUi(textComponent[occurrence].rectTransform, occurrence, multiplier, duration, delay, easing));
                break;
            case TransitionTarget.Image:
                monoBehaviour.StartCoroutine(ScaleUi(imageComponent[occurrence].rectTransform, occurrence, multiplier, duration, delay, easing));
                break;
            case TransitionTarget.Button:
                monoBehaviour.StartCoroutine(ScaleUi((RectTransform)buttonComponent[occurrence].transform, occurrence, multiplier, duration, delay, easing));
                break;
        }
    }

    public void ScaleDown(TransitionTarget target, int occurrence, float multiplier, EasingType easing = EasingType.Linear, float duration = scalingTime, float delay = 0f)
    {
        switch (target)
        {
            case TransitionTarget.Panel:
                monoBehaviour.StartCoroutine(ScaleUi(panelTransform, occurrence, 1 / multiplier, duration, delay, easing));
                break;
            case TransitionTarget.Text:
                monoBehaviour.StartCoroutine(ScaleUi(textComponent[occurrence].rectTransform, occurrence, 1/ multiplier, duration, delay, easing));
                break;
            case TransitionTarget.Image:
                monoBehaviour.StartCoroutine(ScaleUi(imageComponent[occurrence].rectTransform, occurrence, 1 / multiplier, duration, delay, easing));
                break;
            case TransitionTarget.Button:
                monoBehaviour.StartCoroutine(ScaleUi((RectTransform)buttonComponent[occurrence].transform, occurrence, 1 / multiplier, duration, delay, easing));
                break;
        }
    }

    // ----------------------------------------------------- SCALE ANIMATION -----------------------------------------------------

    private IEnumerator ScaleUi(RectTransform component, int occurrence, float scaleAmount, float duration, float delay, EasingType easing)
    {
        if (component == null) { yield break; }

        Vector2 startScale = Vector2.zero;
        Vector2 targetScale;


        if (component == panelTransform)
        {
            if (!storedScale[panelIndex][0])
            {
                startScale = panelTransform.localScale;
                originalScale[panelIndex][0] = startScale;
                storedScale[panelIndex][0] = true;
            }
            else
            {
                startScale = originalScale[panelIndex][0];
            }
        }
        else if (component == textComponent[occurrence].rectTransform)
        {
            if (!storedScale[textIndex][occurrence])
            {
                startScale = textComponent[occurrence].rectTransform.localScale;
                originalScale[textIndex][occurrence] = startScale;
                storedScale[textIndex][occurrence] = true;
            }
            else
            {
                startScale = originalScale[textIndex][occurrence];
            }
        }
        else if (component == imageComponent[occurrence].rectTransform)
        {
            if (!storedScale[imageIndex][occurrence])
            {
                startScale = imageComponent[occurrence].rectTransform.localScale;
                originalScale[imageIndex][occurrence] = startScale;
                storedScale[imageIndex][occurrence] = true;
            }
            else
            {
                startScale = originalScale[imageIndex][occurrence];
            }
        }
        else if (component == (RectTransform)buttonComponent[occurrence].transform)
        {
            if (!storedScale[buttonIndex][occurrence])
            {
                startScale = buttonComponent[occurrence].transform.localScale;
                originalScale[buttonIndex][occurrence] = startScale;
                storedScale[buttonIndex][occurrence] = true;
            }
            else
            {
                startScale = originalScale[buttonIndex][occurrence];
            }
        }

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
    }


}
