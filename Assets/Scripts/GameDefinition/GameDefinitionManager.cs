using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEditor.Experimental.GraphView.GraphView;

public class GameDefinitionManager : MonoBehaviour
{
    public int tilecount = 0;

    // --- Players 
    public void CreatePlayer()
    {
        // TODO - create GameObject (from prefab)
        GameObject newPlayer = Instantiate(Resources.Load("PlayerPiece"), new Vector3(2, 5, 0), Quaternion.identity) as GameObject;
        newPlayer.transform.SetParent(GameObject.FindGameObjectWithTag("Tokens").transform, false);
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
        GameObject newTile = Instantiate(Resources.Load("SquareTile"), new Vector3(-2, -6, 0), Quaternion.identity) as GameObject;
        newTile.transform.SetParent(GameObject.FindGameObjectWithTag("Tiles").transform, false);
        newTile.name = "SquareTile" + tilecount;
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
    int SpinSpinner()
    {
        // TODO - animate spinner (?)
        return 1;
    }

}