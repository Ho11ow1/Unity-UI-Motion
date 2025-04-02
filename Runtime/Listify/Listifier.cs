using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

/* --------------------------------------------------------
 * Unity UI Listifier - List constructor component
 * Created by Hollow1
 * 
 * Creates a hoverable list of UI elements equipped
 * with a background image, title and description
 * (description is only visible on hover)
 * 
 * Version: 2.1.0
 * GitHub: https://github.com/Hollow1/Unity-UI-Motion
 * -------------------------------------------------------- */

public class Listifier : MonoBehaviour
{
    public enum ListDirection
    {
        Vertical,
        Horizontal
    }

    private GameObject prefab;
    private GameObject menuPanel;
    private Transform panelTransform;

    [SerializeField] private float offsetX = 0;
    [SerializeField] private float offsetY = 0;

    private List<KeyValuePair<string, string>> textList = new List<KeyValuePair<string, string>>();
    public List<GameObject> objectList = new List<GameObject>();

    void Awake()
    {
        menuPanel = gameObject;
        panelTransform = GetComponent<RectTransform>();
        prefab = Resources.Load<GameObject>("Listify/ListItem");

        #if UNITY_EDITOR
        if (prefab == null) { Debug.LogError($"[{gameObject.name}] Prefab not found"); }
        else { Debug.Log($"[{gameObject.name}] Loaded prefab: {prefab.name}, Active: {prefab.activeSelf}, Scene: {prefab.scene.name}"); }
        if (panelTransform == null) { Debug.LogWarning($"[{gameObject.name}] No RectTransform component found."); }
        #endif
    }

    // ----------------------------------------------------- PUBLIC API -----------------------------------------------------

    /// <summary>
    /// Creates a vertical list of interactable buttons
    /// </summary>
    /// <param name="pairs">KeyValuePair list for button title + description</param>
    /// <param name="offset">Offset in pixels (or units depending on canvas scaling and render mode). Positive values move the 2nd+ element down</param>
    public void Listify(List<KeyValuePair<string, string>> pairs, float offset)
    {
        offsetY = offset;
        pairs.Reverse();
        MakeLists(pairs);
        AddButtonEffects();
    }

    /// <summary>
    /// Creates a horizontal list of interactable buttons
    /// </summary>
    /// <param name="pairs">KeyValuePair list for button title + description</param>
    /// <param name="offset">Offset in pixels (or units depending on canvas scaling and render mode). Positive values move the 2nd+ element to the right</param>
    public void Rowify(List<KeyValuePair<string, string>> pairs, float offset)
    {
        offsetX = offset;
        pairs.Reverse();
        MakeLists(pairs);
        AddButtonEffects();
    }

    /// <summary>
    /// Returns the created GameObject list
    /// </summary>
    public List<GameObject> GetObjectList()
    {
        return objectList;
    }

    /// <summary>
    /// Sets the button navigation explicitly based on direction
    /// </summary>
    /// <param name="direction">Direction in which the UI should navigate. Vertical:Up,Down - Horizontal:Left,Right</param>
    public void SetNavigation(ListDirection direction)
    {
        for (int i = 0; i < objectList.Count; i++)
        {
            GameObject obj = objectList[i];
            Button btn = obj.GetComponent<Button>();

            Navigation nav = btn.navigation;
            nav.mode = Navigation.Mode.Explicit;

            if (direction == ListDirection.Vertical)
            {
                if (i > 0)
                {
                    nav.selectOnUp = objectList[i - 1].GetComponent<Button>();
                }
                else
                {
                    nav.selectOnUp = btn;
                }

                if (i < objectList.Count - 1)
                {
                    nav.selectOnDown = objectList[i + 1].GetComponent<Button>();
                }
                else
                {
                    nav.selectOnDown = btn;
                }
            }
            else
            {
                if (i > 0)
                {
                    nav.selectOnLeft = objectList[i - 1].GetComponent<Button>();
                }
                else
                {
                    nav.selectOnLeft = btn;
                }

                if (i < objectList.Count - 1)
                {
                    nav.selectOnRight = objectList[i + 1].GetComponent<Button>();
                }
                else
                {
                    nav.selectOnRight = btn;
                }
            }

            btn.navigation = nav;
        }
    }

    /// <summary>
    /// Sets button onClick events in order
    /// </summary>
    /// <param name="event1">Function to be called on the first button</param>
    /// <param name="event2">Function to be called on the second button</param>
    /// <param name="event3">Function to be called on the third button</param>
    /// <param name="event4">Function to be called on the fourth button</param>
    /// <param name="event5">Function to be called on the fifth button</param>
    /// <param name="event6">Function to be called on the sixth button</param>
    public void SetButtonEvents(UnityAction event1 = null, UnityAction event2 = null, UnityAction event3 = null, UnityAction event4 = null, UnityAction event5 = null, UnityAction event6 = null)
    {
        UnityAction[] events = { event1, event2, event3, event4, event5, event6 };

        for (int i = 0; i < objectList.Count; i++)
        {
            if (i < events.Length && events[i] != null)
            {
                GameObject obj = objectList[i];
                Button btn = obj.GetComponent<Button>();

                btn.onClick.AddListener(events[i]);
            }
        }
    }

    /// <summary>
    /// Sets the button navigation explicitly based on direction with onClick events in order
    /// </summary>
    /// <param name="direction">Direction in which the UI should navigate. Vertical:Up,Down - Horizontal:Left,Right</param>
    /// <param name="event1">Function to be called on the first button</param>
    /// <param name="event2">Function to be called on the second button</param>
    /// <param name="event3">Function to be called on the third button</param>
    /// <param name="event4">Function to be called on the fourth button</param>
    /// <param name="event5">Function to be called on the fifth button</param>
    /// <param name="event6">Function to be called on the sixth button</param>
    public void SetNavigationWithEvents(ListDirection direction, UnityAction event1 = null, UnityAction event2 = null, UnityAction event3 = null, UnityAction event4 = null, UnityAction event5 = null, UnityAction event6 = null)
    {
        SetNavigation(direction);
        SetButtonEvents(event1, event2, event3, event4, event5, event6);
    }

    // ----------------------------------------------------- LIST CREATION -----------------------------------------------------

    private void MakeLists(List<KeyValuePair<string, string>> list)
    {
        textList = list;
        textList.Reverse();
        objectList.Clear();

        foreach (KeyValuePair<string, string> item in textList)
        {
            GameObject newItem = Instantiate(prefab, menuPanel.transform);
            RectTransform rectTransform = newItem.GetComponent<RectTransform>();

            TextMeshProUGUI[] arr = newItem.GetComponentsInChildren<TextMeshProUGUI>();

            #if UNITY_EDITOR
            if (arr.Length < 2) { Debug.LogError($"[{gameObject.name}] Doesn't contain 'Title' or 'Description' component"); }
            #endif

            TextMeshProUGUI title = arr[0];
            TextMeshProUGUI description = arr[1];
            description.alpha = 0;

            if (title != null) { title.text = item.Key; }
            if (description != null) { description.text = item.Value; }

            objectList.Add(newItem);

            if (offsetX > 0)
            {
                rectTransform.anchoredPosition += new Vector2(offsetX * (objectList.Count - 1), 0);
            }
            else if (offsetY > 0)
            {
                rectTransform.anchoredPosition -= new Vector2(0, offsetY * (objectList.Count - 1));
            }
            else
            {
                rectTransform.anchoredPosition -= new Vector2(0, 170 * (objectList.Count - 1));
            }
        }
    }

    // ----------------------------------------------------- HOVER EFFECT -----------------------------------------------------

    private void AddButtonEffects()
    {
        foreach (GameObject obj in objectList)
        {
            obj.AddComponent<HoverEffect>();
        }
    }


}
