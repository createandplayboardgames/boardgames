using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.EventSystems;

// Following the tutorial: Drag and Drop in Unity - 2021 Tutorial by Tarodev //

public class DragDrop : MonoBehaviour 
{
    private bool _dragging;
    
    private Vector2 currentDragOffset;

    private Vector2 startPosition;

    public bool overObject = false;
    public bool withinBoard = true;

    void Update()
    {
        if (!_dragging) return;
        var mousePosition = GetMousePos();
        transform.position = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);

    }


    public void OnMouseDown()
    {
        startPosition = transform.position;
        _dragging = true;
        currentDragOffset = GetMousePos() - (Vector2)transform.position; //store offset
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.5f);

    }


    public void OnMouseUp()
    {
        _dragging = false;

        gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1.5f);

        if( !withinBoard )
        {
            transform.position = new Vector3(startPosition.x,
                                             startPosition.y,
                                             transform.position.z);
        }

        //Enable Snapping and check for overlapping object tiles
        if (gameObject.CompareTag("Tiles"))
        {
            if ( overObject )
            {
                transform.position = new Vector3(startPosition.x,
                                                 startPosition.y,
                                                 transform.position.z);
            }
            else
            {

                var currentPos = transform.position;

                transform.position = new Vector3(Mathf.Round(currentPos.x),
                                                 Mathf.Round(currentPos.y),
                                                 transform.position.z);
            }

        }
    }


    public void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Destroying Tile");
            Destroy(gameObject);
        }
    }


    Vector2 GetMousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

}
