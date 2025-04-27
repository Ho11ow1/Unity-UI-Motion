# Unity UI Motion

A lightweight and flexible UI animation system for Unity that delivers fluid animations with extensive customization options

## Features

- **Fade Animations**
  - Zero-configuration setup with CanvasGroup
  - Customizable duration and delay

- **Position Transitions** 
  - Omnidirectional movement
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
3. Attach the appropriate component (`Motion.cs` or `Listifier.cs`) to a UI Panel

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

        panelMotion.SetPanelVisibility(false);

        Motion.fadeStart += OnStart;
        Motion.fadeEnd += OnEnd;
    }

    void Start()
    {
        CreateMenuList();

        objectList = panelListifier.GetObjectList();
        eventSystem.SetSelectedGameObject(objectList[0]);
    }

    void OnDisable()
    {
        Motion.fadeStart -= OnStart;
        Motion.fadeEnd -= OnEnd;
    }

    public void ShowPopup()
    {
        panelMotion.FadeIn(Motion.AnimationTarget.Panel, 1, 0.75f);
        panelMotion.TransitionFromLeft(Motion.AnimationTarget.Image, 1, 50f, Motion.EasingType.EaseIn, 1.5f);
    }

    public void HidePopup()
    {
        panelMotion.FadeOut(Motion.AnimationTarget.Panel, 1, 0.5f, 1.5f);
    }

    private void Log()
    {
        Debug.Log("Button pressed");
    }

    private void OnStart()
    {
        Debug.Log("Animation started: " + Time.time);
    }

    private void OnEnd()
    {
        Debug.Log("Animation finished: " + Time.time);
    }

    private void CreateMenuList()
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
        // Use the appropriate direction based on Listify / Rowify
        panelListifier.SetNavigation(Listifier.ListDirection.Vertical);
        panelListifier.SetButtonEvents(Log, Log, Log, Log, Log, Log);

        // Single combined function
        // panelListifier.SetNavigationWithEvents()
    }
}

```

## Requirements

- Unity 2022.3 or higher
- TextMeshPro package

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## Author

Created and maintained by [Hollow1](https://github.com/Ho11ow1)
