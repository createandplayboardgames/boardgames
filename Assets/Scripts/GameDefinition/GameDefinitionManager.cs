using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEditor.Experimental.GraphView.GraphView;

public class GameDefinitionManager : MonoBehaviour
{
    // TODO - this is data, and should go elsewhere
    public List<PlayerData> players = new List<PlayerData>();

    public int tilecount = 0;

    private GameObject loadGamePiece(string pieceName, string parentName)
    {
        var parent = GameObject.Find(parentName);
        GameObject gamePiece = Instantiate(Resources.Load(pieceName),
            parent.transform.position, parent.transform.rotation) as GameObject;
            gamePiece.transform.parent = parent.transform;
        gamePiece.GetComponent<SpriteRenderer>().sortingLayerName = parent.GetComponent<SpriteRenderer>().sortingLayerName;
        return gamePiece;
    }

    // --- Players 
    public void CreatePlayer()
    {
        GameObject player = loadGamePiece("Player", "Players");
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
        loadGamePiece("SquareTile", "Tileset");
        tilecount++;
    }
    public void DeleteTile()
    {
        // TODO - delete TileData's GameObject
        tilecount--;
       
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


    // --- Spinner
    int SpinSpinner()
    {
        // TODO - animate spinner (?)
        return 1;
    }

}