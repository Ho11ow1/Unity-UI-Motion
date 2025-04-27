using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

/* --------------------------------------------------------
 * Unity UI Motion - Base Animation Component
 * Created by Hollow1
 * 
 * A base class for UI animation components providing
 * common functionality for internal classes.
 * 
 * Version: 2.3.1
 * GitHub: https://github.com/Hollow1/Unity-UI-Motion
 * -------------------------------------------------------- */

#pragma warning disable IDE0090 // Use 'new'
#pragma warning disable IDE1006 // Naming Styles
public class Motion : MonoBehaviour
{
    public static event UnityAction fadeStart;
    public static event UnityAction fadeEnd;
    public static event UnityAction transitionStart;
    public static event UnityAction transitionEnd;
    public static event UnityAction scaleStart;
    public static event UnityAction scaleEnd;
    public static event UnityAction rotateStart;
    public static event UnityAction rotateEnd;
    public static event UnityAction typeWriteStart;
    public static event UnityAction typeWriteEnd;

    public enum AnimationTarget
    {
        Panel,
        Text,
        Image,
        Button
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
    private Rotate rotationComponent;
    private Scale scalingComponent;
    private TypeWrite typeWriterComponent;

    // Component variables
    private CanvasGroup _cg;
    private RectTransform _panel;
    private TextMeshProUGUI[] _texts;
    private Image[] _images;
    private Button[] _buttons;

    // Internal constants
    internal const int panelIndex = 0;
    internal const int textIndex = 1;
    internal const int imageIndex = 2;
    internal const int buttonIndex = 3;
    internal const float defaultDuration = 0.5f;

    void Awake()
    {
        _cg = GetComponent<CanvasGroup>();

        _texts = GetComponentsInChildren<TextMeshProUGUI>();

        var imageList = new List<Image>();
        foreach (RectTransform child in transform)
        {
            if (child.TryGetComponent<Image>(out var img))
            {
                imageList.Add(img);
            }
        }
        _images = imageList.ToArray();

        _buttons = GetComponentsInChildren<Button>();

        _panel = GetComponent<RectTransform>();

        #if UNITY_EDITOR
        if (_cg == null) { Debug.LogWarning($"[{gameObject.name}] No CanvasGroup component found."); }
        if (_texts == null) { Debug.LogWarning($"[{gameObject.name}] No Text component found in children. Parent: [{transform.parent.name ?? "none"}]"); }
        if (_images == null) { Debug.LogWarning($"[{gameObject.name}] No Image component found in children. Parent: [{transform.parent.name ?? "none"}]"); }
        if (_buttons == null) { Debug.LogWarning($"[{gameObject.name}] No Button component found in children. Parent: [{transform.parent.name ?? "none"}]"); }
        if (_panel == null) { Debug.LogWarning($"[{gameObject.name}] No RectTransform component found."); }
        #endif

        fadeComponent = new Fade(_texts, _images, _buttons, _cg, this);
        transitionComponent = new Transition(_texts, _images, _buttons, _panel, this);
        rotationComponent = new Rotate(_texts, _images, _buttons, _panel, this);
        scalingComponent = new Scale(_texts, _images, _buttons, _panel, this);
        typeWriterComponent = new TypeWrite(_texts, this);
    }

    // ----------------------------------------------------- Fade API -----------------------------------------------------

    /// <summary>
    /// Immediately sets the UI panel visibility
    /// </summary>
    /// <param name="visible">Sets the panel visibility condition</param>
    public void SetPanelVisibility(bool visible)
    {
        fadeComponent.SetPanelVisibility(visible);
    }

    /// <summary>
    /// Fades in the UI element
    /// </summary>
    /// <param name="target">Target component to transition (Panel, Text, Image, Button)</param>
    /// <param name="occurrence">Specifies the instance of the target element</param>
    /// <param name="duration">Time in seconds for the fading duration</param>
    /// <param name="delay">Time in seconds to wait before starting the fade</param>
    public void FadeIn(AnimationTarget target, int occurrence, float duration = defaultDuration, float delay = 0f)
    {
        occurrence -= 1;
        fadeComponent.FadeIn(target, occurrence, duration, delay, fadeStart, fadeEnd);
    }

    /// <summary>
    /// Fades out the UI element
    /// </summary>
    /// <param name="target">Target component to transition (Panel, Text, Image, Button)</param>
    /// <param name="occurrence">Specifies the instance of the target element</param>
    /// <param name="duration">Time in seconds for the fading duration</param>
    /// <param name="delay">Time in seconds to wait before starting the fade</param>
    public void FadeOut(AnimationTarget target, int occurrence, float duration = defaultDuration, float delay = 0f)
    {
        occurrence -= 1;
        fadeComponent.FadeOut(target, occurrence, duration, delay, fadeStart, fadeEnd);
    }

    // ----------------------------------------------------- Transition API -----------------------------------------------------

    /// <summary>
    /// Transitions the UI element from a position offset upward back to its starting position
    /// </summary>
    /// <param name="target">Target component to transition (Panel, Text, Image, Button)</param>
    /// <param name="occurrence">Specifies the instance of the target element</param>
    /// <param name="offset">Offset in pixels (or units depending on canvas scaling and render mode). Positive values move the element up</param>
    /// <param name="easing">Specifies the easing method the transition should use</param>
    /// <param name="duration">Time in seconds for the transition duration</param>
    /// <param name="delay">Time in seconds to wait before starting the transition</param>
    public void TransitionFromUp(AnimationTarget target, int occurrence, float offset, EasingType easing = EasingType.Linear, float duration = defaultDuration, float delay = 0f)
    {
        occurrence -= 1;
        transitionComponent.TransitionFromUp(target, occurrence, offset, easing, duration, delay, transitionStart, transitionEnd);
    }

    /// <summary>
    /// Transitions the UI element from a position offset downward back to its starting position
    /// </summary>
    /// <param name="target">Target component to transition (Panel, Text, Image, Button)</param>
    /// <param name="occurrence">Specifies the instance of the target element</param>
    /// <param name="offset">Offset in pixels (or units depending on canvas scaling and render mode). Positive values move the element down</param>
    /// <param name="easing">Specifies the easing method the transition should use</param>
    /// <param name="duration">Time in seconds for the transition duration</param>
    /// <param name="delay">Time in seconds to wait before starting the transition</param>
    public void TransitionFromDown(AnimationTarget target, int occurrence, float offset, EasingType easing = EasingType.Linear, float duration = defaultDuration, float delay = 0f)
    {
        occurrence -= 1;
        transitionComponent.TransitionFromDown(target, occurrence, offset, easing, duration, delay, transitionStart, transitionEnd);
    }

    /// <summary>
    /// Transitions the UI element from a position offset to the left back to its starting position
    /// </summary>
    /// <param name="target">Target component to transition (Panel, Text, Image, Button)</param>
    /// <param name="occurrence">Specifies the instance of the target element</param>
    /// <param name="offset">Offset in pixels (or units depending on canvas scaling and render mode). Positive values move the element to the left</param>
    /// <param name="easing">Specifies the easing method the transition should use</param>
    /// <param name="duration">Time in seconds for the transition duration</param>
    /// <param name="delay">Time in seconds to wait before starting the transition</param>
    public void TransitionFromLeft(AnimationTarget target, int occurrence, float offset, EasingType easing = EasingType.Linear, float duration = defaultDuration, float delay = 0f)
    {
        occurrence -= 1;
        transitionComponent.TransitionFromLeft(target, occurrence, offset, easing, duration, delay, transitionStart, transitionEnd);
    }

    /// <summary>
    /// Transitions the UI element from a position offset to the right back to its starting position
    /// </summary>
    /// <param name="target">Target component to transition (Panel, Text, Image, Button)</param>
    /// <param name="occurrence">Specifies the instance of the target element</param>
    /// <param name="offset">Offset in pixels (or units depending on canvas scaling and render mode). Positive values move the element to the right</param>
    /// <param name="easing">Specifies the easing method the transition should use</param>
    /// <param name="duration">Time in seconds for the transition duration</param>
    /// <param name="delay">Time in seconds to wait before starting the transition</param>
    public void TransitionFromRight(AnimationTarget target, int occurrence, float offset, EasingType easing = EasingType.Linear, float duration = defaultDuration, float delay = 0f)
    {
        occurrence -= 1;
        transitionComponent.TransitionFromRight(target, occurrence, offset, easing, duration, delay, transitionStart, transitionEnd);
    }

    /// <summary>
    /// Transitions the UI element from an offset position on both axes back to its starting position
    /// </summary>
    /// <param name="target">Target component to transition (Panel, Text, Image, Button)</param>
    /// <param name="occurrence">Specifies the instance of the target element</param>
    /// <param name="offset">Offset in pixels (or units depending on canvas scaling and render mode). determines the starting offset position to animate from, Positive values offset right and up</param>
    /// <param name="easing">Specifies the easing method the transition should use</param>
    /// <param name="duration">Time in seconds for the transition duration</param>
    /// <param name="delay">Time in seconds to wait before starting the transition</param>
    public void TransitionFromPosition(AnimationTarget target, int occurrence, Vector2 offset, EasingType easing = EasingType.Linear, float duration = defaultDuration, float delay = 0f)
    {
        occurrence -= 1;
        transitionComponent.TransitionFromPosition(target, occurrence, offset, easing, duration, delay, transitionStart, transitionEnd);
    }

    /// <summary>
    /// Transitions the UI element from its starting position to a position offset upward
    /// </summary>
    /// <param name="target">Target component to transition (Panel, Text, Image, Button)</param>
    /// <param name="occurrence">Specifies the instance of the target element</param>
    /// <param name="offset">Offset in pixels (or units depending on canvas scaling and render mode). Positive values move the element up</param>
    /// <param name="easing">Specifies the easing method the transition should use</param>
    /// <param name="duration">Time in seconds for the transition duration</param>
    /// <param name="delay">Time in seconds to wait before starting the transition</param>
    public void TransitionToUp(AnimationTarget target, int occurrence, float offset, EasingType easing = EasingType.Linear, float duration = defaultDuration, float delay = 0f)
    {
        occurrence -= 1;
        transitionComponent.TransitionToUp(target, occurrence, offset, easing, duration, delay, transitionStart, transitionEnd);
    }

    /// <summary>
    /// Transitions the UI element from its starting position to a position offset downward
    /// </summary>
    /// <param name="target">Target component to transition (Panel, Text, Image, Button)</param>
    /// <param name="occurrence">Specifies the instance of the target element</param>
    /// <param name="offset">Offset in pixels (or units depending on canvas scaling and render mode). Positive values move the element down</param>
    /// <param name="easing">Specifies the easing method the transition should use</param>
    /// <param name="duration">Time in seconds for the transition duration</param>
    /// <param name="delay">Time in seconds to wait before starting the transition</param>
    public void TransitionToDown(AnimationTarget target, int occurrence, float offset, EasingType easing = EasingType.Linear, float duration = defaultDuration, float delay = 0f)
    {
        occurrence -= 1;
        transitionComponent.TransitionToDown(target, occurrence, offset, easing, duration, delay, transitionStart, transitionEnd);
    }

    /// <summary>
    /// Transitions the UI element from its starting position to a position offset to the left
    /// </summary>
    /// <param name="target">Target component to transition (Panel, Text, Image, Button)</param>
    /// <param name="occurrence">Specifies the instance of the target element</param>
    /// <param name="offset">Offset in pixels (or units depending on canvas scaling and render mode). Positive values move the element to the left</param>
    /// <param name="easing">Specifies the easing method the transition should use</param>
    /// <param name="duration">Time in seconds for the transition duration</param>
    /// <param name="delay">Time in seconds to wait before starting the transition</param>
    public void TransitionToLeft(AnimationTarget target, int occurrence, float offset, EasingType easing = EasingType.Linear, float duration = defaultDuration, float delay = 0f)
    {
        occurrence -= 1;
        transitionComponent.TransitionToLeft(target, occurrence, offset, easing, duration, delay, transitionStart, transitionEnd);
    }

    /// <summary>
    /// Transitions the UI element from its starting position to a position offset to the right
    /// </summary>
    /// <param name="target">Target component to transition (Panel, Text, Image, Button)</param>
    /// <param name="occurrence">Specifies the instance of the target element</param>
    /// <param name="offset">Offset in pixels (or units depending on canvas scaling and render mode). Positive values move the element to the right</param>
    /// <param name="easing">Specifies the easing method the transition should use</param>
    /// <param name="duration">Time in seconds for the transition duration</param>
    /// <param name="delay">Time in seconds to wait before starting the transition</param>
    public void TransitionToRight(AnimationTarget target, int occurrence, float offset, EasingType easing = EasingType.Linear, float duration = defaultDuration, float delay = 0f)
    {
        occurrence -= 1;
        transitionComponent.TransitionToRight(target, occurrence, offset, easing, duration, delay, transitionStart, transitionEnd);
    }

    /// <summary>
    /// Transitions the UI element from its starting position to an offset position
    /// </summary>
    /// <param name="target">Target component to transition (Panel, Text, Image, Button)</param>
    /// <param name="occurrence">Specifies the instance of the target element</param>
    /// <param name="offset">Offset in pixels (or units depending on canvas scaling and render mode). determines the final offset position to animate to, Positive values offset right and up</param>
    /// <param name="easing">Specifies the easing method the transition should use</param>
    /// <param name="duration">Time in seconds for the transition duration</param>
    /// <param name="delay">Time in seconds to wait before starting the transition</param>
    public void TransitionToPosition(AnimationTarget target, int occurrence, Vector2 offset, EasingType easing = EasingType.Linear, float duration = defaultDuration, float delay = 0f)
    {
        occurrence -= 1;
        transitionComponent.TransitionToPosition(target, occurrence, offset, easing, duration, delay, transitionStart, transitionEnd);
    }

    // ----------------------------------------------------- Rotation API -----------------------------------------------------

    /// <summary>
    /// Rotate the UI element with a custom delay and duration
    /// </summary>
    /// <param name="target">Target component to transition (Panel, Text, Image, Button)</param>
    /// <param name="occurrence">Specifies the instance of the target element</param>
    /// <param name="degrees">Degrees the rotation should rotate, positive values go counter-clockwise, negative clockwise</param>
    /// <param name="easing">Specifies the easing method the transition should use</param>
    /// <param name="duration">Time in seconds for the rotation duration</param>
    /// <param name="delay">Time in seconds to wait before starting the transition</param>
    public void Rotate(AnimationTarget target, int occurrence, float degrees, EasingType easing = EasingType.Linear, float duration = defaultDuration, float delay = 0f)
    {
        occurrence -= 1;
        rotationComponent.Rotation(target, occurrence, degrees, easing, duration, delay, rotateStart, rotateEnd);
    }

    // ----------------------------------------------------- Scaling API -----------------------------------------------------

    /// <summary>
    /// Scales up the UI element with a custom delay and duration
    /// </summary>
    /// <param name="target">Target component to scale (Panel, Text, Image, Button)</param>
    /// <param name="occurrence">Specifies the instance of the target element</param>
    /// <param name="multiplier">Scale multiplier. Values greater than 1 increase size, must be greater than 0. (Scale is based on the local scale of the parent)</param>
    /// <param name="easing">Specifies the easing method the scaling should use</param>
    /// <param name="duration">Time in seconds the scaling animation should take</param>
    /// <param name="delay">Time in seconds to wait before starting the scaling</param>
    public void ScaleUp(AnimationTarget target, int occurrence, float multiplier, EasingType easing = EasingType.Linear, float duration = defaultDuration, float delay = 0f)
    {
        occurrence -= 1;
        scalingComponent.ScaleUp(target, occurrence, multiplier, easing, duration, delay, scaleStart, scaleEnd);
    }

    /// <summary>
    /// Scales down the UI element with a custom delay and duration
    /// </summary>
    /// <param name="target">Target component to scale (Panel, Text, Image, Button)</param>
    /// <param name="occurrence">Specifies the instance of the target element</param>
    /// <param name="multiplier">Scale multiplier. Values greater than 1 decrease size, must be greater than 0. (Scale is based on the local scale of the parent)</param>
    /// <param name="easing">Specifies the easing method the scaling should use</param>
    /// <param name="duration">Time in seconds the scaling animation should take</param>
    /// <param name="delay">Time in seconds to wait before starting the scaling</param>
    public void ScaleDown(AnimationTarget target, int occurrence, float multiplier, EasingType easing = EasingType.Linear, float duration = defaultDuration, float delay = 0f)
    {
        occurrence -= 1;
        scalingComponent.ScaleDown(target, occurrence, multiplier, easing, duration, delay, scaleStart, scaleEnd);
    }

    // ----------------------------------------------------- TypeWriter API -----------------------------------------------------

    /// <summary>
    /// Applies a typeWriter effect to the TextMeshPro component with a custom delay and duration
    /// </summary>
    /// <param name="occurrence">Specifies the instance of the target element</param>
    /// <param name="delay">Time in seconds for the delay per character</param>
    /// <param name="duration">Time in seconds for the entire text to animate</param>
    public void TypeWrite(int occurrence, float delay = 0.3f, float duration = 3f)
    {
        occurrence -= 1;
        typeWriterComponent.TypeWriter(occurrence, delay, duration, typeWriteStart, typeWriteEnd);
    }


}
