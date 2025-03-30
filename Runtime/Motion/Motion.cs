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
 * common functionality for internal classes.
 * 
 * Version: 2.1.0
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
    private CanvasGroup cg;
    private TextMeshProUGUI text;
    private Image image;
    private RectTransform panel;

    // Global constant
    protected const float defaultDuration = 0.5f;

    void Awake()
    {
        cg = GetComponent<CanvasGroup>() ?? gameObject.AddComponent<CanvasGroup>();

        text = GetComponentInChildren<TextMeshProUGUI>(); // Maybe convert into array for better multi processing

        image = null; // Maybe convert into array for better multi processing
        foreach (RectTransform child in transform)
        {
            if (child.TryGetComponent<Image>(out image)) { break; }
        }

        panel = GetComponent<RectTransform>();

        #if UNITY_EDITOR
        if (cg == null) { Debug.LogWarning($"[{gameObject.name}] No CanvasGroup component found, added automatically."); }
        if (text == null) { Debug.LogWarning($"[{gameObject.name}] No Text component found in children. Parent: [{transform.parent.name ?? "none"}]"); }
        if (image == null) { Debug.LogWarning($"[{gameObject.name}] No Image component found in children. Parent: [{transform.parent.name ?? "none"}]"); }
        if (panel == null) { Debug.LogWarning($"[{gameObject.name}] No RectTransform component found."); }
        #endif

        fadeComponent = new Fade(cg, this);
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
    /// Transitions the UI element from a position offset upward back to its starting position
    /// </summary>
    /// <param name="target">Target component to transition (Text, Image, Panel, or All)</param>
    /// <param name="offset">Offset in pixels (or units depending on canvas scaling and render mode). Positive values move the element up</param>
    /// <param name="easing">Specifies the easing method the transition should use</param>
    /// <param name="duration">Time in seconds for the transition duration</param>
    /// <param name="delay">Time in seconds to wait before starting the transition</param>
    public void TransitionFromUp(TransitionTarget target, float offset, EasingType easing = EasingType.Linear, float duration = defaultDuration, float delay = 0f)
    {
        transitionComponent.TransitionFromUp(target, offset, easing, duration, delay);
    }

    /// <summary>
    /// Transitions the UI element from a position offset downward back to its starting position
    /// </summary>
    /// <param name="target">Target component to transition (Text, Image, Panel, or All)</param>
    /// <param name="offset">Offset in pixels (or units depending on canvas scaling and render mode). Positive values move the element down</param>
    /// <param name="easing">Specifies the easing method the transition should use</param>
    /// <param name="duration">Time in seconds for the transition duration</param>
    /// <param name="delay">Time in seconds to wait before starting the transition</param>
    public void TransitionFromDown(TransitionTarget target, float offset, EasingType easing = EasingType.Linear, float duration = defaultDuration, float delay = 0f)
    {
        transitionComponent.TransitionFromDown(target, offset, easing, duration, delay);
    }

    /// <summary>
    /// Transitions the UI element from a position offset to the left back to its starting position
    /// </summary>
    /// <param name="target">Target component to transition (Text, Image, Panel, or All)</param>
    /// <param name="offset">Offset in pixels (or units depending on canvas scaling and render mode). Positive values move the element to the left</param>
    /// <param name="easing">Specifies the easing method the transition should use</param>
    /// <param name="duration">Time in seconds for the transition duration</param>
    /// <param name="delay">Time in seconds to wait before starting the transition</param>
    public void TransitionFromLeft(TransitionTarget target, float offset, EasingType easing = EasingType.Linear, float duration = defaultDuration, float delay = 0f)
    {
        transitionComponent.TransitionFromLeft(target, offset, easing, duration, delay);
    }

    /// <summary>
    /// Transitions the UI element from a position offset to the right back to its starting position
    /// </summary>
    /// <param name="target">Target component to transition (Text, Image, Panel, or All)</param>
    /// <param name="offset">Offset in pixels (or units depending on canvas scaling and render mode). Positive values move the element to the right</param>
    /// <param name="easing">Specifies the easing method the transition should use</param>
    /// <param name="duration">Time in seconds for the transition duration</param>
    /// <param name="delay">Time in seconds to wait before starting the transition</param>
    public void TransitionFromRight(TransitionTarget target, float offset, EasingType easing = EasingType.Linear, float duration = defaultDuration, float delay = 0f)
    {
        transitionComponent.TransitionFromRight(target, offset, easing, duration, delay);
    }

    /// <summary>
    /// Transitions the UI element from an offset position on both axes back to its starting position
    /// </summary>
    /// <param name="target">Target component to transition (Text, Image, Panel, or All)</param>
    /// <param name="offset">Offset in pixels (or units depending on canvas scaling and render mode). determines the starting offset position to animate from, Positive values offset right and up</param>
    /// <param name="easing">Specifies the easing method the transition should use</param>
    /// <param name="duration">Time in seconds for the transition duration</param>
    /// <param name="delay">Time in seconds to wait before starting the transition</param>
    public void TransitionFromPosition(TransitionTarget target, Vector2 offset, EasingType easing = EasingType.Linear, float duration = defaultDuration, float delay = 0f)
    {
        transitionComponent.TransitionFromPosition(target, offset, easing, duration, delay);
    }

    /// <summary>
    /// Transitions the UI element from its starting position to a position offset upward
    /// </summary>
    /// <param name="target">Target component to transition (Text, Image, Panel, or All)</param>
    /// <param name="offset">Offset in pixels (or units depending on canvas scaling and render mode). Positive values move the element up</param>
    /// <param name="easing">Specifies the easing method the transition should use</param>
    /// <param name="duration">Time in seconds for the transition duration</param>
    /// <param name="delay">Time in seconds to wait before starting the transition</param>
    public void TransitionToUp(TransitionTarget target, float offset, EasingType easing = EasingType.Linear, float duration = defaultDuration, float delay = 0f)
    {
        transitionComponent.TransitionToUp(target, offset, easing, duration, delay);
    }

    /// <summary>
    /// Transitions the UI element from its starting position to a position offset downward
    /// </summary>
    /// <param name="target">Target component to transition (Text, Image, Panel, or All)</param>
    /// <param name="offset">Offset in pixels (or units depending on canvas scaling and render mode). Positive values move the element down</param>
    /// <param name="easing">Specifies the easing method the transition should use</param>
    /// <param name="duration">Time in seconds for the transition duration</param>
    /// <param name="delay">Time in seconds to wait before starting the transition</param>
    public void TransitionToDown(TransitionTarget target, float offset, EasingType easing = EasingType.Linear, float duration = defaultDuration, float delay = 0f)
    {
        transitionComponent.TransitionToDown(target, offset, easing, duration, delay);
    }

    /// <summary>
    /// Transitions the UI element from its starting position to a position offset to the left
    /// </summary>
    /// <param name="target">Target component to transition (Text, Image, Panel, or All)</param>
    /// <param name="offset">Offset in pixels (or units depending on canvas scaling and render mode). Positive values move the element to the left</param>
    /// <param name="easing">Specifies the easing method the transition should use</param>
    /// <param name="duration">Time in seconds for the transition duration</param>
    /// <param name="delay">Time in seconds to wait before starting the transition</param>
    public void TransitionToLeft(TransitionTarget target, float offset, EasingType easing = EasingType.Linear, float duration = defaultDuration, float delay = 0f)
    {
        transitionComponent.TransitionToLeft(target, offset, easing, duration, delay);
    }

    /// <summary>
    /// Transitions the UI element from its starting position to a position offset to the right
    /// </summary>
    /// <param name="target">Target component to transition (Text, Image, Panel, or All)</param>
    /// <param name="offset">Offset in pixels (or units depending on canvas scaling and render mode). Positive values move the element to the right</param>
    /// <param name="easing">Specifies the easing method the transition should use</param>
    /// <param name="duration">Time in seconds for the transition duration</param>
    /// <param name="delay">Time in seconds to wait before starting the transition</param>
    public void TransitionToRight(TransitionTarget target, float offset, EasingType easing = EasingType.Linear, float duration = defaultDuration, float delay = 0f)
    {
        transitionComponent.TransitionToRight(target, offset, easing, duration, delay);
    }

    /// <summary>
    /// Transitions the UI element from its starting position to an offset position
    /// </summary>
    /// <param name="target">Target component to transition (Text, Image, Panel, or All)</param>
    /// <param name="offset">Offset in pixels (or units depending on canvas scaling and render mode). determines the final offset position to animate to, Positive values offset right and up</param>
    /// <param name="easing">Specifies the easing method the transition should use</param>
    /// <param name="duration">Time in seconds for the transition duration</param>
    /// <param name="delay">Time in seconds to wait before starting the transition</param>
    public void TransitionToPosition(TransitionTarget target, Vector2 offset, EasingType easing = EasingType.Linear, float duration = defaultDuration, float delay = 0f)
    {
        transitionComponent.TransitionToPosition(target, offset, easing, duration, delay);
    }

    // ----------------------------------------------------- Rotation API -----------------------------------------------------

    /// <summary>
    /// Rotate the UI element with a custom delay and duration
    /// </summary>
    /// <param name="target">Target component to transition (Text, Image, Panel, or All)</param>
    /// <param name="degrees">Degrees the rotation should rotate, positive values go counter-clockwise, negative clockwise</param>
    /// <param name="easing">Specifies the easing method the transition should use</param>
    /// <param name="duration">Time in seconds for the rotation duration</param>
    /// <param name="delay">Time in seconds to wait before starting the transition</param>
    public void Rotate(TransitionTarget target, float degrees, EasingType easing = EasingType.Linear, float duration = defaultDuration, float delay = 0f)
    {
        rotationComponent.Rotation(target, degrees, easing, duration, delay);
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