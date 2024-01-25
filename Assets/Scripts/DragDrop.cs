using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Following the tutorial: Drag and Drop in Unity - 2021 Tutorial by Tarodev //

public class DragDrop : MonoBehaviour
{
    private bool isDragged = false; // keep track of sprite state
    private Vector3 spriteDragPosition; 
    private Vector3 _dragOffset;
    private Camera _cam;
    SpriteRenderer aSpriteRenderer;

    void Start()
    {
        aSpriteRenderer = GetComponent<SpriteRenderer>();
        aSpriteRenderer.color = new Color(1f, 1f, 1f, 1f);
    }

    void Awake()
    {
        _cam = Camera.main;
    }

    void OnMouseDown()
    {
        isDragged = true;
        _dragOffset = transform.position - GetMousePos();
    }

    void OnMouseDrag()
    {
        if (isDragged)
        {
            transform.position = GetMousePos() + _dragOffset;
            aSpriteRenderer.color = new Color(1f, 1f, 1f, .5f);
        }
    }

    void OnMouseUp()
    {
        isDragged = false;
        aSpriteRenderer.color = new Color(1f, 1f, 1f, 1f);
    }

    Vector3 GetMousePos()
    {
        var mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return mousePos;
    }

}
