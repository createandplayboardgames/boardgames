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
    public  bool overObject = false; 
    private Vector2 startPosition;
    private static GameObject selected = null;


    // Drag-Drop and Selection 
    // ---------------------------------------------------------------------------
    public void Start()
    {
        gameDefinitionManager = GameObject.Find("GameDefinitionManager").GetComponent<GameDefinitionManager>();
        layoutHelper = GameObject.Find("LayoutHelper").GetComponent<LayoutHelper>();
    }
    public void Update() // input events handled in update, to better manager overlapping game-pieces
    { 
        // check, was THIS clicked - in other words, is THIS the *frontmost* collider under the mouse?
        bool thisClicked = true;
        //compare sprite-renderer's collisions
        SpriteRenderer s1 = GetComponent<SpriteRenderer>();
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, s1.bounds.size/4, 0f);
        foreach (Collider2D collider in colliders){
            SpriteRenderer s2 = collider.GetComponent<SpriteRenderer>();
            if (s2 == null) continue;
            //compare sorting-layers and sorting-order
            int ID1 = SortingLayer.GetLayerValueFromID(s1.sortingLayerID);
            int ID2 = SortingLayer.GetLayerValueFromID(s2.sortingLayerID);
            if ((ID1 < ID2 && collider.OverlapPoint(GetMousePos())) || (ID1 == ID2 && s1.sortingOrder < s2.sortingOrder)){ 
                thisClicked = false;
                break;
            }
        }
        if (!thisClicked && !isDragging) return;  //if THIS wasn't clicked, there's nothing to do (unless already dragging) 
        bool atMouseLocation = GetComponent<Collider2D>().OverlapPoint(GetMousePos());

        if (Input.GetMouseButtonDown(1) && atMouseLocation){ //right-click, deletion
            gameDefinitionManager.DeleteGamePiece(gameObject); 
            GameObject.Find("MenuManager").GetComponent<MenuLayoutManager>().HideAllInfoMenus();
            return;
        }

        if (Input.GetMouseButtonDown(0) && !isDragging) 
        { // Mouse Down
            if (!atMouseLocation) return;
            isDragging = true;
            SetHighlightForDrag(true);
        } 
        else if (Input.GetMouseButton(0))
        { //Dragging
            if (!isDragging) return;
            transform.position = layoutHelper.GetBoundedPosition(new Vector3(GetMousePos().x, GetMousePos().y, transform.position.z));    
        }
        else if (Input.GetMouseButtonUp(0))
        {  // Mouse Up
            if (!isDragging) return;
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

    Vector2 GetMousePos() { return Camera.main.ScreenToWorldPoint(Input.mousePosition); }

    private void SetHighlightForDrag(bool dragging){
        if (dragging)   gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
        else            gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1.5f);
    }

    private void AttemptDropOnTile()
    {
        if (GetComponent<TileData>() != null) return; //first, confirm this isn't a tile
        TileData tile = FindOverlappingTile(); //find overlapping tile (or null)

        //perform specific drop-action, depending on type of this
        switch (GameDefinitionManager.GetGamePieceData(gameObject)){
            case PlayerData data:               
                layoutHelper.SnapPlayerToTile(data, null);                      // unsnap
                if (tile != null) 
                    layoutHelper.SnapPlayerToTile(data, tile);                  // snap
                return;
            case ActionData data:     
                layoutHelper.SnapActionToTile(data, null);                      // unsnap
                if (tile != null) {
                    bool success = layoutHelper.SnapActionToTile(data, tile);   // snap
                    if (!success) transform.position = startPosition;           // if already occupied, reset position
                }
                return;
            default:
                Debug.Log("invalid drop attempt"); 
                break;
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
