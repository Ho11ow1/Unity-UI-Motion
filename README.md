# Unity UI Motion

A lightweight and flexible UI animation system for Unity that provides smooth transitions and fading effects.

## Features

- **Fade Animations**
  - Smooth fade in/out transitions
  - Customizable duration and delay
  - Instant visibility toggling
  - Zero-configuration setup with CanvasGroup

- **Position Transitions** 
  - Support for all 4 directions (Up, Down, Left, Right) + diagonal
  - Multiple easing functions
  - Flexible component targeting
  - Customization options:
    - Configurable offset distance
    - Adjustable animation duration
    - Optional animation delay
    - Custom position vectors

- **Scale Transformations**
  - Smooth scaling animations
  - Configurable scale multiplier
  - Support for different easing functions

- **Rotation Animations**
  - Precise rotation control in degrees
  - Multiple easing options
  - Configurable duration and delay

- **Text Effects**
  - Dynamic TypeWriter text animation
  - Character-by-character reveal
  - Customizable typing speed
  - Configurable start delay

## Installation

### Option 1: Manual Installation
1. Download the project from this repository
2. Add the unzipped folder to your Assets folder
3. Attach the `Motion.cs` component to your gameObject

### Option 2: Unity Package Manager
1. In Unity, go to Window > Package Manager
2. Click the + button and select "Add package from git URL..."
3. Enter: ```https://github.com/Ho11ow1/Unity-UI-Motion.git```

## Usage

### Basic Usage

```csharp
// Get the reference
Motion animation = uiElement.GetComponent<Motion>();

// Fade in with the default duration
animation.FadeIn();

// Transition TextMeshPro text up with an offset of 2 (default duration)
animation.TransitionUp(Motion.TransitionTarget.Text, 2)
```

### Advanced Usage

```csharp
// Fade in with a 0.5 second delay and a 1 second duration
animation.FadeIn(0.5f, 1f);

// Scale up TextMeshPro text with a 1.5x multiplier and a cubic easing function (default duration)
animation.ScaleUp(Motion.TransitionTarget.Text, 1.5f, Motion.EasingType.Cubic);

// Rotate an Image 30° clockwise (default duration)
animation.Rotate(Motion.TransitionTarget.Image, 30);
```

### Integration Example

```csharp
public class PopupManager : MonoBehaviour
{
    [SerializeField] GameObject panel;
    private Motion panelMotion;

    void Awake()
    {
        panelMotion = panel.GetComponent<Motion>();

        panel.SetActive(true); // Make sure the Ui panel is active
        panelMotion.TurnInvisible(); // Start invisible
    }

    public void ShowPopup()
    {
        panelMotion.FadeIn(0, 0.75f);
        panelMotion.TransitionRight(Motion.TransitionTarget.Panel, 100, Motion.EasingType.EaseIn);
    }

    public void HidePopup() 
    {
        panelMotion.FadeOut(0f, 1f);
    }
}
```

## Requirements

- Unity 2022.3 or higher
- TextMeshPro package (for text transitions)

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## Author

Created and maintained by [Hollow1](https://github.com/Ho11ow1)
