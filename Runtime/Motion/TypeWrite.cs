using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/* --------------------------------------------------------
 * Unity UI Motion - TypeWriter Animation Component
 * Created by Hollow1
 * 
 * Applies a typewriter effect to a TextMeshProUGUI component
 * 
 * Version: 2.1.0
 * GitHub: https://github.com/Hollow1/Unity-UI-Motion
 * -------------------------------------------------------- */

[AddComponentMenu("")]
internal class TypeWrite
{
    private readonly TextMeshProUGUI[] textComponent;
    private readonly MonoBehaviour monoBehaviour;

    private readonly string[] targetArr = new string[10];
    private int length;

    private const float standardDelay = 0.3f;
    private const float standardDuration = 3f;

    public TypeWrite(TextMeshProUGUI[] tmp, MonoBehaviour runner)
    {
        textComponent = tmp;
        monoBehaviour = runner;
    }

    // ----------------------------------------------------- PUBLIC API -----------------------------------------------------

    public void TypeWriter(int occurrence, float delay = standardDelay, float duration = standardDuration)
    {
        targetArr[occurrence] = textComponent[occurrence].text;
        length = targetArr[occurrence].Length;
        monoBehaviour.StartCoroutine(TW(occurrence, delay, duration));
    }

    // ----------------------------------------------------- TYPEWRITER EFFECT -----------------------------------------------------

    private IEnumerator TW(int occurrence, float delay, float duration)
    {
        if (textComponent == null) { yield break; }

        textComponent[occurrence].text = "";
        string currentText = "";

        if (duration > 0 && length > 0) { delay = duration / length; }

        foreach (char c in targetArr[occurrence])
        {
            currentText += c;
            textComponent[occurrence].text = currentText;
            yield return new WaitForSeconds(delay);
        }

        if (textComponent[occurrence].text != targetArr[occurrence]) { textComponent[occurrence].text = targetArr[occurrence]; }
    }
}
