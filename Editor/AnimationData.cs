using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
internal class AnimationData
{
    internal string animation = "Animation";
    internal float duration = 0f;
    internal float delay = 0f;
    internal float offset = 0f;
    internal Vector2 vector = new Vector2(0, 0);
    internal float degrees = 0f;
    internal float multiplier = 0f;

    internal const string _animationText = "Animation";
    internal const string _fadeText = "Fade";
    internal const string _transitionText = "Transition";
    internal const string _scaleText = "Scale";
    internal const string _rotateText = "Rotate";
    internal const string _typeWriteText = "TypeWrite";

    // -------------------------------------------------------------- DATA SETTERS --------------------------------------------------------------
    // Being used in internal static methods so it's necessary to duplicate
    private static void InitializePanelData(GameObject panel, Dictionary<GameObject, Dictionary<int, Dictionary<RectTransform, AnimationData>>> panelDataMap)
    {
        if (!panelDataMap.ContainsKey(panel))
        {
            panelDataMap[panel] = new Dictionary<int, Dictionary<RectTransform, AnimationData>>
            {
                { MotionWIP._textIndex, new Dictionary<RectTransform, AnimationData>() },
                { MotionWIP._imageIndex, new Dictionary<RectTransform, AnimationData>() },
                { MotionWIP._buttonIndex, new Dictionary<RectTransform, AnimationData>() }
            };
        }
    }

    // -------------------------------------------------------------- DATA GETTERS --------------------------------------------------------------

    internal static AnimationData GetAnimationData(RectTransform rect, Dictionary<GameObject, Dictionary<int, Dictionary<RectTransform, AnimationData>>> panelDataMap)
    {
        GameObject panel = rect.transform.parent.gameObject;
        if (!panelDataMap.ContainsKey(panel))
        {
            InitializePanelData(panel, panelDataMap);
        }

        int typeIndex = -1;
        if (rect.GetComponent<TextMeshProUGUI>())
        {
            typeIndex = MotionWIP._textIndex;
        }
        else if (rect.GetComponent<Button>())
        {
            typeIndex = MotionWIP._buttonIndex;
        }
        else if (rect.GetComponent<Image>())
        {
            typeIndex = MotionWIP._imageIndex;
        }

        if (typeIndex != -1)
        {
            if (!panelDataMap[panel][typeIndex].ContainsKey(rect))
            {
                panelDataMap[panel][typeIndex][rect] = new AnimationData();
            }

            return panelDataMap[panel][typeIndex][rect];
        }

        return null;
    }

    internal static (AnimationData data, int index, string type) GetTupleAnimationData(RectTransform rect, Dictionary<GameObject, Dictionary<int, Dictionary<RectTransform, AnimationData>>> panelDataMap)
    {
        GameObject panel = rect.transform.parent.gameObject;
        if (!panelDataMap.ContainsKey(panel))
        {
            InitializePanelData(panel, panelDataMap);
        }

        string type = string.Empty;
        int typeIndex = -1;

        if (rect.GetComponent<TextMeshProUGUI>())
        {
            type = "Motion.AnimationTarget.Text";
            typeIndex = MotionWIP._textIndex;
        }
        else if (rect.GetComponent<Button>())
        {
            type = "Motion.AnimationTarget.Button";
            typeIndex = MotionWIP._buttonIndex;
        }
        else if (rect.GetComponent<Image>())
        {
            type = "Motion.AnimationTarget.Image";
            typeIndex = MotionWIP._imageIndex;
        }

        if (typeIndex != -1)
        {
            if (!panelDataMap[panel][typeIndex].ContainsKey(rect))
            {
                panelDataMap[panel][typeIndex][rect] = new AnimationData();
            }

            int index = panelDataMap[panel][typeIndex].Keys.ToList().IndexOf(rect) + 1;
            return (panelDataMap[panel][typeIndex][rect], index, type);
        }

        return (null, 0, string.Empty);
    }

    // -------------------------------------------------------------- UTILITY FUNCTIONS --------------------------------------------------------------

    internal static string GetAnimationString(AnimationData data)
    {
        var animationString = "";
        switch (data.animation)
        {
            case _fadeText + "In":
                animationString = "FadeIn";
                break;
            case _fadeText + "Out":
                animationString = "FadeOut";
                break;
            case _transitionText + "FromUp":
                animationString = "TransitionFromUp";
                break;
            case _transitionText + "FromDown":
                animationString = "TransitionFromDown";
                break;
            case _transitionText + "FromLeft":
                animationString = "TransitionFromLeft";
                break;
            case _transitionText + "FromRight":
                animationString = "TransitionFromRight";
                break;
            case _transitionText + "FromPosition":
                animationString = "TransitionFromPosition";
                break;
            case _transitionText + "ToUp":
                animationString = "TransitionToUp";
                break;
            case _transitionText + "ToDown":
                animationString = "TransitionToDown";
                break;
            case _transitionText + "ToLeft":
                animationString = "TransitionToLeft";
                break;
            case _transitionText + "ToRight":
                animationString = "TransitionToRight";
                break;
            case _transitionText + "ToPosition":
                animationString = "TransitionToPosition";
                break;
            case _rotateText:
                animationString = _rotateText;
                break;
            case _scaleText + "Up":
                animationString = "ScaleUp";
                break;
            case _scaleText + "Down":
                animationString = "ScaleDown";
                break;
            case _typeWriteText:
                animationString = _typeWriteText;
                break;
        }

        return animationString;
    }
}