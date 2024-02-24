using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEditor.Experimental.GraphView.GraphView;

public class GameDefinitionManager : MonoBehaviour
{
    // TODO - this is data, and should go elsewhere
    public List<PlayerData> players = new List<PlayerData>();

    public int tilecount = 0;

    private GameObject loadGamePiece(string pieceName)
    {
        var board = GameObject.Find("Board");
        GameObject gamePiece = Instantiate(Resources.Load(pieceName),
            board.transform.position, board.transform.rotation) as GameObject;
        gamePiece.transform.parent = board.transform;
        return gamePiece;
    }

    // --- Players 
    public void CreatePlayer()
    {
        GameObject player = loadGamePiece("Player");
        players.Add(player.GetComponent<PlayerData>());
    }
    void DeletePlayer(PlayerData player)
    {
        // TODO - delete PlayerData's GameObject
    }
    void MovePlayer(PlayerData player, TileData tile)
    {
        //TODO - move PlayerData's GameObject to TileData's GameObject
    }


    // --- Tiles
    public void CreateTile()
    {
        loadGamePiece("Tile");
        tilecount++;
    }



    public void DeleteTile()
    {
        // TODO - delete TileData's GameObject
        tilecount--;
       
    }

    public void ConnectPorts(ConnectableSide.OutgoingPort outgoing, ConnectableSide.IncomingPort incoming)
    {
        // TODO - validation
        outgoing.connectedTo = incoming;
        // TODO - update views
    }


    // --- Spinner
    public int SpinSpinner()
    {
        // TODO - animate spinner (?)
        return 1;
    }

 
}