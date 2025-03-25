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
    private readonly TextMeshProUGUI textComponent;
    private readonly MonoBehaviour monoBehaviour;

    private readonly string targetText;
    private readonly int length;

    private const float standardDelay = 0.3f;
    private const float standardDuration = 3f;

    public TypeWrite(TextMeshProUGUI tmp, MonoBehaviour runner)
    {
        textComponent = tmp;
        monoBehaviour = runner;
        targetText = textComponent.text;
        length = targetText.Length;
    }

    // ----------------------------------------------------- PUBLIC API -----------------------------------------------------

    public void TypeWriter(float delay = standardDelay, float duration = standardDuration)
    {
        monoBehaviour.StartCoroutine(TW(delay, duration));
    }

    // ----------------------------------------------------- TYPEWRITER EFFECT -----------------------------------------------------

    private IEnumerator TW(float delay, float duration)
    {
        if (textComponent == null) { yield break; }

        textComponent.text = "";
        string currentText = "";

        if (duration > 0 && length > 0) { delay = duration / length; }

        foreach (char c in targetText)
        {
            currentText += c;
            textComponent.text = currentText;
            yield return new WaitForSeconds(delay);
        }

        if (textComponent.text != targetText) { textComponent.text = targetText; }
    }
}
