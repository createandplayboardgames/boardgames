using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour
{
    GameDefinitionManager gameDefinitionManager;
    private LayoutHelper layoutHelper;
    private bool isDragging = false;
    public  bool overObject = false; //TODO - where is this used?
    private Vector2 startPosition;
    private static GameObject selected = null;


    // Drag-Drop and Selection 
    // ---------------------------------------------------------------------------
    public void Start()
    {
        gameDefinitionManager = GameObject.Find("GameDefinitionManager").GetComponent<GameDefinitionManager>();
        layoutHelper = GameObject.Find("LayoutHelper").GetComponent<LayoutHelper>();
    }
    public void Update() // events called in update, to account overlaps between objects
    { 
        // check, was this clicked - in other words, is this the frontmost collider also under the mouse ?
        bool thisClicked = true;
        SpriteRenderer s1 = GetComponent<SpriteRenderer>();
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, s1.bounds.size/4, 0f);
        foreach (Collider2D collider in colliders){
            SpriteRenderer s2 = collider.GetComponent<SpriteRenderer>();
            if (s2 == null) continue;
            int ID1 = SortingLayer.GetLayerValueFromID(s1.sortingLayerID);
            int ID2 = SortingLayer.GetLayerValueFromID(s2.sortingLayerID);
            if ((ID1 < ID2 && collider.OverlapPoint(GetMousePos())) || (ID1 == ID2 && s1.sortingOrder < s2.sortingOrder)){ 
                thisClicked = false;
                break;
            }
        }
        if (!thisClicked && !isDragging) return;  //if not clicked, nothing to do - unless dragging
        bool atMouseLocation = GetComponent<Collider2D>().OverlapPoint(GetMousePos());

        if (Input.GetMouseButtonDown(0) && !isDragging) 
        { // Mouse Down
            if (!atMouseLocation)  return;
            isDragging = true;
            SetHighlightForDrag(true);
        } 
        else if (Input.GetMouseButton(0))
        { //Dragging
            if (!isDragging) return;
            transform.position = layoutHelper.GetBoundedPosition( 
                new Vector3(GetMousePos().x, GetMousePos().y, 
                    transform.position.z));    
        }
        else if (Input.GetMouseButtonUp(0))
        {  // Mouse Up
            if (!isDragging)
                return;
            isDragging = false;
            SetHighlightForDrag(false);
            
            AttemptDropOnTile();

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

    private void AttemptDropOnTile(){
        //first, confirm this isn't a tile
        if (GetComponent<TileData>() != null) return;

        // Does object overlap w/ Tile?
        TileData tile = FindOverlappingTile();
        // find type, perform appropriate snap
        PlayerData player = GetComponent<PlayerData>();
        if (player){
            layoutHelper.SnapPlayerToTile(player, null); //unsnap
            if (tile != null) layoutHelper.SnapPlayerToTile(player, tile); //snap
            return;
        }
        ActionData action = GetComponent<ActionData>();
        if (action){
            layoutHelper.SnapActionToTile(action, null); //unsnap
            if (tile != null) {
                bool success = layoutHelper.SnapActionToTile(action, tile); //snap
                if (!success){
                    transform.position = startPosition;
                }
            }
            return;   
        }
    }

    private TileData FindOverlappingTile(){
        Collider2D[] overlapping = Physics2D.OverlapCircleAll(transform.position, .15f); 
        foreach (Collider2D collider in overlapping){
            if (collider.gameObject == gameObject) //skip self
                continue;
            if (collider.gameObject.GetComponent<TileData>() != null)
                return collider.gameObject.GetComponent<TileData>();
        }
        return null;
    }
}
