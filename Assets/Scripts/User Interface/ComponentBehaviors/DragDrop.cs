using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Following the tutorial: Drag and Drop in Unity - 2021 Tutorial by Tarodev //

public class DragDrop : MonoBehaviour //IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private bool _dragging;
    
    private Vector2 currentDragOffset;

    public Vector3 targetPosition;

    public bool trigger = false;

    public TileSnapping tileSnapping;


    void Update()
    {
        if (!_dragging) return;
        var mousePosition = GetMousePos();
        transform.position = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);

    }


    public void OnMouseDown()
    {
        _dragging = true;
        currentDragOffset = GetMousePos() - (Vector2)transform.position; //store offset

        // Disable Triggers on tile child nodes
        if (gameObject.CompareTag("Tiles"))
        {
            DisableTriggers(gameObject);
        }
    }

    public void OnMouseUp()
    {
        _dragging = false;
        // Enable Triggers on tile child nodes
        if (gameObject.CompareTag("Tiles"))
        {
            EnableTriggers(gameObject);
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

    public void DisableTriggers(GameObject gameObject)
    {
        gameObject.GetComponent<TileSnapping>().enabled = true;
        gameObject.GetComponent<TileSnapping>().allowInteraction = true;
        if (trigger == true)
        {
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                GameObject child = gameObject.transform.GetChild(i).gameObject;
                if (child.GetComponent<Collider2D>().isTrigger)
                {
                    child.GetComponent<Collider2D>().isTrigger = false;
                }

            }
            trigger = false;
        }
    }

    public void EnableTriggers(GameObject gameObject)
    {
        if (trigger == false)
        {
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                GameObject child = gameObject.transform.GetChild(i).gameObject;
                if (!child.GetComponent<Collider2D>().isTrigger)
                {
                    child.GetComponent<Collider2D>().isTrigger = true;
                }

            }
            trigger = true;
        }
        // Disable TileSnapping
        gameObject.GetComponent<TileSnapping>().enabled = false;
        gameObject.GetComponent<TileSnapping>().allowInteraction = false;
    }

}
