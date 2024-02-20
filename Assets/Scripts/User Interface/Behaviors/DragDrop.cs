using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Following the tutorial: Drag and Drop in Unity - 2021 Tutorial by Tarodev //

public class DragDrop : MonoBehaviour
{
    private bool _dragging;
    private Vector2 _dragOffset;

    public Vector3 targetPosition;

    Collider2D tileCollider;


    void Update()
    {
        if (!_dragging) return;
        var mousePosition = GetMousePos();
        transform.position = mousePosition - _dragOffset;
    }

    void OnMouseDown()
    {
        _dragging = true;
        _dragOffset = GetMousePos() - (Vector2)transform.position;
        tileCollider = GetComponent<Collider2D>();

    }

    public void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Destroy(gameObject);
        }
    }


    void OnMouseUp()
    {
        _dragging = false;
        //TODO: Snap to closest tile if in range
    }

    Vector2 GetMousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void SnapToPosition()
    {
        //TODO
    }

}
