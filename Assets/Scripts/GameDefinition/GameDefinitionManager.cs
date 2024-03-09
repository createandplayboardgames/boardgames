using System;
using System.Collections.Generic;
using UnityEngine;

public class GameDefinitionManager : MonoBehaviour
{
    // TODO - this is data, and should go elsewhere
    public List<PlayerData> players = new();
    private readonly int MAX_PLAYER_COUNT = 4;
    public int tilecount = 0;
    public static PlayerData RANDOM_PLAYER_DUMMY = new("RANDOM PLAYER"); 
    public static PlayerData CURRENT_PLAYER_DUMMY = new("CURRENT PLAYER"); //TODO - MUST be saved, in order for play game to work
    public List<PlayerData> GetAllPlayersAndDummies()
    {
        List<PlayerData> all = new();
        all.Add(GameDefinitionManager.RANDOM_PLAYER_DUMMY); //add dummy classes
        all.Add(GameDefinitionManager.CURRENT_PLAYER_DUMMY);
        all.AddRange(players); //copy
        return all;
    }


    private GameObject LoadGameItem(string pieceName)
    {
        var parent = GameObject.Find("Board");
        GameObject gamePiece = Instantiate(Resources.Load(pieceName),
            parent.transform.position, parent.transform.rotation) as GameObject;
        gamePiece.transform.parent = parent.transform;
        //gamePiece.GetComponent<SpriteRenderer>().sortingLayerName = parent.GetComponent<SpriteRenderer>().sortingLayerName;
        //TODO - set sorting layer, by type! VERY IMPORTANT!

        //TODO - because the camera can pan and zoom, we should instantiate objects in the middle of the camera 
        //ideally without overlapping others

        gamePiece.GetComponent<Select>()?.SelectPiece();
        return gamePiece;
    }

    // --- Players 
    public void CreatePlayer()
    {
        if (players.Count > MAX_PLAYER_COUNT){
            //TODO - display message
            return; 
        }
        GameObject player = LoadGameItem("Player");
        players.Add(player.GetComponent<PlayerData>());
        Debug.Log("added player to players");
    }
    void DeletePlayer(PlayerData player)
    {
        players.Remove(player);
        Destroy(player.gameObject);
        //TODO - delete from actions which held references to this player
    }
    void MovePlayer(PlayerData player, TileData tile)
    {
        player.location = tile;
        //TODO - move PlayerData's GameObject to TileData's GameObject
    }


    // --- Tiles
    public void CreateTile()
    {
        LoadGameItem("Tile");
        tilecount++;
    }



    public void DeleteTile(TileData tileData)
    {
        Destroy(tileData.gameObject);
        tilecount--;
       
    }

    public void ConnectPorts(ConnectableSide.OutgoingPort outgoing, ConnectableSide.IncomingPort incoming)
    {
        // TODO - validation
        outgoing.connectedTo = incoming;
        // TODO - update views
    }


    

    // --- Actions
    internal void CreateFinishGameAction()
    {
        LoadGameItem("ActionFinishGame");
    }

    internal void CreateChangePointsAction()
    {
        LoadGameItem("ActionChangePoints");
    }

    internal void CreateMoveToAction()
    {
        LoadGameItem("ActionMoveTo");
    }
}