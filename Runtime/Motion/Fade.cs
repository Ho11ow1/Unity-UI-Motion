using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

using static Motion;

/* --------------------------------------------------------
 * Unity UI Motion - Fade Animation Component
 * Created by Hollow1
 * 
 * Applies a Fade animation to a UI component
 * 
 * Version: 2.3.1
 * GitHub: https://github.com/Hollow1/Unity-UI-Motion
 * -------------------------------------------------------- */

#pragma warning disable IDE0090 // Use 'new'
[AddComponentMenu("")]
internal class Fade
{
    private readonly TextMeshProUGUI[] _textComponent;
    private readonly Image[] _imageComponent;
    private readonly Button[] _buttonComponent;
    private readonly CanvasGroup _panelAlpha;
    private readonly MonoBehaviour _monoBehaviour;

    private readonly List<Utils.AutoIncreaseList<float>> originalAlpha= new List<Utils.AutoIncreaseList<float>>()
    {
        new Utils.AutoIncreaseList<float>(),
        new Utils.AutoIncreaseList<float>(),
        new Utils.AutoIncreaseList<float>(),
        new Utils.AutoIncreaseList<float>()
    };

    private readonly List<Utils.AutoIncreaseList<bool>> storedAlpha = new List<Utils.AutoIncreaseList<bool>>()
    {
        new Utils.AutoIncreaseList<bool>(),
        new Utils.AutoIncreaseList<bool>(),
        new Utils.AutoIncreaseList<bool>(),
        new Utils.AutoIncreaseList<bool>()
    };


    private const float transparent = 0f;
    private const float visible = 1.0f;

    public Fade(TextMeshProUGUI[] text, Image[] image, Button[] button, CanvasGroup cg, MonoBehaviour runner)
    {
        _textComponent = text;
        _imageComponent = image;
        _buttonComponent = button;
        _panelAlpha = cg;
        _monoBehaviour = runner;
    }

    // ----------------------------------------------------- PUBLIC API -----------------------------------------------------

    public void SetPanelVisibility(bool visibility)
    {
        if (_panelAlpha == null) { return; }

        _panelAlpha.alpha = visibility ? visible : transparent;
    }

    public void FadeIn(AnimationTarget target, int occurrence, float delay = 0f, float duration = Motion.defaultDuration, UnityAction fadeStart = null, UnityAction fadeEnd = null)
    {
        switch (target)
        {
            case AnimationTarget.Panel:
                _monoBehaviour.StartCoroutine(FadeUiIn(_panelAlpha.gameObject, occurrence, duration, delay, fadeStart, fadeEnd));
                break;
            case AnimationTarget.Text:
                _monoBehaviour.StartCoroutine(FadeUiIn(_textComponent[occurrence].gameObject, occurrence, duration, delay, fadeStart, fadeEnd));
                break;
            case AnimationTarget.Image:
                _monoBehaviour.StartCoroutine(FadeUiIn(_imageComponent[occurrence].gameObject, occurrence, duration, delay, fadeStart, fadeEnd));
                break;
            case AnimationTarget.Button:
                _monoBehaviour.StartCoroutine(FadeUiIn(_buttonComponent[occurrence].gameObject, occurrence, duration, delay, fadeStart, fadeEnd));
                break;
        }
    }

    public void FadeOut(AnimationTarget target, int occurrence, float duration = defaultDuration, float delay = 0f, UnityAction fadeStart = null, UnityAction fadeEnd = null)
    {
        switch (target)
        {
            case AnimationTarget.Panel:
                _monoBehaviour.StartCoroutine(FadeUiOut(_panelAlpha.gameObject, occurrence, duration, delay, fadeStart, fadeEnd));
                break;
            case AnimationTarget.Text:
                _monoBehaviour.StartCoroutine(FadeUiOut(_textComponent[occurrence].gameObject, occurrence, duration, delay, fadeStart, fadeEnd));
                break;
            case AnimationTarget.Image:
                _monoBehaviour.StartCoroutine(FadeUiOut(_imageComponent[occurrence].gameObject, occurrence, duration, delay, fadeStart, fadeEnd));
                break;
            case AnimationTarget.Button:
                _monoBehaviour.StartCoroutine(FadeUiOut(_buttonComponent[occurrence].gameObject, occurrence, duration, delay, fadeStart, fadeEnd));
                break;
        }
    }

    // ----------------------------------------------------- FADE IN ANIMATION -----------------------------------------------------

    private IEnumerator FadeUiIn(GameObject component, int occurrence, float duration, float delay, UnityAction fadeStart, UnityAction fadeEnd)
    {
        if (component == null) { yield break; }

        if (component == _panelAlpha.gameObject)
        {
            if (!storedAlpha[panelIndex][0])
            {
                originalAlpha[panelIndex][0] = _panelAlpha.alpha;
                storedAlpha[panelIndex][0] = true;
            }
            _panelAlpha.alpha = transparent;
        }
        else if (component == _textComponent[occurrence].gameObject)
        {
            if (!storedAlpha[textIndex][occurrence])
            {
                originalAlpha[textIndex][occurrence] = _textComponent[occurrence].alpha;
                storedAlpha[textIndex][occurrence] = true;
            }
            _textComponent[occurrence].alpha = transparent;
        }
        else if (component == _imageComponent[occurrence].gameObject)
        {
            Color imgColour = _imageComponent[occurrence].color;
            if (!storedAlpha[imageIndex][occurrence])
            {
                originalAlpha[imageIndex][occurrence] = imgColour.a;
                storedAlpha[imageIndex][occurrence] = true;
            }
            imgColour.a = transparent;
            _imageComponent[occurrence].color = imgColour;
        }
        else if (component == _buttonComponent[occurrence].gameObject)
        {
            Color btnColour = _buttonComponent[occurrence].image.color;
            if (!storedAlpha[buttonIndex][occurrence])
            {
                originalAlpha[buttonIndex][occurrence] = btnColour.a;
                storedAlpha[buttonIndex][occurrence] = true;
            }
            btnColour.a = transparent;
            _buttonComponent[occurrence].image.color = btnColour;
        }

        fadeStart?.Invoke();
        float elapsedTime = 0f;
        if (delay > 0) { yield return new WaitForSeconds(delay); }

        while (elapsedTime < duration)
        {
            float time = elapsedTime / duration;

            if (component == _panelAlpha.gameObject) 
            { 
                _panelAlpha.alpha = Mathf.Lerp(transparent, visible, time); 
            }
            else if (component == _textComponent[occurrence].gameObject) 
            { 
                _textComponent[occurrence].alpha = Mathf.Lerp(transparent, visible, time); 
            }
            else if (component == _imageComponent[occurrence].gameObject) 
            {
                Color imgColour = _imageComponent[occurrence].color;
                imgColour.a = Mathf.Lerp(transparent, visible, time);
                _imageComponent[occurrence].color = imgColour;
            }
            else if (component == _buttonComponent[occurrence].gameObject) 
            { 
                Color btnColour = _buttonComponent[occurrence].image.color;
                btnColour.a = Mathf.Lerp(transparent, visible, time);
                _buttonComponent[occurrence].image.color = btnColour;
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        if (component == _panelAlpha.gameObject) 
        {
            _panelAlpha.alpha = visible; 
        }
        else if (component == _textComponent[occurrence].gameObject) 
        { 
            _textComponent[occurrence].alpha = visible; 
        }
        else if (component == _imageComponent[occurrence].gameObject)
        {
            Color imgColour = _imageComponent[occurrence].color;
            imgColour.a = visible;
            _imageComponent[occurrence].color = imgColour;
        }
        else if (component == _buttonComponent[occurrence].gameObject)
        {
            Color btnColour = _buttonComponent[occurrence].image.color;
            btnColour.a = visible;
            _buttonComponent[occurrence].image.color = btnColour;
        }
        fadeEnd?.Invoke();
    }

    // ----------------------------------------------------- FADE OUT ANIMATION -----------------------------------------------------

    private IEnumerator FadeUiOut(GameObject component, int occurrence, float duration, float delay, UnityAction fadeStart, UnityAction fadeEnd)
    {
        if (component == null) { yield break; }

        if (component == _panelAlpha.gameObject)
        {
            if (!storedAlpha[panelIndex][0])
            {
                originalAlpha[panelIndex][0] = _panelAlpha.alpha;
                storedAlpha[panelIndex][0] = true;
            }
            _panelAlpha.alpha = visible;
        }
        else if (component == _textComponent[occurrence].gameObject)
        {
            if (!storedAlpha[textIndex][occurrence])
            {
                originalAlpha[textIndex][occurrence] = _textComponent[occurrence].alpha;
                storedAlpha[textIndex][occurrence] = true;
            }
            _textComponent[occurrence].alpha = visible;
        }
        else if (component == _imageComponent[occurrence].gameObject)
        {
            Color imgColour = _imageComponent[occurrence].color;
            if (!storedAlpha[imageIndex][occurrence])
            {
                originalAlpha[imageIndex][occurrence] = imgColour.a;
                storedAlpha[imageIndex][occurrence] = true;
            }
            imgColour.a = visible;
            _imageComponent[occurrence].color = imgColour;
        }
        else if (component == _buttonComponent[occurrence].gameObject)
        {
            Color btnColour = _buttonComponent[occurrence].image.color;
            if (!storedAlpha[buttonIndex][occurrence])
            {
                originalAlpha[buttonIndex][occurrence] = btnColour.a;
                storedAlpha[buttonIndex][occurrence] = true;
            }
            btnColour.a = visible;
            _buttonComponent[occurrence].image.color = btnColour;
        }

        fadeStart?.Invoke();
        float elapsedTime = 0f;
        if (delay > 0) { yield return new WaitForSeconds(delay); }

        while (elapsedTime < duration)
        {
            float time = elapsedTime / duration;

            if (component == _panelAlpha.gameObject)
            {
                _panelAlpha.alpha = Mathf.Lerp(visible, transparent, time);
            }
            else if (component == _textComponent[occurrence].gameObject)
            {
                _textComponent[occurrence].alpha = Mathf.Lerp(visible, transparent, time);
            }
            else if (component == _imageComponent[occurrence].gameObject)
            {
                Color imgColour = _imageComponent[occurrence].color;
                imgColour.a = Mathf.Lerp(visible, transparent, time);
                _imageComponent[occurrence].color = imgColour;
            }
            else if (component == _buttonComponent[occurrence].gameObject)
            {
                Color btnColour = _buttonComponent[occurrence].image.color;
                btnColour.a = Mathf.Lerp(visible, transparent, time);
                _buttonComponent[occurrence].image.color = btnColour;
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        if (component == _panelAlpha.gameObject)
        {
            _panelAlpha.alpha = transparent;
        }
        else if (component == _textComponent[occurrence].gameObject)
        {
            _textComponent[occurrence].alpha = transparent;
        }
        else if (component == _imageComponent[occurrence].gameObject)
        {
            Color imgColour = _imageComponent[occurrence].color;
            imgColour.a = transparent;
            _imageComponent[occurrence].color = imgColour;
        }
        else if (component == _buttonComponent[occurrence].gameObject)
        {
            Color btnColour = _buttonComponent[occurrence].image.color;
            btnColour.a = transparent;
            _buttonComponent[occurrence].image.color = btnColour;
        }
        fadeEnd?.Invoke();
    }


}
