using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEditor.Experimental.GraphView.GraphView;

public class GameDefinitionManager : MonoBehaviour
{
    // --Create Dictionary for game object and squaretile
    public Dictionary<GameObject, SquareTileData> tileList = new();
    SquareTileData squareTile;

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
    public void CreateTile(GameObject tileGameObject)
    {
        // TODO - create GameObject (from prefab)
        squareTile = tileGameObject.AddComponent<SquareTileData>();
        // Add to dictionary holding gameobject and data of square tiles
        tileList.Add(tileGameObject, squareTile);


    }
    public void DeleteTile(GameObject tileGameObject)
    {
        // TODO - delete TileData's GameObject
        tileList.Remove(tileGameObject);
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