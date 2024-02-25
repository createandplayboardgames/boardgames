using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Following the tutorial: Drag and Drop in Unity - 2021 Tutorial by Tarodev //

public class DragDrop : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private bool isDragHappening = false;
    private Vector2 currentDragOffset;
    public Vector3 targetPosition;
    Collider2D tileCollider;

    private void Start()
    {
        var rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("HELLO DOWN");
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
            Debug.Log("DESTROYING TILE");
            //Destroy(gameObject);
        }
    }
    Vector2 GetMousePos(){ return Camera.main.ScreenToWorldPoint(Input.mousePosition); }


    public void SnapToPosition()
    {
        //TODO
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("OnCollisionEnter2D");
    }
    public void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("OnTriggerStay2D");
    }


}
