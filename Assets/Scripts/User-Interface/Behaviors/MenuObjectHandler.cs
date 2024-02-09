using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Handles Menu bar item selection
 */

public class MenuObjectHandler : MonoBehaviour
{
    [SerializeField] MenuObject item;
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
        editor.ObjectSelected(item);
    }
}