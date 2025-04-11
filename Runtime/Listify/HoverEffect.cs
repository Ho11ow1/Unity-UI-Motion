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
 * Version: 2.2.1
 * GitHub: https://github.com/Hollow1/Unity-UI-Motion
 * -------------------------------------------------------- */

public class HoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{
    private RectTransform _rectTransform;
    private Button _button;
    private TextMeshProUGUI[] _textArr;
    private bool _isSelected = false;

    private Coroutine _hoverEffectCoroutine;
    private readonly Dictionary<RectTransform, Coroutine> _textPositionCoroutines = new Dictionary<RectTransform, Coroutine>();

    private float _width;
    private float _height;
    private float _widthDifference;
    private float _heightDifference;
    private Vector2 _originalPosition;

    private readonly float _effectDuration = 0.2f;
    private readonly float _xOffset = 25f;

    private readonly float _yOffset = 15f;
    private readonly float _visibleText = 1.0f;
    private readonly float _invisibleText = 0.0f;

    void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _button = GetComponent<Button>();
        _textArr = _button.GetComponentsInChildren<TextMeshProUGUI>();

        #if UNITY_EDITOR
        if (_textArr == null || _textArr.Length < 1) { Debug.LogWarning($" [{gameObject.name}] No TextMeshProUGUI(Description) component found.  Parent: [{transform.parent.name}]"); }
        #endif

        InitOriginalValues();
    }

    void OnDisable()
    {
        StopAllCoroutines();
        _hoverEffectCoroutine = null;
        _textPositionCoroutines.Clear();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!_isSelected)
        {
            ApplyHoverEffect(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!_isSelected)
        {
            ApplyHoverEffect(false);
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        _isSelected = true;
        ApplyHoverEffect(true);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        _isSelected = false;
        ApplyHoverEffect(false);
    }

    private void InitOriginalValues()
    {
        if (_textArr.Length < 2) { return; }

        _width = _rectTransform.rect.width;
        _height = _rectTransform.rect.height;
        _widthDifference = _width - _textArr[1].rectTransform.rect.width;
        _heightDifference = _height - _textArr[1].rectTransform.rect.height;

        _originalPosition = _rectTransform.anchoredPosition;
    }

    private void ApplyHoverEffect(bool isHovered)
    {
        if (_textArr.Length < 2) { return; }

        Vector2 targetSize = isHovered ? new Vector2(_width + _widthDifference, _height + _heightDifference) : new Vector2(_width, _height);
        Vector2 targetPosition = isHovered ? _originalPosition + new Vector2(_xOffset, 0) : _originalPosition;

        foreach (TextMeshProUGUI text in _textArr)
        {
            if (_textPositionCoroutines.TryGetValue(text.rectTransform, out Coroutine currentCoroutine))
            {
                StopCoroutine(currentCoroutine);
                _textPositionCoroutines.Remove(text.rectTransform);
            }

            Vector2 textOriginalPosition = text.rectTransform.anchoredPosition;
            Vector2 targetTextPosition = isHovered ? textOriginalPosition + new Vector2(0, _yOffset) : textOriginalPosition - new Vector2(0, _yOffset);

            text.rectTransform.anchoredPosition = textOriginalPosition;
            _textPositionCoroutines[text.rectTransform] = StartCoroutine(LerpTextPosition(text.rectTransform, textOriginalPosition, targetTextPosition));
        }

        _textArr[1].alpha = isHovered ? _visibleText : _invisibleText;

        if (_hoverEffectCoroutine != null) { StopCoroutine(_hoverEffectCoroutine); }
        _hoverEffectCoroutine = StartCoroutine(LerpHoverEffect(targetSize, targetPosition));
    }

    private IEnumerator LerpTextPosition(RectTransform rectTransform, Vector2 startPos, Vector2 targetPos)
    {
        float elapsedTime = 0f;
        while (elapsedTime < _effectDuration)
        {
            float time = elapsedTime / _effectDuration;
            float easedTime = Mathf.SmoothStep(0, 1, time);

            rectTransform.anchoredPosition = Vector2.Lerp(startPos, targetPos, easedTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rectTransform.anchoredPosition = targetPos;
        _textPositionCoroutines.Remove(rectTransform);
    }

    private IEnumerator LerpHoverEffect(Vector2 targetSize, Vector2 targetPosition)
    {
        Vector2 startSize = _rectTransform.sizeDelta;
        Vector2 startPosition = _rectTransform.anchoredPosition;

        float elapsedTime = 0f;
        while (elapsedTime < _effectDuration)
        {
            float time = elapsedTime / _effectDuration;
            float easedTime = Mathf.SmoothStep(0, 1, time);

            _rectTransform.sizeDelta = Vector2.Lerp(startSize, targetSize, easedTime);
            _rectTransform.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, easedTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _rectTransform.sizeDelta = targetSize;
        _rectTransform.anchoredPosition = targetPosition;
        _hoverEffectCoroutine = null;
    }


}
