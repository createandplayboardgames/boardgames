
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileData : MonoBehaviour
{
    private GameDefinitionManager gameDefinitionManager;

    public Transform tilePosition;
    public Transform left;
    public Transform right;
    public Transform up;
    public Transform down;

    public bool isEndingTile = false;

    private GameAction associatedAction = null;
    public Boolean shouldFinishGame = false;



    public bool IsEndingTile()
    {
        return associatedAction is FinishGameAction || shouldFinishGame;
    }
    public GameAction getAssociatedAction()
    {
        return associatedAction;
    }

    // Get all current tile edges that have a connection.
    public void GetAllOutgoingConnections()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            GameObject child = gameObject.transform.GetChild(i).gameObject;
            if (child.TryGetComponent<EdgeData>(out EdgeData edge) && edge.isConnected)
            { Debug.Log(edge.edge); }
        }
    }

    // Get all connected tiles
    public void GetAllIncomingConnections()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            GameObject child = gameObject.transform.GetChild(i).gameObject;
            if (child.TryGetComponent<EdgeData>(out EdgeData edge) && edge.isConnected)
            { Debug.Log(edge.connectedEdge.GetTile()); }
        }
    }


    // Returns tile directly above current tile
    public TileData Up()
    {
        EdgeData edge = up.gameObject.GetComponent<EdgeData>().connectedEdge;
        if (edge != null)
        {
            Debug.Log(edge.GetTile());
            return edge.GetTile();
        }
        else return null;
    }

    // Returns tile directly below current tile
    public TileData Down()
    {
        EdgeData edge = down.gameObject.GetComponent<EdgeData>().connectedEdge;
        if (edge != null)
        {
            Debug.Log(edge.GetTile());
            return edge.GetTile();
        }
        else return null;

    }

    // Returns tile directly right of the current tile
    public TileData Right()
    {
        EdgeData edge = right.gameObject.GetComponent<EdgeData>().connectedEdge;
        if (edge != null)
        {
            Debug.Log(edge.GetTile());
            return edge.GetTile();
        }
        else return null;

    }
    
    // Returns tile directly left of the current tile
    public TileData Left()
    {
        EdgeData edge = left.gameObject.GetComponent<EdgeData>().connectedEdge;
        if (edge != null)
        {
            Debug.Log(edge.GetTile());
            return edge.GetTile();
        }
        else return null;

    }

}


