# Unity UI Motion

A lightweight and flexible UI animation system for Unity that provides smooth transitions and fading effects.

## Features

- **Fade Animations**
  - Smooth fade in/out transitions
  - Customizable duration and delay
  - Zero-configuration setup with CanvasGroup

- **Position Transitions** 
  - Support for all 4 directions (Up, Down, Left, Right)
  - Multiple easing functions
  - Works with Text, Images and Panels

## Installation

### Option 1: Manual Installation
1. Download the `Fade.cs` and `Transition.cs` scripts from this repository
2. Add it to your Unity project's Assets folder
3. Ensure your UI element has a CanvasGroup component attached(`Fade.cs`)

### Option 2: Unity Package Manager
1. In Unity, go to Window > Package Manager
2. Click the + button and select "Add package from git URL..."
3. Enter: ```https://github.com/Ho11ow1/Unity-UI-Motion.git```

## Quick Start

### Fade Animations

```csharp
// Get the Fade component from your UI element
var fade = GetComponent<Fade>();

// Fade in with default duration (0.5s)
fade.FadeIn();

// Fade out with custom duration and delay
fade.FadeOut(delay: 1.0f, duration: 0.3f);
```

### Position Transitions

```csharp
// Get the Transition component from your UI element
var transition = GetComponent<Transition>();

// Slide in from right to left
transition.TransitionLeft(target: Transition.TransitionTarget.Text, offset: 100f);

// Slide up with custom parameters
transition.TransitionUp(
    target: Transition.TransitionTarget.Text,
    offset: 50f,
    easing: EasingType.EaseInOut,
    duration: 0.3f,
    delay: 0.5f
);
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
