using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameDefinitionManager : MonoBehaviour
{
    private readonly int MAX_PLAYER_COUNT = 4;
    public GameDefinitionCache cache = new();

    // ====== General Functions
    private GameObject LoadGameObject(string prefabName, String sortingLayerName){
        var parent = GameObject.Find(Keywords.GAMEOBJECT_BOARD);
        GameObject gamePiece = Instantiate(Resources.Load(prefabName),
            parent.transform.position, parent.transform.rotation) as GameObject;
        gamePiece.transform.parent = parent.transform;
        gamePiece.GetComponent<SpriteRenderer>().sortingLayerName = sortingLayerName;
        //TODO - select (currently won't work with actions)
        //TODO - because the camera can pan and zoom, we should instantiate objects in the middle of the camera - ideally without overlapping 
        return gamePiece;
    }

    // ====== Players 
    public void CreatePlayer(){
        if (cache.players.Count > MAX_PLAYER_COUNT)
            return; //TODO - show error message
        var obj = LoadGameObject(Keywords.PREFAB_PLAYER, Keywords.SORTING_LAYER_PLAYERS);
        
        //give the player a unique name, of form "player-num"  - TODO - this needs testing!
        int i = 0;
        while (true){
            String name = "player-" + i;  // choose potential name
            bool isNameUnique = true; //check if name unique...
            foreach (PlayerData player in cache.players){ //... against all current names
                if (player.playerName == name) {
                    isNameUnique = false; 
                    break;
                }
            }
            if (isNameUnique){
                obj.GetComponent<PlayerData>().playerName = name;
                break;
            }
            i++; 
        }
        cache.players.Add(obj.GetComponent<PlayerData>());
    }
    public void DeletePlayer(PlayerData player){
        cache.players.Remove(player);
        Destroy(player.gameObject); //TODO - confirm that this destroys references to playerData in, for example, actions
    }

    // ====== Tiles
    public void CreateTile(){
        var obj = LoadGameObject(Keywords.PREFAB_TILE, Keywords.SORTING_LAYER_TILES);
        cache.tiles.Add(obj.GetComponent<TileData>());
    }
    public void DeleteTile(TileData tile){
        cache.tiles.Remove(tile);
        Destroy(tile.gameObject); //TODO - confirm that this destroys references to playerData in, for example, actions
    }

    // ====== Actions
    public void CreateFinishGameAction(){
        GameObject action = LoadGameObject(Keywords.PREFAB_ACTION_FINISH_GAME, Keywords.SORTING_LAYER_ACTIONS);
        cache.actions.Add(action.GetComponent<FinishGameActionData>());
    }
    public void CreateChangePointsAction(){
        GameObject action = LoadGameObject(Keywords.PREFAB_ACTION_CHANGE_POINTS, Keywords.SORTING_LAYER_ACTIONS);
        cache.actions.Add(action.GetComponent<ChangePointsActionData>());
    }
    public void CreateMoveToAction(){
        GameObject action = LoadGameObject(Keywords.PREFAB_ACTION_MOVE_TO, Keywords.SORTING_LAYER_ACTIONS);
        cache.actions.Add(action.GetComponent<MoveToActionData>());
    }
    public void DeleteAction(ActionData action)
    {
        cache.actions.Remove(action);
        Destroy(action.gameObject);
    }

    // Connect Ports
    public void UpdateConnections(Transform node, Collider2D other)
    {
        if (!node.gameObject.GetComponent<EdgeData>().isConnected && !other.gameObject.GetComponent<EdgeData>().isConnected)
        {
            node.gameObject.GetComponent<EdgeData>().connectedEdge = null;
            other.gameObject.GetComponent<EdgeData>().connectedEdge = null;
        }
        else
        {
            node.gameObject.GetComponent<EdgeData>().connectedEdge = other.gameObject.GetComponent<EdgeData>();
            other.gameObject.GetComponent<EdgeData>().connectedEdge = node.gameObject.GetComponent<EdgeData>();
        }
    }

}