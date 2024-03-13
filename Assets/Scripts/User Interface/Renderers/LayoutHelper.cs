using System.Collections.Generic;
using UnityEngine;

public class LayoutHelper : MonoBehaviour{

    GameDefinitionManager gameDefinitionManager;
    public void Start(){
        gameDefinitionManager = GameObject.Find("GameDefinitionManager").GetComponent<GameDefinitionManager>();
    }

    // ===== Organizing Items on Tiles
    public void SnapPlayerToTile(PlayerData player, TileData tile){
        if (tile == null){ //unset case
            player.location = null;
            player.transform.parent = null; 
        } else { //set case
            player.location = tile;
            LayoutPlayersOnTile(tile);
        }
    }
    public void SnapActionToTile(ActionData action, TileData tile){
        foreach (ActionData a in gameDefinitionManager.cache.actions){
            if (a.location == tile){
                //TODO - error message (no more than one action per-tile (?))
                return;
            }
        }
        LayoutActionOnTile(action, tile);
    }

    private void LayoutPlayersOnTile(TileData tile){
        // determine players on tile
        List<PlayerData> playersOnTile = new();
        foreach (PlayerData player in gameDefinitionManager.cache.players)
            if (player.location == tile) { playersOnTile.Add(player); }
        int count = playersOnTile.Count;
        if (count == 0) return;
        if (count == 1) LayoutItemInCenterOfTile(playersOnTile[0], tile);
        else LayoutPlayersInCircle(playersOnTile, tile);
    }
    private void LayoutActionOnTile(NonTileData action, TileData tile){
        LayoutItemInCenterOfTile(action, tile);
    }

    private void LayoutPlayersInCircle(List<PlayerData> players, TileData tile){
		for (int i = 0; i < players.Count; i++){
            //first, center
            var t = players[i].gameObject.transform;
            t.position = tile.gameObject.transform.position; //position
            t.parent = tile.gameObject.transform; //parent;
            float angleIncrement = 360 / players.Count;
            //calculate deltaX and deltaY
            float startAngle = 180;
            float degrees = (i * angleIncrement + startAngle) % 360;
            float radians = degrees * (Mathf.PI / 180);
            float radius = (tile.gameObject.GetComponent<SpriteRenderer>().bounds.size.x / 2.0f)
                - (players[i].GetComponent<SpriteRenderer>().bounds.size.x / 2.0f);
            float deltaX = radius * Mathf.Cos(radians);
            float deltaY = radius * Mathf.Sin(radians);
            //move 
            t.position = new Vector3(t.position.x + deltaX, t.position.y + deltaY, t.position.z);
        } // NOTE - scaling shouldn't be needed, assuming player image size is limited
      	
    }
    private void LayoutItemInCenterOfTile(NonTileData item, TileData tile){
        var t = item.gameObject.transform;
        t.position = tile.gameObject.transform.position; //position
        t.parent = tile.gameObject.transform; //parent;
    }

}	