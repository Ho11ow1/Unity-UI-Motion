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
 * Version: 2.3.1
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

    public void TypeWriterDelay(int occurrence, float delay = _standardDelay, UnityAction typeWriteStart = null, UnityAction typeWriteEnd = null)
    {
        _targetString[occurrence] = _textComponent[occurrence].text;
        _length = _targetString[occurrence].Length;
        _monoBehaviour.StartCoroutine(WriterDelay(occurrence, delay, typeWriteStart, typeWriteEnd));
    }

    public void TypeWriterDuration(int occurrence, float duration = _standardDuration, UnityAction typeWriteStart = null, UnityAction typeWriteEnd = null)
    {
        _targetString[occurrence] = _textComponent[occurrence].text;
        _length = _targetString[occurrence].Length;
        _monoBehaviour.StartCoroutine(WriterDuration(occurrence, duration, typeWriteStart, typeWriteEnd));
    }

    // ----------------------------------------------------- TYPEWRITER EFFECT -----------------------------------------------------

    private IEnumerator WriterDuration(int occurrence, float duration, UnityAction typeWriteStart, UnityAction typeWriteEnd)
    {
        if (_textComponent == null) { yield break; }

        typeWriteStart?.Invoke();
        _textComponent[occurrence].text = "";
        string currentText = "";

        float delay = 0f;
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

    private IEnumerator WriterDelay(int occurrence, float delay, UnityAction typeWriteStart, UnityAction typeWriteEnd)
    {
        if (_textComponent == null) { yield break; }

        typeWriteStart?.Invoke();
        _textComponent[occurrence].text = "";
        string currentText = "";

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
