using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropOnTile : MonoBehaviour, IPointerUpHandler
{

    GameDefinitionManager gameDefinitionManager;
    LayoutHelper layoutHelper;

    public void Start()
    {
        gameDefinitionManager = GameObject.Find("GameDefinitionManager").GetComponent<GameDefinitionManager>();
        layoutHelper = GameObject.Find("LayoutHelper").GetComponent<LayoutHelper>();
    }

    public void OnPointerUp(PointerEventData eventData){
        AttemptDropOnTile();
    }
    private void AttemptDropOnTile(){
        // Does object overlap w/ Tile?
        Collider2D[] overlapping = Physics2D.OverlapCircleAll(transform.position, 1f); //TODO
        var player = GetComponent<PlayerData>();
        layoutHelper.SnapPlayerToTile(player, null); //unsnap
        foreach (Collider2D collider in overlapping){
            if (collider.gameObject == gameObject) //skip self
                continue;
            if (collider.gameObject.GetComponent<TileData>() != null)
            { //overlaps with Tile
                var tile = collider.gameObject.GetComponent<TileData>();
                layoutHelper.SnapPlayerToTile(player, tile);
                break;
            }
        }
    }




}
