using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Following the tutorial: Drag and Drop in Unity - 2021 Tutorial by Tarodev //

public class DragDrop : MonoBehaviour
{
    private bool isDragHappening = false;
    private Vector2 currentDragOffset;
    public Vector3 targetPosition;
    Collider2D tileCollider;

    private void Start(){
        var rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
    }
    void OnMouseDown(){
        isDragHappening = true; //start drag
        currentDragOffset = GetMousePos() - (Vector2) transform.position; //store offset
    }
    void Update()
    {
        if (!isDragHappening) return;
        var newPos = GetMousePos() - currentDragOffset; //calculate new position
        transform.position = new Vector3(newPos.x, newPos.y, transform.position.z) ; //drag (don't change Z-position)
    }
    void OnMouseUp(){
        isDragHappening = false; //end drag
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
