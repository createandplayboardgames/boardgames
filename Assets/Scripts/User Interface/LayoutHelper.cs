using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class LayoutHelper : MonoBehaviour{

    GameDefinitionManager gameDefinitionManager;

    public void Start(){
        gameDefinitionManager = GameObject.Find("GameDefinitionManager").GetComponent<GameDefinitionManager>();
    }

    // ===== Organizing Items on Tiles
    public bool SnapPlayerToTile(PlayerData player, TileData tile){
        UpdateLocationData(player, tile);
        LayoutPlayersOnTile(tile);
        return true;
    }
    public bool SnapActionToTile(ActionData action, TileData tile){
        if (tile != null){
            foreach (ActionData a in gameDefinitionManager.cache.actions){
                if (a.location == tile && action != a){
                    //TODO - error message (no more than one action per-tile (?))
                    return false;
                }
            }
        }
        UpdateLocationData(action, tile);
        LayoutActionOnTile(action, tile);
        return true; 
    }
    public void UpdateLocationData(NonTileData item, TileData tile)
    {
        if (tile == null)
        { //unset 
            item.location = null;
            item.transform.parent = null;
        }
        else
        { //set 
            item.location = tile;            
        }
    }

    public void LayoutPlayersOnTile(TileData tile){
        // determine players on tile
        if (tile == null) return;
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
        if (tile == null) return;
        var t = item.gameObject.transform;
        t.position = tile.gameObject.transform.position; //position
        t.parent = tile.gameObject.transform; //parent;
    }

    public Vector3 GetBoundedPosition(Vector3 input)
    {
        Vector3 p = input;
        GameObject board = GameObject.Find("Background");
        Vector3 boardCenter = board.transform.position;
        float boardWidth = board.GetComponent<SpriteRenderer>().bounds.size.x;
        float boardHeight = board.GetComponent<SpriteRenderer>().bounds.size.y;
        p.x = Mathf.Clamp(p.x, boardCenter.x - boardWidth / 2,  boardCenter.x + boardWidth / 2);
        p.y = Mathf.Clamp(p.y, boardCenter.y - boardHeight / 2, boardCenter.y + boardHeight / 2);
        return p;
    }

    public void StartFlashErrorText(string messageText){
        var textMesh = GameObject.Find("ErrorText").GetComponent<TMP_Text>();
        textMesh.text = messageText;
        textMesh.enabled = true;
        StartCoroutine(EndFlashErrorText());
    }
    IEnumerator EndFlashErrorText(){
        yield return new WaitForSeconds(1);
        var textMesh = GameObject.Find("ErrorText").GetComponent<TMP_Text>();
        textMesh.enabled = false;
    }

}   