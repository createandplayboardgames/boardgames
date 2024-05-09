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
    private GameObject LoadGameObject(string prefabName, String sortingLayerName)
    {
        //place in board
        Debug.Log("LoadGameObject(), loading... " + prefabName);
        var parent = GameObject.Find(Keywords.GAMEOBJECT_BOARD);
        GameObject gamePiece = Instantiate(Resources.Load(prefabName),
            parent.transform.position, parent.transform.rotation) as GameObject;
        gamePiece.transform.parent = parent.transform;
        
        //place in camera center
        Vector3 position = GameObject.Find("Main Camera").transform.position;
        position.z = gamePiece.transform.position.z;
        gamePiece.transform.position = position;  

        //assign sorting layer and sorting order
        gamePiece.GetComponent<SpriteRenderer>().sortingLayerName = sortingLayerName;
        gamePiece.GetComponent<SpriteRenderer>().sortingOrder = cache.itemsLoadedCount; // put in front, always

        //TODO - select (currently won't work with actions)

        cache.itemsLoadedCount++;
        return gamePiece;
    }
    public void DeleteGamePiece(GameObject obj){
        switch (GetGamePieceData(obj)){
            case PlayerData data:               DeletePlayer(data); break;
            case TileData data:                 DeleteTile(data); break;
            case ActionData data:               DeleteAction(data); break;
            default:                            Debug.Log("invalid item requested deletion"); break;
        }
    }
    public void AssignSprite(GameObject obj, String assetPath)
    {
        obj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(assetPath);
    }
    public static Component GetGamePieceData(GameObject obj){
        Component data = obj.GetComponent<PlayerData>()             as Component ??
                         obj.GetComponent<TileData>()               as Component ??
                         obj.GetComponent<FinishGameActionData>()   as Component ??
                         obj.GetComponent<ChangePointsActionData>() as Component ??
                         obj.GetComponent<MoveToActionData>()       as Component ??
                         obj.GetComponent<BlockPathActionData>()    as Component;
        return data;
    }


    // ====== Players 
    public PlayerData CreatePlayer(){
        if (cache.players.Count > MAX_PLAYER_COUNT)
            return null; //TODO - show error message
        var obj = LoadGameObject(Keywords.PREFAB_PLAYER, Keywords.SORTING_LAYER_PLAYERS);
        AssignUniquePlayerName(obj.GetComponent<PlayerData>());
        cache.players.Add(obj.GetComponent<PlayerData>());
        return obj.GetComponent<PlayerData>();
    }
    public void DeletePlayer(PlayerData player){
        cache.players.Remove(player);
        Destroy(player.gameObject); //TODO - confirm that this destroys references to playerData in, for example, actions
    }
    private void AssignUniquePlayerName(PlayerData player){
        // Give the player a unique name, of form "player-num"
        // TODO - this needs testing!
        for (int i = 0; ; i++){
            String name = "player-" + i;  // choose potential name
            bool isNameUnique = true; //check if name unique...
            foreach (PlayerData p in cache.players){ //... against all current names
                if (p.playerName == name) {
                    isNameUnique = false; 
                    break;
                }
            }
            if (isNameUnique){ //name was unique, assign it!
                player.playerName = name;
                return;
            }
        }
    }

    // ====== Tiles
    public TileData CreateTile(){
        var obj = LoadGameObject(Keywords.PREFAB_TILE, Keywords.SORTING_LAYER_TILES);
        cache.tiles.Add(obj.GetComponent<TileData>());
        return obj.GetComponent<TileData>();
    }
    public void DeleteTile(TileData tile){
        cache.tiles.Remove(tile);
        Destroy(tile.gameObject); //TODO - confirm that this destroys references to playerData in, for example, actions
    }
    public void UpdateConnections(Transform node, Collider2D other)
    {
        if (!node.gameObject.GetComponent<EdgeData>().isConnected && !other.gameObject.GetComponent<EdgeData>().isConnected){
            node.gameObject.GetComponent<EdgeData>().connectedEdge = null;
            other.gameObject.GetComponent<EdgeData>().connectedEdge = null;
        } else {
            node.gameObject.GetComponent<EdgeData>().connectedEdge = other.gameObject.GetComponent<EdgeData>();
            other.gameObject.GetComponent<EdgeData>().connectedEdge = node.gameObject.GetComponent<EdgeData>();
        }
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
    public void CreateBlockPathAction(){
        GameObject action = LoadGameObject(Keywords.PREFAB_ACTION_BLOCK_PATH, Keywords.SORTING_LAYER_ACTIONS);
        cache.actions.Add(action.GetComponent<BlockPathActionData>());
    }
    public void DeleteAction(ActionData action){
        cache.actions.Remove(action);
        Destroy(action.gameObject);
    }

}