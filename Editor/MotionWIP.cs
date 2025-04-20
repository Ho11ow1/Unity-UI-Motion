using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;

#pragma warning disable IDE0090 // Use 'new(...)'
public class MotionWIP : EditorWindow
{
    private static MotionWIP window;
    // Motion variables
    private readonly List<GameObject> _motionList = new List<GameObject>();
    private Vector2 _scrollView;

    // Track which object is selected for code generation
    private RectTransform _selectedForCodeGen = null;

    // Styling variables
    private readonly GUIStyle _componentStyle = new GUIStyle();
    private readonly GUIStyle _childrenStyle = new GUIStyle();
    // Colour variables
    private Color _btnColour = new Color32(255, 255, 255, 255);
    private Color _inactiveColour = new Color32(255, 51, 89, 255);
    // Size variables
    private readonly GUILayoutOption _labelWidth = GUILayout.Width(52);
    private readonly GUILayoutOption _inputWidth = GUILayout.Width(30);
    private const int _selectWidth = 55;
    private const int _childIndent = 15;
    private const int _dropdownWidth = 96;

    // Text variables
    private const string _findText = "Find";
    // Animation variables
    private const string _animationText = "Animation";
    private const string _fadeText = "Fade";
    private const string _transitionText = "Transition";
    private const string _scaleText = "Scale";
    private const string _rotateText = "Rotate";
    private const string _typeWriteText = "TypeWrite";
    // Conveluted KeyValuePair finally works
    private readonly Dictionary<GameObject, Dictionary<int, Dictionary<RectTransform, AnimationData>>> _panelDataMap = 
                new Dictionary<GameObject, Dictionary<int, Dictionary<RectTransform, AnimationData>>>();
    private const int _textIndex = 0;
    private const int _imageIndex = 1;
    private const int _buttonIndex = 2;

    [MenuItem("Window/Motion/Motion WIP")]
    public static void ShowWindow()
    {
        window = GetWindow<MotionWIP>("Motion WIP");
        window.position = new Rect(650, 250, 650, 550);
        window.Show();
    }

    void OnEnable()
    {
        _componentStyle.alignment = TextAnchor.MiddleLeft;
        _componentStyle.stretchWidth = true;
        _componentStyle.wordWrap = false;

        _childrenStyle.alignment = TextAnchor.MiddleLeft;
        _childrenStyle.margin = new RectOffset(0, 0, 0, 0);
        _childrenStyle.padding = new RectOffset(0, 0, 0, 0);
        _childrenStyle.margin.left = _childIndent;
        _childrenStyle.stretchWidth = true;
        _childrenStyle.wordWrap = false;

        FindPanels();
    }

    private void OnDestroy()
    {
        _panelDataMap.Clear();
    }

    private void OnGUI()
    {
        GUILayout.Space(10);

        EditorGUILayout.LabelField("Motion Objects", EditorStyles.boldLabel);
        if (_motionList.Count == 0)
        {
            EditorGUILayout.HelpBox("No UI panels with `Motion` component found.", MessageType.Info);
        }

        _scrollView = EditorGUILayout.BeginScrollView(_scrollView);
        EditorGUILayout.BeginVertical("box");

        for (int i = 0; i < _motionList.Count; i++)
        {
            GameObject obj = _motionList[i];
            if (obj != null)
            {
                EditorGUILayout.BeginHorizontal(_componentStyle);

                if (!obj.activeInHierarchy)
                {
                    GUI.backgroundColor = _inactiveColour;
                    EditorGUILayout.ObjectField(obj, typeof(GameObject), true);
                }
                else
                {
                    EditorGUILayout.ObjectField(obj, typeof(GameObject), true);
                }

                GUI.backgroundColor = _btnColour;
                if (GUILayout.Button(_findText, GUILayout.Width(_selectWidth)))
                {
                    Selection.activeGameObject = obj;
                    EditorGUIUtility.PingObject(obj);
                }

                EditorGUILayout.EndHorizontal();

                DrawChildren(obj);
            }
        }

        EditorGUILayout.EndVertical();
        EditorGUILayout.EndScrollView();
    }

    // -------------------------------------------------------------- SET PANEL DATA --------------------------------------------------------------

    private void FindPanels()
    {
        _motionList.Clear();
        _panelDataMap.Clear();

        GameObject[] gameObjects = GameObject.FindObjectsOfType<GameObject>(true);

        foreach (GameObject obj in gameObjects)
        {
            if (obj.layer == LayerMask.NameToLayer("UI"))
            {
                if (obj.GetComponent<Motion>() != null)
                {
                    _motionList.Add(obj);
                    InitializePanelData(obj);
                }
            }
        }

        Debug.Log($"Found {_motionList.Count} UI panels with `Motion` scripts (including inactive)");
    }

    private void InitializePanelData(GameObject panel)
    {
        if (!_panelDataMap.ContainsKey(panel))
        {
            _panelDataMap[panel] = new Dictionary<int, Dictionary<RectTransform, AnimationData>>
            {
                { _textIndex, new Dictionary<RectTransform, AnimationData>() },
                { _imageIndex, new Dictionary<RectTransform, AnimationData>() },
                { _buttonIndex, new Dictionary<RectTransform, AnimationData>() }
            };
        }
    }

    // -------------------------------------------------------------- DRAW GUI --------------------------------------------------------------

    private void DrawChildren(GameObject gameObject)
    {
        List<RectTransform> rects = new List<RectTransform>();

        foreach (RectTransform child in gameObject.transform)
        {
            rects.Add(child);
        }

        foreach (RectTransform rect in rects)
        {
            EditorGUILayout.BeginHorizontal(_childrenStyle);

            if (!rect.gameObject.activeInHierarchy)
            {
                GUI.backgroundColor = _inactiveColour;
                EditorGUILayout.ObjectField(rect.gameObject, typeof(GameObject), true);
            }
            else
            {
                EditorGUILayout.ObjectField(rect.gameObject, typeof(GameObject), true);
            }

            GUI.backgroundColor = _btnColour;
            if (GUILayout.Button("Code", GUILayout.Width(_selectWidth)))
            {
                _selectedForCodeGen = (_selectedForCodeGen == rect) ? null : rect;
                Repaint();
            }

            EditorGUILayout.EndHorizontal();

            DrawAnimationControls(rect);

            if (_selectedForCodeGen == rect)
            {
                (AnimationData dataPoint, int indexPoint, string type) = GetTupleAnimationData(rect);
                if (dataPoint != null)
                {
                    GenerateMotionCode(dataPoint, indexPoint, type);
                }
            }
        }
    }

    private void DrawAnimationControls(RectTransform rect)
    {
        AnimationData data = GetAnimationData(rect);
        GUILayout.BeginHorizontal(_childrenStyle);

        GUIContent content = new GUIContent(data.animation, EditorGUIUtility.IconContent("d_UnityEditor.ConsoleWindow").image);

        bool clicked = EditorGUILayout.DropdownButton(content, FocusType.Passive, GUILayout.Width(_dropdownWidth));
        var button = GUILayoutUtility.GetLastRect();

        Rect menuPos = SetMenuPosition(button);

        if (clicked)
        {
            GenericMenu menu = new GenericMenu();
            menu.AddItem(new GUIContent(_fadeText), data.animation == _fadeText, (userData) => OnSelected((RectTransform)userData, _fadeText), rect);
            menu.AddItem(new GUIContent(_transitionText), data.animation == _transitionText, (userData) => OnSelected((RectTransform)userData, _transitionText), rect);
            menu.AddItem(new GUIContent(_rotateText), data.animation == _rotateText, (userData) => OnSelected((RectTransform)userData, _rotateText), rect);
            menu.AddItem(new GUIContent(_scaleText), data.animation == _scaleText, (userData) => OnSelected((RectTransform)userData, _scaleText), rect);
            if (rect.gameObject.GetComponent<TextMeshProUGUI>())
            {
                menu.AddItem(new GUIContent(_typeWriteText), data.animation == _typeWriteText, (userData) => OnSelected((RectTransform)userData, _typeWriteText), rect);
            }

            menu.DropDown(menuPos);
        }

        if (data.animation != "" && data.animation != _animationText)
        {
            EditorGUILayout.LabelField("Duration:", _labelWidth);
            data.duration = EditorGUILayout.FloatField(data.duration, _inputWidth);

            EditorGUILayout.LabelField("Delay:", _labelWidth);
            data.delay = EditorGUILayout.FloatField(data.delay, _inputWidth);
        }

        if (data.animation == _transitionText)
        {
            EditorGUILayout.LabelField("Offset:", _labelWidth);
            data.offset = EditorGUILayout.FloatField(data.offset, _inputWidth);
        }
        else if (data.animation == _rotateText)
        {
            EditorGUILayout.LabelField("Degrees:", _labelWidth);
            data.degrees = EditorGUILayout.FloatField(data.degrees, _inputWidth);
        }
        else if (data.animation == _scaleText)
        {
            EditorGUILayout.LabelField("Multiplier:", _labelWidth);
            data.multiplier = EditorGUILayout.FloatField(data.multiplier, _inputWidth);
        }

        GUILayout.EndHorizontal();
    }

    private Rect SetMenuPosition(Rect buttonRect)
    {
        Vector2 mousePos = Event.current.mousePosition;
        return new Rect(mousePos.x, mousePos.y, buttonRect.width, 0);
    }

    private void OnSelected(RectTransform rect, string animation)
    {
        if (rect != null)
        {
            AnimationData data = GetAnimationData(rect);
            data.animation = animation;
            //Debug.Log("Selected animation: " + animation + "\n" +
            //          "Object: " + rect.gameObject.name +
            //          (rect.gameObject.transform.parent != null ? " Parent: " + rect.gameObject.transform.parent.gameObject.name : ""));

            Repaint();
        }
    }

    // -------------------------------------------------------------- CODE GENERATION --------------------------------------------------------------

    private void GenerateMotionCode(AnimationData data, int occurrence, string type)
    {
        if (data.animation == _animationText || string.IsNullOrEmpty(data.animation))
        {
            EditorGUILayout.HelpBox("Please select an animation type first", MessageType.Warning);
            return;
        }

        string animationString = "";
        switch (data.animation)
        {
            case _fadeText:
                animationString = "Fade";// Add FadeIn/Out dropdown
                break;
            case _transitionText:
                animationString = "Transition";// Add TransitionDirections dropdown
                break;
            case _rotateText:
                animationString = "Rotate";
                break;
            case _scaleText:
                animationString = "Scale";
                break;
            case _typeWriteText:
                animationString = "TypeWrite";
                break;
        }

        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        float codeWidth = 550f;
        string[] labels = 
        { 
            "[SerializeField] private GameObject panel;", 
            "private Motion animator;", 
            "void Awake()", 
            "    animator = panel.GetComponent<Motion>();", "private void PlayAnimation()" 
        };

        EditorGUILayout.LabelField("// Generated Code", EditorStyles.boldLabel);
        EditorGUILayout.Space(5);

        EditorGUILayout.LabelField(labels[0], GUILayout.Width(codeWidth));
        EditorGUILayout.LabelField(labels[1], GUILayout.Width(codeWidth));
        EditorGUILayout.Space(5);

        EditorGUILayout.LabelField(labels[2], GUILayout.Width(codeWidth));
        EditorGUILayout.LabelField("{", GUILayout.Width(codeWidth));
        EditorGUILayout.LabelField(labels[3], GUILayout.Width(codeWidth));
        EditorGUILayout.LabelField("}", GUILayout.Width(codeWidth));
        EditorGUILayout.Space(5);

        EditorGUILayout.LabelField(labels[4], GUILayout.Width(codeWidth));
        EditorGUILayout.LabelField("{", GUILayout.Width(codeWidth));

        string parameters = $"{type}, {occurrence}";
        if (data.animation == _typeWriteText)
        {
            parameters = $"{occurrence}";
        }

        if (data.animation == _typeWriteText)
        {
            if (data.delay > 0)
            {
                parameters += $", delay: {data.delay}f";
            }

            if (data.duration > 0)
            {
                parameters += $", duration: {data.duration}f";
            }
        }
        else
        {
            if (data.duration > 0)
            {
                parameters += $", duration: {data.duration}f";
            }

            if (data.delay > 0)
            {
                parameters += $", delay: {data.delay}f";
            }
        }

        if (data.animation == _transitionText && data.offset != 0)
        {
            parameters += $", offset: {data.offset}f";
        }
        else if (data.animation == _rotateText && data.degrees != 0)
        {
            parameters += $", degrees: {data.degrees}f";
        }
        else if (data.animation == _scaleText && data.multiplier != 0)
        {
            parameters += $", multiplier: {data.multiplier}f";
        }

        GUIStyle codeStyle = new GUIStyle(EditorStyles.label) { fontStyle = FontStyle.Bold };

        EditorGUILayout.LabelField($"    animator.{animationString}({parameters});", codeStyle, GUILayout.Width(codeWidth));
        EditorGUILayout.LabelField("}", GUILayout.Width(codeWidth));
        EditorGUILayout.Space(5);

        if (GUILayout.Button("Copy to Clipboard", GUILayout.Width(150)))
        {
            string code = $"animator.{animationString}({parameters});";
            string s = "";
            for (int i = 0; i < labels.Length; i++)
            {
                s += labels[i];

                if (i == 2) { s += "{"; }
                else if (i == 3){ s += "}"; }
                else if (i == 4){ s += "{"; }
                
                s += "\n";
            }
            s += code + "\n" + "}\n\n";
            EditorGUIUtility.systemCopyBuffer = s;

            Debug.Log("Code copied to clipboard!");
        }

        EditorGUILayout.EndVertical();
    }

    // -------------------------------------------------------------- DATA GETTERS --------------------------------------------------------------

    private AnimationData GetAnimationData(RectTransform rect)
    {
        GameObject panel = rect.transform.parent.gameObject;
        if (!_panelDataMap.ContainsKey(panel))
        {
            InitializePanelData(panel);
        }

        int typeIndex = -1;
        if (rect.GetComponent<TextMeshProUGUI>())
        {
            typeIndex = _textIndex;
        }
        else if (rect.GetComponent<Button>())
        {
            typeIndex = _buttonIndex;
        }
        else if (rect.GetComponent<Image>())
        {
            typeIndex = _imageIndex;
        }

        if (typeIndex != -1)
        {
            if (!_panelDataMap[panel][typeIndex].ContainsKey(rect))
            {
                _panelDataMap[panel][typeIndex][rect] = new AnimationData();
            }

            return _panelDataMap[panel][typeIndex][rect];
        }

        return null;
    }

    private (AnimationData data, int index, string type) GetTupleAnimationData(RectTransform rect)
    {
        GameObject panel = rect.transform.parent.gameObject;
        if (!_panelDataMap.ContainsKey(panel))
        {
            InitializePanelData(panel);
        }

        string type = string.Empty;
        int typeIndex = -1;

        if (rect.GetComponent<TextMeshProUGUI>())
        {
            type = "Motion.AnimationTarget.Text";
            typeIndex = _textIndex;
        }
        else if (rect.GetComponent<Button>())
        {
            type = "Motion.AnimationTarget.Button";
            typeIndex = _buttonIndex;
        }
        else if (rect.GetComponent<Image>())
        {
            type = "Motion.AnimationTarget.Image";
            typeIndex = _imageIndex;
        }

        if (typeIndex != -1)
        {
            if (!_panelDataMap[panel][typeIndex].ContainsKey(rect))
            {
                _panelDataMap[panel][typeIndex][rect] = new AnimationData();
            }

            int index = _panelDataMap[panel][typeIndex].Keys.ToList().IndexOf(rect) + 1;
            return (_panelDataMap[panel][typeIndex][rect], index, type);
        }

        return (null, 0, string.Empty);
    }


}

[System.Serializable]
public class AnimationData
{
    public string animation = "Animation";
    public float duration = 0f;
    public float delay = 0f;
    public float offset = 0f;
    public float degrees = 0f;
    public float multiplier = 0f;
}

