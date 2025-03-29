# Unity UI Motion

A lightweight and flexible UI animation system for Unity that provides smooth transitions and fading effects.

## Features

- **Fade Animations**
  - Zero-configuration setup with CanvasGroup

- **Position Transitions** 
  - Multiple easing functions
  - Flexible component targeting

- **Scale Transformations**
  - Smooth scaling animations
  - Configurable scale multiplier

- **Rotation Animations**
  - Precise rotation control in degrees
  - Multiple easing options
  - Configurable duration and delay

- **Text Effects**
  - Dynamic TypeWriter text animation
  - Customizable typing speed
  - Configurable start delay

- **Button menu creation**
  - Explicit navigation
  - Vertical or horizontal layouts
  - Automatic button hover effects

## Installation

### Option 1: Manual Installation
1. Download the project from this repository
2. Add the unzipped folder to your Assets folder
3. Attach the appropriate component (`Motion.cs` or `Listifier.cs`) to your gameObject

### Option 2: Unity Package Manager
1. In Unity, go to Window > Package Manager
2. Click the + button and select "Add package from git URL..."
3. Enter: ```https://github.com/Ho11ow1/Unity-UI-Motion.git```

## Usage

```csharp
public class Example : MonoBehaviour
{
    [SerializeField] GameObject panel;
    private EventSystem eventSystem;

    private Motion panelMotion;
    private Listifier panelListifier;

    private List<GameObject> objectList;

    void Awake()
    {
        panel.SetActive(true);

        panelMotion = panel.GetComponent<Motion>();
        panelListifier = panel.GetComponent<Listifier>();
        eventSystem = FindAnyObjectByType<EventSystem>();

        panelMotion.TurnInvisible();
    }

    void Start()
    {
        var menuList = new List<KeyValuePair<string, string>>
        {
            new KeyValuePair<string, string>("Title", "Example description"),
            new KeyValuePair<string, string>("Longer title", "Longer example description"),
            new KeyValuePair<string, string>("Exit game", "Short example")
        };
        // Create a vertical list with 100 pixel spacing
        panelListifier.Listify(menuList, 100);

        // Or create a horizontal list with 150 pixel spacing
        // listifier.Rowify(menuList, 150);

        // Seperate functions
        panelListifier.SetNavigation(Listifier.ListDirection.Vertical); // Use the appropriate ListDirection based on Listify or Rowify
        panelListifier.SetButtonEvents(Example, Example, Example, Example, Example, Example);

        // Single combined function
        // panelListifier.SetNavigationWithEvents(Listifier.ListDirection.Vertical, Example, Example, Example, Example, Example, Example);

        objectList = panelListifier.GetObjectList();
        eventSystem.SetSelectedGameObject(objectList[0]);
    }

    public void ShowPopup()
    {
        panelMotion.FadeIn(0, 0.75f);
        panelMotion.TransitionFromLeft(Motion.TransitionTarget.Panel, 100, Motion.EasingType.EaseIn);
    }

    public void HidePopup()
    {
        panelMotion.FadeOut(0f, 1f);
    }

    private void Example()
    {
        Debug.Log("Button pressed");
    }


}

```

## Requirements

- Unity 2022.3 or higher
- TextMeshPro package (for text transitions and `Listifier` component)

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## Author

Created and maintained by [Hollow1](https://github.com/Ho11ow1)
