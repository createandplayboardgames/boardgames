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
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
        // Disable Triggers on tile child nodes
        if (gameObject.CompareTag("Tiles"))
        {
            DisableTriggers(gameObject);
        }
    }

    public void OnMouseUp()
    {
        _dragging = false;
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1.5f);

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

    //Disable active triggers in moving object
    public void DisableTriggers(GameObject gameObject)
    {
        gameObject.GetComponent<TileSnapping>().enabled = true;
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

    //Enable triggers in set object
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
        gameObject.GetComponent<TileSnapping>().enabled = true;

    }

}
