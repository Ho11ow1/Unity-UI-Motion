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
    private Scale scalingComponent;
    private Rotate rotationComponent;
    private TypeWrite typeWriterComponent;

    // Component variables
    private CanvasGroup cg;
    private RectTransform panel;
    private TextMeshProUGUI[] texts;
    private Image[] images;
    private Button[] buttons;
    private List<Image> imageList = new List<Image>();

    // Internal constants
    internal const int panelIndex = 0;
    internal const int textIndex = 1;
    internal const int imageIndex = 2;
    internal const int buttonIndex = 3;

    private const float defaultDuration = 0.5f;

    void Awake()
    {
        cg = GetComponent<CanvasGroup>() ?? gameObject.AddComponent<CanvasGroup>();

        texts = GetComponentsInChildren<TextMeshProUGUI>();

        foreach (RectTransform child in transform)
        {
            Image img = child.GetComponent<Image>();
            if (img != null)
            {
                imageList.Add(img);
            }
        }
        images = imageList.ToArray();

        buttons = GetComponentsInChildren<Button>();

        panel = GetComponent<RectTransform>();

        #if UNITY_EDITOR
        if (cg == null) { Debug.LogWarning($"[{gameObject.name}] No CanvasGroup component found, added automatically."); }
        if (texts == null) { Debug.LogWarning($"[{gameObject.name}] No Text component found in children. Parent: [{transform.parent.name ?? "none"}]"); }
        if (images == null) { Debug.LogWarning($"[{gameObject.name}] No Image component found in children. Parent: [{transform.parent.name ?? "none"}]"); }
        if (buttons == null) { Debug.LogWarning($"[{gameObject.name}] No Button component found in children. Parent: [{transform.parent.name ?? "none"}]"); }
        if (panel == null) { Debug.LogWarning($"[{gameObject.name}] No RectTransform component found."); }
        #endif

        fadeComponent = new Fade(cg, this);
        transitionComponent = new Transition(texts, images, buttons, panel, this);
        scalingComponent = new Scale(texts, images, buttons, panel, this);
        rotationComponent = new Rotate(texts, images, buttons, panel, this);
        typeWriterComponent = new TypeWrite(texts, this);
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
    /// <param name="target">Target component to transition (Panel, Text, Image, Button)</param>
    /// <param name="occurrence">Specifies the instance of the target element</param>
    /// <param name="offset">Offset in pixels (or units depending on canvas scaling and render mode). Positive values move the element up</param>
    /// <param name="easing">Specifies the easing method the transition should use</param>
    /// <param name="duration">Time in seconds for the transition duration</param>
    /// <param name="delay">Time in seconds to wait before starting the transition</param>
    public void TransitionFromUp(TransitionTarget target, int occurrence, float offset, EasingType easing = EasingType.Linear, float duration = defaultDuration, float delay = 0f)
    {
        occurrence -= 1;
        transitionComponent.TransitionFromUp(target, occurrence, offset, easing, duration, delay);
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
    public void TransitionFromDown(TransitionTarget target, int occurrence, float offset, EasingType easing = EasingType.Linear, float duration = defaultDuration, float delay = 0f)
    {
        occurrence -= 1;
        transitionComponent.TransitionFromDown(target, occurrence, offset, easing, duration, delay);
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
    public void TransitionFromLeft(TransitionTarget target, int occurrence, float offset, EasingType easing = EasingType.Linear, float duration = defaultDuration, float delay = 0f)
    {
        occurrence -= 1;
        transitionComponent.TransitionFromLeft(target, occurrence, offset, easing, duration, delay);
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
    public void TransitionFromRight(TransitionTarget target, int occurrence, float offset, EasingType easing = EasingType.Linear, float duration = defaultDuration, float delay = 0f)
    {
        occurrence -= 1;
        transitionComponent.TransitionFromRight(target, occurrence, offset, easing, duration, delay);
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
    public void TransitionFromPosition(TransitionTarget target, int occurrence, Vector2 offset, EasingType easing = EasingType.Linear, float duration = defaultDuration, float delay = 0f)
    {
        occurrence -= 1;
        transitionComponent.TransitionFromPosition(target, occurrence, offset, easing, duration, delay);
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
    public void TransitionToUp(TransitionTarget target, int occurrence, float offset, EasingType easing = EasingType.Linear, float duration = defaultDuration, float delay = 0f)
    {
        occurrence -= 1;
        transitionComponent.TransitionToUp(target, occurrence, offset, easing, duration, delay);
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
    public void TransitionToDown(TransitionTarget target, int occurrence, float offset, EasingType easing = EasingType.Linear, float duration = defaultDuration, float delay = 0f)
    {
        occurrence -= 1;
        transitionComponent.TransitionToDown(target, occurrence, offset, easing, duration, delay);
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
    public void TransitionToLeft(TransitionTarget target, int occurrence, float offset, EasingType easing = EasingType.Linear, float duration = defaultDuration, float delay = 0f)
    {
        occurrence -= 1;
        transitionComponent.TransitionToLeft(target, occurrence, offset, easing, duration, delay);
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
    public void TransitionToRight(TransitionTarget target, int occurrence, float offset, EasingType easing = EasingType.Linear, float duration = defaultDuration, float delay = 0f)
    {
        occurrence -= 1;
        transitionComponent.TransitionToRight(target, occurrence, offset, easing, duration, delay);
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
    public void TransitionToPosition(TransitionTarget target, int occurrence, Vector2 offset, EasingType easing = EasingType.Linear, float duration = defaultDuration, float delay = 0f)
    {
        occurrence -= 1;
        transitionComponent.TransitionToPosition(target, occurrence, offset, easing, duration, delay);
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
    public void Rotate(TransitionTarget target, int occurrence, float degrees, EasingType easing = EasingType.Linear, float duration = defaultDuration, float delay = 0f)
    {
        occurrence -= 1;
        rotationComponent.Rotation(target, occurrence, degrees, easing, duration, delay);
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
    public void ScaleUp(TransitionTarget target, int occurrence, float multiplier, EasingType easing = EasingType.Linear, float duration = defaultDuration, float delay = 0f)
    {
        occurrence -= 1;
        scalingComponent.ScaleUp(target, occurrence, multiplier, easing, duration, delay);
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
    public void ScaleDown(TransitionTarget target, int occurrence, float multiplier, EasingType easing = EasingType.Linear, float duration = defaultDuration, float delay = 0f)
    {
        occurrence -= 1;
        scalingComponent.ScaleDown(target, occurrence, multiplier, easing, duration, delay);
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
        typeWriterComponent.TypeWriter(occurrence, delay, duration);
    }


}
