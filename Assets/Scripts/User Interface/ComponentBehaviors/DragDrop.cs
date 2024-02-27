using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Following the tutorial: Drag and Drop in Unity - 2021 Tutorial by Tarodev //

public class DragDrop : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private Vector2 currentDragOffset;
    public Vector3 targetPosition;

    public void OnPointerDown(PointerEventData eventData)
    {
        currentDragOffset = GetMousePos() - (Vector2)transform.position; //store offset
    }
    public void OnDrag(PointerEventData eventData)
    {
        var newPos = GetMousePos() - currentDragOffset; //calculate new position
        transform.position = new Vector3(newPos.x, newPos.y, transform.position.z); //drag (don't change Z-position)    }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        //TODO: Snap to closest tile if in range
    }

    public void OnMouseOver(){
        if (Input.GetMouseButtonDown(1)){ //right click
            Debug.Log("Destroying Tile");
            Destroy(gameObject);
        }
    }
    Vector2 GetMousePos(){ return Camera.main.ScreenToWorldPoint(Input.mousePosition); }

}
