using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingButtonHandler : MonoBehaviour
{
    [SerializeField] BuildingObject item;
    Button button;

    EditorUI editor;
    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ButtonClicked);
        editor = EditorUI.GetInstance();
    }

    private void ButtonClicked()
    {
        // Debug.Log("Button was clicked: " + item.name);
        editor.ObjectSelected(item);
    }
}