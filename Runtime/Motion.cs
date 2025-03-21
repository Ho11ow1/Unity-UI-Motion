using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/* --------------------------------------------------------
 * Unity UI Motion - Base Animation Component
 * Created by Hollow1
 * 
 * A base class for UI animation components providing
 * common functionality for internal animations.
 * 
 * Version: 2.0.0
 * GitHub: https://github.com/Hollow1/Unity-UI-Motion
 * -------------------------------------------------------- */

public class Motion : MonoBehaviour
{
    public enum TransitionTarget
    {
        Text,
        Image,
        Panel,
        All
    }

    public enum EasingType
    {
        Linear,
        Cubic,
        EaseIn, // Quadratic
        EaseOut, // Quadratic
        EaseInOut // Quadratic
    }

    // Class variables
    private Fade fadeComponent;
    private Transition transitionComponent;
    private Scale scalingComponent;
    private Rotate rotationComponent;
    private TypeWrite typeWriterComponent;

    // Component variables
    private CanvasGroup group;
    private TextMeshProUGUI text;
    private Image image;
    private RectTransform panel;

    // Global constant
    protected const float defaultDuration = 0.5f;

    void Awake()
    {
        group = GetComponent<CanvasGroup>() ?? gameObject.AddComponent<CanvasGroup>();

        text = GetComponentInChildren<TextMeshProUGUI>();

        image = null;
        foreach (RectTransform child in transform) // transform: Direct access to children
        {
            if (child.TryGetComponent<Image>(out image)) { break; }
        }

        panel = GetComponent<RectTransform>();

        #if UNITY_EDITOR
        if (group == null) { Debug.LogWarning($"[{gameObject.name}] No CanvasGroup component found, added automatically."); }
        if (text == null) { Debug.LogWarning($"[{gameObject.name}] No Text component found in children. Parent: [{transform.parent.name ?? "none"}]"); }
        if (image == null) { Debug.LogWarning($"[{gameObject.name}] No Image component found in children. Parent: [{transform.parent.name ?? "none"}]"); }
        if (panel == null) { Debug.LogWarning($"[{gameObject.name}] No RectTransform component found."); }
        #endif

        fadeComponent = new Fade(group, this);
        transitionComponent = new Transition(text, image, panel, this);
        scalingComponent = new Scale(text, image, panel, this);
        rotationComponent = new Rotate(text, image, panel, this);
        typeWriterComponent = new TypeWrite(text, this);
    }

    // ----------------------------------------------------- Fade API -----------------------------------------------------

    /// <summary>
    /// Immediately sets the UI element to be invisible without an animation
    /// </summary>
    public void TurnInvisible()
    {
        fadeComponent.TurnInvisible();
    }

    /// <summary>
    /// Immediately sets the UI element to be visible without an animation
    /// </summary>
    public void TurnVisible()
    {
        fadeComponent.TurnVisible();
    }

    /// <summary>
    /// Fades in the UI element with a custom delay and duration
    /// </summary>
    /// <param name="delay">Time in seconds to wait before starting the fade</param>
    /// <param name="duration">Time in seconds the fade animation should take</param>
    public void FadeIn(float delay = 0f, float duration = defaultDuration)
    {
        fadeComponent.FadeIn(delay, duration);
    }

    /// <summary>
    /// Fades out the UI element with a custom delay and duration
    /// </summary>
    /// <param name="delay">Time in seconds to wait before starting the fade</param>
    /// <param name="duration">Time in seconds the fade animation should take</param>
    public void FadeOut(float delay = 0f, float duration = defaultDuration)
    {
        fadeComponent.FadeOut(delay, duration);
    }

    // ----------------------------------------------------- Transition API -----------------------------------------------------

    /// <summary>
    /// Transitions the UI element up using the default duration and delay
    /// </summary>
    /// <param name="target">Target component to transition (Text, Image, Panel, or All)</param>
    /// <param name="offset">Offset in pixels (or units depending on canvas scaling). Positive values move the element down by this amount before animating to its original position.</param>
    /// <param name="easing">Specifies the easing method the transition should use</param>
    /// <param name="duration">Time in seconds for the transition duration</param>
    /// <param name="delay">Time in seconds to wait before starting the transition</param>
    public void TransitionUp(TransitionTarget target, float offset, EasingType easing = EasingType.Linear, float duration = defaultDuration, float delay = 0f)
    {
        transitionComponent.TransitionUp(target, offset, easing, duration, delay);
    }

    /// <summary>
    /// Transitions the UI element down using the default duration and delay
    /// </summary>
    /// <param name="target">Target component to transition (Text, Image, Panel, or All)</param>
    /// <param name="offset">Offset in pixels (or units depending on canvas scaling). Positive values move the element up by this amount before animating to its original position.</param>
    /// <param name="easing">Specifies the easing method the transition should use</param>
    /// <param name="duration">Time in seconds for the transition duration</param>
    /// <param name="delay">Time in seconds to wait before starting the transition</param>
    public void TransitionDown(TransitionTarget target, float offset, EasingType easing = EasingType.Linear, float duration = defaultDuration, float delay = 0f)
    {
        transitionComponent.TransitionDown(target, offset, easing, duration, delay);
    }

    /// <summary>
    /// Transitions the UI element from right to left using the default duration and delay
    /// </summary>
    /// <param name="target">Target component to transition (Text, Image, Panel, or All)</param>
    /// <param name="offset">Offset in pixels (or units depending on canvas scaling). Positive values move the element to the right by this amount before animating to its original position.</param>
    /// <param name="easing">Specifies the easing method the transition should use</param>
    /// <param name="duration">Time in seconds for the transition duration</param>
    /// <param name="delay">Time in seconds to wait before starting the transition</param>
    public void TransitionLeft(TransitionTarget target, float offset, EasingType easing = EasingType.Linear, float duration = defaultDuration, float delay = 0f)
    {
        transitionComponent.TransitionLeft(target, offset, easing, duration, delay);
    }

    /// <summary>
    /// Transitions the UI element from left to right using the default duration and delay
    /// </summary>
    /// <param name="target">Target component to transition (Text, Image, Panel, or All)</param>
    /// <param name="offset">Offset in pixels (or units depending on canvas scaling). Positive values move the element to the left by this amount before animating to its original position.</param>
    /// <param name="easing">Specifies the easing method the transition should use</param>
    /// <param name="duration">Time in seconds for the transition duration</param>
    /// <param name="delay">Time in seconds to wait before starting the transition</param>
    public void TransitionRight(TransitionTarget target, float offset, EasingType easing = EasingType.Linear, float duration = defaultDuration, float delay = 0f)
    {
        transitionComponent.TransitionRight(target, offset, easing, duration, delay);
    }

    /// <summary>
    /// Transitions the UI element on both axis based on the offset
    /// </summary>
    /// <param name="target">Target component to transition (Text, Image, Panel, or All)</param>
    /// <param name="offset">Offset in pixels (or units depending on canvas scaling). Positive values move the element to the right and up by this amount before animating to its original position.</param>
    /// <param name="easing">Specifies the easing method the transition should use</param>
    /// <param name="duration">Time in seconds for the transition duration</param>
    /// <param name="delay">Time in seconds to wait before starting the transition</param>
    public void TransitionPosition(TransitionTarget target, Vector2 offset, EasingType easing = EasingType.Linear, float duration = defaultDuration, float delay = 0f)
    {
        transitionComponent.TransitionPosition(target, offset, easing, duration, delay);
    }

    // ----------------------------------------------------- Rotation API -----------------------------------------------------

    /// <summary>
    /// Rotate the UI element with a custom delay and duration
    /// </summary>
    /// <param name="target">Target component to transition (Text, Image, Panel, or All)</param>
    /// <param name="degrees">Degrees the rotation should rotate, positive values go clockwise, negative counter-clockwise</param>
    /// <param name="easing">Specifies the easing method the transition should use</param>
    /// <param name="duration">Time in seconds for the rotation duration</param>
    /// <param name="delay">Time in seconds to wait before starting the transition</param>
    public void Rotate(TransitionTarget target, float degrees, EasingType easing = EasingType.Linear, float duration = defaultDuration, float delay = 0f)
    {
        rotationComponent.RotateUi(target, degrees, easing, duration, delay);
    }

    // ----------------------------------------------------- Scaling API -----------------------------------------------------

    /// <summary>
    /// Scales up the UI element with a custom delay and duration
    /// </summary>
    /// <param name="target">Target component to scale (Text, Image, Panel, or All)</param>
    /// <param name="multiplier">Scale multiplier. Values greater than 1 increase size, must be greater than 0. (Scale is based on the local scale of the parent)</param>
    /// <param name="easing">Specifies the easing method the scaling should use</param>
    /// <param name="duration">Time in seconds the scaling animation should take</param>
    /// <param name="delay">Time in seconds to wait before starting the scaling</param>
    public void ScaleUp(TransitionTarget target, float multiplier, EasingType easing = EasingType.Linear, float duration = defaultDuration, float delay = 0f)
    {
        scalingComponent.ScaleUp(target, multiplier, easing, duration, delay);
    }

    /// <summary>
    /// Scales down the UI element with a custom delay and duration
    /// </summary>
    /// <param name="target">Target component to scale (Text, Image, Panel, or All)</param>
    /// <param name="multiplier">Scale multiplier. Values greater than 1 decrease size, must be greater than 0. (Scale is based on the local scale of the parent)</param>
    /// <param name="easing">Specifies the easing method the scaling should use</param>
    /// <param name="duration">Time in seconds the scaling animation should take</param>
    /// <param name="delay">Time in seconds to wait before starting the scaling</param>
    public void ScaleDown(TransitionTarget target, float multiplier, EasingType easing = EasingType.Linear, float duration = defaultDuration, float delay = 0f)
    {
        scalingComponent.ScaleDown(target, multiplier, easing, duration, delay);
    }

    // ----------------------------------------------------- TypeWriter API -----------------------------------------------------

    /// <summary>
    /// Applies a typeWriter effect to the TextMeshPro component with a custom delay and duration
    /// </summary>
    /// <param name="delay">Time in seconds for the delay per character</param>
    /// <param name="duration">Time in seconds for the entire text to animate</param>
    public void TypeWrite(float delay = 0.3f, float duration = 3f)
    {
        typeWriterComponent.TypeWriter(delay, duration);
    }
}

internal static class Easing
{
    internal static float SetEasingFunction(float time, Motion.EasingType easing)
    {
        switch (easing)
        {
            case Motion.EasingType.Linear:
                return time;
            case Motion.EasingType.Cubic:
                return time * time * time;
            case Motion.EasingType.EaseIn:
                return time * time;
            case Motion.EasingType.EaseOut:
                return time * (2 - time);
            case Motion.EasingType.EaseInOut:
                return time < 0.5f ? 2 * time * time : -1 + (4 - 2 * time) * time;
            default:
                return time;
        }
    }
}