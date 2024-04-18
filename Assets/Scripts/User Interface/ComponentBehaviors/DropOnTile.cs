using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropOnTile : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    GameDefinitionManager gameDefinitionManager;
    LayoutHelper layoutHelper;

    public void Start(){
        gameDefinitionManager = GameObject.Find("GameDefinitionManager").GetComponent<GameDefinitionManager>();
        layoutHelper = GameObject.Find("LayoutHelper").GetComponent<LayoutHelper>();
    }

    public void OnPointerDown(PointerEventData eventData){
        //nothing - required for OnPointerU to work
    }
    public void OnPointerUp(PointerEventData eventData){
        AttemptDropOnTile();
    }
    private void AttemptDropOnTile(){
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
            if (tile != null) layoutHelper.SnapActionToTile(action, tile); //snap
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
