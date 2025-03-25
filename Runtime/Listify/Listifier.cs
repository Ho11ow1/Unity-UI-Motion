using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

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
    private GameObject prefab;

    private GameObject uiPanel;
    private Transform panelTransform;

    private List<KeyValuePair<string, string>> textList = new List<KeyValuePair<string, string>>();
    public List<GameObject> objectList = new List<GameObject>();

    void Awake()
    {
        uiPanel = gameObject;
        panelTransform = GetComponent<RectTransform>();
        prefab = Resources.Load<GameObject>("Listify/ListItem");

        #if UNITY_EDITOR
        if (panelTransform == null) { Debug.LogWarning($"[{gameObject.name}] No RectTransform component found."); }
        #endif
    }

    public void Listify(List<KeyValuePair<string, string>> pairs)
    {
        MakeLists(pairs);
    }

    private void MakeLists(List<KeyValuePair<string, string>> list)
    {
        foreach (KeyValuePair<string, string> item in list)
        {
            textList.Add(item);
        }

        textList.Reverse();

        foreach (KeyValuePair<string, string> item in textList)
        {
            GameObject newItem = Instantiate(prefab, uiPanel.transform);

            // Switch to GetComponentInChildren
            // or 
            // GetComponentsInChildren
            TextMeshProUGUI title = newItem.transform.Find("Title").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI description = newItem.transform.Find("Description").GetComponent<TextMeshProUGUI>();

            if (title != null) { title.text = item.Key; }
            if (description != null) { description.text = item.Value; }

            foreach (GameObject obj in objectList)
            {
                if (obj != null)
                {
                    obj.transform.localPosition -= new Vector3(0, 170, 0);
                }
            }

            objectList.Add(newItem);
        }
    }

    private void AddEffects()
    {
        foreach (GameObject obj in objectList)
        {
            Button button = obj.GetComponent<Button>();
            EventTrigger eventTrigger = button.GetComponent<EventTrigger>() ?? obj.AddComponent<EventTrigger>();


            // Add hover listener
        }
    }
    
    private void OnHover(bool isHovered, Button btn)
    {
        var rectTransform = btn.GetComponent<RectTransform>();
        float height = rectTransform.rect.height;
        // Interpolate height for smoothness
        // Along with setting the Description.text to visible / invisible
        if (isHovered)
        {
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, height + 70);
        }
        else
        {
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, height - 70);
        }
    }
}
