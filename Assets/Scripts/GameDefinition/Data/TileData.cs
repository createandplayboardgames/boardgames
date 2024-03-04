
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

    // Get nodes and components from tile node points and gets positions of nodes
    public Transform tilePosition;

    public GameObject adjacentTile = null;

    public List<GameObject> adjacentTiles = new List<GameObject>();

    // For two direction options: left to right or up to down.
    public bool left_outgoing;
    public bool right_outgoing;
    public bool up_outgoing;
    public bool down_outgoing;


    public ConnectableSide port_left;
    public ConnectableSide port_right;
    public ConnectableSide port_top;
    public ConnectableSide port_bottom;
    public bool isEndingTile = false;
    public bool newTile = false;

    private GameAction associatedAction = null;
    public Boolean shouldFinishGame = false;


    public void Update()
    {
        if (newTile && adjacentTile != null)
        {
            AddAdjacentTile(adjacentTile);
        }
        else if (!newTile && adjacentTile!=null)
        {
            RemoveAdjacentTile(adjacentTile);
            adjacentTile = null;
        }

    }

    // add adjacent tile if not currently in list
    public void AddAdjacentTile(GameObject tile)
    {
        if (!adjacentTiles.Contains(tile))
        {
            adjacentTiles.Add(tile);
        }
    }

    // remove tile if currently in adjacent tile list
    public void RemoveAdjacentTile(GameObject tile)
    {
        if (adjacentTiles.Contains(tile))
        {
            adjacentTiles.Remove(tile);
        }
    }

    public bool IsEndingTile()
    {
        return associatedAction is FinishGameAction || shouldFinishGame;
    }
    public GameAction getAssociatedAction()
    {
        return associatedAction;
    }
}

public class ConnectableSide : MonoBehaviour
{
    public OutgoingPort outgoing;
    public IncomingPort incoming;
    public class OutgoingPort
    {
        public IncomingPort connectedTo = null;
    }
    public class IncomingPort
    {
        public Boolean hasConnection = false;
    }
}

