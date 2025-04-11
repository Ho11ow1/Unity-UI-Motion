using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

/* --------------------------------------------------------
 * Unity UI Motion - TypeWriter Animation Component
 * Created by Hollow1
 * 
 * Applies a typewriter effect to a TextMeshProUGUI component
 * 
 * Version: 2.2.1
 * GitHub: https://github.com/Hollow1/Unity-UI-Motion
 * -------------------------------------------------------- */

#pragma warning disable IDE0090 // Use 'new'
[AddComponentMenu("")]
internal class TypeWrite
{
    private readonly TextMeshProUGUI[] _textComponent;
    private readonly MonoBehaviour _monoBehaviour;

    private readonly Utils.StringAutoIncreaseList _targetString = new Utils.StringAutoIncreaseList();
    private int _length;

    private const float _standardDelay = 0.3f;
    private const float _standardDuration = 3f;

    public TypeWrite(TextMeshProUGUI[] tmp, MonoBehaviour runner)
    {
        _textComponent = tmp;
        _monoBehaviour = runner;
    }

    // ----------------------------------------------------- PUBLIC API -----------------------------------------------------

    public void TypeWriter(int occurrence, float delay = _standardDelay, float duration = _standardDuration, UnityAction typeWriteStart = null, UnityAction typeWriteEnd = null)
    {
        _targetString[occurrence] = _textComponent[occurrence].text;
        _length = _targetString[occurrence].Length;
        _monoBehaviour.StartCoroutine(Writer(occurrence, delay, duration, typeWriteStart, typeWriteEnd));
    }

    // ----------------------------------------------------- TYPEWRITER EFFECT -----------------------------------------------------

    private IEnumerator Writer(int occurrence, float delay, float duration, UnityAction typeWriteStart, UnityAction typeWriteEnd)
    {
        if (_textComponent == null) { yield break; }

        typeWriteStart?.Invoke();
        _textComponent[occurrence].text = "";
        string currentText = "";

        if (duration > 0 && _length > 0) { delay = duration / _length; }

        foreach (char c in _targetString[occurrence])
        {
            currentText += c;
            _textComponent[occurrence].text = currentText;
            yield return new WaitForSeconds(delay);
        }

        if (_textComponent[occurrence].text != _targetString[occurrence]) { _textComponent[occurrence].text = _targetString[occurrence]; }
        typeWriteEnd?.Invoke();
    }
}
