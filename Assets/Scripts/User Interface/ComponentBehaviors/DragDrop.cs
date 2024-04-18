using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour
{
    private LayoutHelper layoutHelper;
    private Vector2 startPosition;
    private bool isDragging = false;
    public bool overObject  = false; //TODO - is this unused?

    public void Start()
    {
        layoutHelper = GameObject.Find("LayoutHelper").GetComponent<LayoutHelper>();
    }
    public void Update() // events called in update, to account overlaps between objects
    { 
        // check, was this clicked - in other words, is this the frontmost collider under the mouse ?
        bool thisClicked = true;
        SpriteRenderer r1 = GetComponent<SpriteRenderer>();
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, r1.bounds.size, 0f);
        foreach (Collider2D collider in colliders){
            SpriteRenderer r2 = collider.GetComponent<SpriteRenderer>();
            if (r2 == null) continue;
            int ID1 = SortingLayer.GetLayerValueFromID(r1.sortingLayerID);
            int ID2 = SortingLayer.GetLayerValueFromID(r2.sortingLayerID);
            if (ID1 < ID2 && collider.OverlapPoint(GetMousePos())){ 
                thisClicked = false;
                break;
            }
        }
        if (!thisClicked && !isDragging) return;  //if dragging, can ignore these rules
        //check, at mouse location?
        bool atMouseLocation = GetComponent<Collider2D>().OverlapPoint(GetMousePos());

        // Mouse Down
        if (Input.GetMouseButtonDown(0) && !isDragging) 
        {
            if (!atMouseLocation) return;
            isDragging = true;
            SetHighlightForDrag(true);
            if (GetComponent<PlayerData>() != null) { // if player, dis-associate with any tiles until reset 
                layoutHelper.SnapPlayerToTile(GetComponent<PlayerData>(), null);
            }
        } 
        // Drag
        else if (Input.GetMouseButton(0))
        {
            if (!isDragging) return;
            transform.position = layoutHelper.GetBoundedPosition(
                new Vector3(GetMousePos().x, GetMousePos().y, transform.position.z));    
        }
        // Mouse Up
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            SetHighlightForDrag(false);
            
            // Enable Snapping and check for overlapping object tiles
            if (gameObject.CompareTag("Tiles")){
                if (overObject){
                    transform.position = new Vector3(startPosition.x, startPosition.y, transform.position.z);
                } else {
                    var currentPos = transform.position;
                    transform.position = new Vector3(Mathf.Round(currentPos.x), Mathf.Round(currentPos.y), transform.position.z);
                }
            
            }
        }
    }

    public void OnMouseOver() { // TODO - move this, into Update()?
        if (Input.GetMouseButtonDown(1))
            Destroy(gameObject); //TODO - do this through GameDefinitionManager, to ensure that cache remains valid 
    }
    Vector2 GetMousePos() { return Camera.main.ScreenToWorldPoint(Input.mousePosition); }

    private void SetHighlightForDrag(bool dragging){
        if (dragging) gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
        else gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1.5f);
    }
}
