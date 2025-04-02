using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

/* --------------------------------------------------------
 * Unity UI Listifier - OnHover effect component
 * Created by Hollow1
 * 
 * Applies a customisable hover effect to a button on hover
 * 
 * Version: 2.1.0
 * GitHub: https://github.com/Hollow1/Unity-UI-Motion
 * -------------------------------------------------------- */

public class HoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{
    private RectTransform rectTransform;
    private Button button;
    private TextMeshProUGUI[] textArr;
    private bool isSelected = false;

    private Coroutine hoverEffectCoroutine;
    private Dictionary<RectTransform, Coroutine> textPositionCoroutines = new Dictionary<RectTransform, Coroutine>();

    private float width;
    private float height;
    private float widthDifference;
    private float heightDifference;
    private Vector2 originalPosition;

    private readonly float effectDuration = 0.2f;
    private readonly float xOffset = 25f;

    private readonly float yOffset = 15f;
    private readonly float visibleText = 1.0f;
    private readonly float invisibleText = 0.0f;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        button = GetComponent<Button>();
        textArr = button.GetComponentsInChildren<TextMeshProUGUI>();

        #if UNITY_EDITOR
        if (textArr == null || textArr.Length < 1) { Debug.LogWarning($" [{gameObject.name}] No TextMeshProUGUI(Description) component found.  Parent: [{transform.parent.name}]"); }
        #endif

        InitOriginalValues();
    }

    void OnDisable()
    {
        StopAllCoroutines();
        hoverEffectCoroutine = null;
        textPositionCoroutines.Clear();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isSelected)
        {
            ApplyHoverEffect(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isSelected)
        {
            ApplyHoverEffect(false);
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        isSelected = true;
        ApplyHoverEffect(true);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        isSelected = false;
        ApplyHoverEffect(false);
    }

    private void InitOriginalValues()
    {
        if (textArr.Length < 2) { return; }

        width = rectTransform.rect.width;
        height = rectTransform.rect.height;
        widthDifference = width - textArr[1].rectTransform.rect.width;
        heightDifference = height - textArr[1].rectTransform.rect.height;

        originalPosition = rectTransform.anchoredPosition;
    }

    private void ApplyHoverEffect(bool isHovered)
    {
        if (textArr.Length < 2) { return; }

        Vector2 targetSize = isHovered ? new Vector2(width + widthDifference, height + heightDifference) : new Vector2(width, height);
        Vector2 targetPosition = isHovered ? originalPosition + new Vector2(xOffset, 0) : originalPosition;

        foreach (TextMeshProUGUI text in textArr)
        {
            if (textPositionCoroutines.TryGetValue(text.rectTransform, out Coroutine currentCoroutine))
            {
                StopCoroutine(currentCoroutine);
                textPositionCoroutines.Remove(text.rectTransform);
            }

            Vector2 textOriginalPosition = text.rectTransform.anchoredPosition;
            Vector2 targetTextPosition = isHovered ? textOriginalPosition + new Vector2(0, yOffset) : textOriginalPosition - new Vector2(0, yOffset);

            text.rectTransform.anchoredPosition = textOriginalPosition;
            textPositionCoroutines[text.rectTransform] = StartCoroutine(LerpTextPosition(text.rectTransform, textOriginalPosition, targetTextPosition));
        }

        textArr[1].alpha = isHovered ? visibleText : invisibleText;

        if (hoverEffectCoroutine != null) { StopCoroutine(hoverEffectCoroutine); }
        hoverEffectCoroutine = StartCoroutine(LerpHoverEffect(targetSize, targetPosition));
    }

    private IEnumerator LerpTextPosition(RectTransform rectTransform, Vector2 startPos, Vector2 targetPos)
    {
        float elapsedTime = 0f;
        while (elapsedTime < effectDuration)
        {
            float time = elapsedTime / effectDuration;
            float easedTime = Mathf.SmoothStep(0, 1, time);

            rectTransform.anchoredPosition = Vector2.Lerp(startPos, targetPos, easedTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rectTransform.anchoredPosition = targetPos;
        textPositionCoroutines.Remove(rectTransform);
    }

    private IEnumerator LerpHoverEffect(Vector2 targetSize, Vector2 targetPosition)
    {
        Vector2 startSize = rectTransform.sizeDelta;
        Vector2 startPosition = rectTransform.anchoredPosition;

        float elapsedTime = 0f;
        while (elapsedTime < effectDuration)
        {
            float time = elapsedTime / effectDuration;
            float easedTime = Mathf.SmoothStep(0, 1, time);

            rectTransform.sizeDelta = Vector2.Lerp(startSize, targetSize, easedTime);
            rectTransform.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, easedTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rectTransform.sizeDelta = targetSize;
        rectTransform.anchoredPosition = targetPosition;
        hoverEffectCoroutine = null;
    }


}
