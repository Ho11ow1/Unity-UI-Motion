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

    private readonly Utils.StringAutoIncreaseList targetString = new Utils.StringAutoIncreaseList();
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
        targetString[occurrence] = textComponent[occurrence].text;
        length = targetString[occurrence].Length;
        monoBehaviour.StartCoroutine(Writer(occurrence, delay, duration));
    }

    // ----------------------------------------------------- TYPEWRITER EFFECT -----------------------------------------------------

    private IEnumerator Writer(int occurrence, float delay, float duration)
    {
        if (textComponent == null) { yield break; }

        textComponent[occurrence].text = "";
        string currentText = "";

        if (duration > 0 && length > 0) { delay = duration / length; }

        foreach (char c in targetString[occurrence])
        {
            currentText += c;
            textComponent[occurrence].text = currentText;
            yield return new WaitForSeconds(delay);
        }

        if (textComponent[occurrence].text != targetString[occurrence]) { textComponent[occurrence].text = targetString[occurrence]; }
    }
}
