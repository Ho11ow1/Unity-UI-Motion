using UnityEngine;

/* --------------------------------------------------------
 * Unity UI Motion - Base Animation Component
 * Created by Hollow1 (Restructured)
 * 
 * A base class for UI animation components providing
 * common functionality for derived animation types.
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

    private Fade fadeComponent;
    private Transition transitionComponent;
    
    protected const float defaultDuration = 0.5f;

    void Awake()
    {
        fadeComponent = GetComponent<Fade>();
        if (fadeComponent == null)
        {
            fadeComponent = gameObject.AddComponent<Fade>();
        }

        transitionComponent = GetComponent<Transition>();
        if (transitionComponent == null)
        {
            transitionComponent = gameObject.AddComponent<Transition>();
        }
    }

    // ----------------------------------------------------- Fade API -----------------------------------------------------
    public void TurnInvisible()
    {
        fadeComponent.TurnInvisible();
    }

    public void TurnVisible()
    {
        fadeComponent.TurnVisible();
    }

    public void FadeIn(float delay = 0f, float duration = defaultDuration)
    {
        fadeComponent.FadeIn(delay, duration);
    }

    public void FadeOut(float delay = 0f, float duration = defaultDuration)
    {
        fadeComponent.FadeOut(delay, duration);
    }

    // ----------------------------------------------------- Transition API -----------------------------------------------------
    public void TransitionUp(TransitionTarget target, float offset, EasingType easing = EasingType.Linear, float duration = defaultDuration, float delay = 0f)
    {
        transitionComponent.TransitionUp(target, offset, easing, duration, delay);
    }

    public void TransitionDown(TransitionTarget target, float offset, EasingType easing = EasingType.Linear, float duration = defaultDuration, float delay = 0f)
    {
        transitionComponent.TransitionDown(target, offset, easing, duration, delay);
    }

    public void TransitionLeft(TransitionTarget target, float offset, EasingType easing = EasingType.Linear, float duration = defaultDuration, float delay = 0f)
    {
        transitionComponent.TransitionLeft(target, offset, easing, duration, delay);
    }

    public void TransitionRight(TransitionTarget target, float offset, EasingType easing = EasingType.Linear, float duration = defaultDuration, float delay = 0f)
    {
        transitionComponent.TransitionRight(target, offset, easing, duration, delay);
    }

    public void TransitionPosition(TransitionTarget target, Vector2 offset, EasingType easing = EasingType.Linear, float duration = defaultDuration, float delay = 0f)
    {
        transitionComponent.TransitionPosition(target, offset, easing, duration, delay);
    }


}
