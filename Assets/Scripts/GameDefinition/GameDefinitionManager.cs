using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEditor.Experimental.GraphView.GraphView;

public class GameDefinitionManager : MonoBehaviour
{

    // --- Players 
    void CreatePlayer()
    {
        // TODO - create GameObject (from prefab)
    }
    void DeletePlayer(PlayerData player)
    {
        // TODO - delete PlayerData's GameObject
    }
    void MovePlayer(PlayerData player, SquareTileData tile)
    {
        //TODO - move PlayerData's GameObject to TileData's GameObject
    }


    // --- Tiles
    void CreateTile()
    {
        // TODO - create GameObject (from prefab)
    }
    void DeleteTile(SquareTileData tile)
    {
        // TODO - delete TileData's GameObject
    }
    void ConnectPorts(ConnectableSide.OutgoingPort outgoing, ConnectableSide.IncomingPort incoming)
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


