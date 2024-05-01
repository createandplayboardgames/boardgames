using System.Collections.Generic;
using UnityEngine;

public class TileData : MonoBehaviour
{
    public Transform tilePosition;
    public Transform left;
    public Transform right;
    public Transform up;
    public Transform down;

    public bool isEndingTile = false;

    private GameAction associatedAction = null;
    public bool shouldFinishGame = false;

    public bool IsEndingTile()
    {
        return associatedAction is FinishGameAction || shouldFinishGame;
    }
    public GameAction GetAssociatedAction()
    {
        return associatedAction;
    }

    // Get all current tile edges that have a connection.
    public List<EdgeData> GetAllOutgoingConnections()
    {
        List<EdgeData> outgoingConnections = new List<EdgeData>();
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            GameObject child = gameObject.transform.GetChild(i).gameObject;
            if (child.TryGetComponent<EdgeData>(out EdgeData edge) && edge.isConnected)
            {
                outgoingConnections.Add(edge);
            }
        }
        return outgoingConnections;
    }

    // Get all connected tiles
    public List<TileData> GetAllIncomingConnections()
    {
        List<TileData> incomingConnections = new List<TileData>();
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            GameObject child = gameObject.transform.GetChild(i).gameObject;
            if (child.TryGetComponent<EdgeData>(out EdgeData edge) && edge.isConnected)
            { incomingConnections.Add(edge.GetComponent<EdgeData>().connectedEdge.GetTile()); }
        }
        return incomingConnections;
    }

    //Block Tile connections
    //TODO: Test (get adjacent tiles to block their connection instead?)
    public void BlockAllConnections()
    {
        List<EdgeData> edges = GetAllOutgoingConnections();
        for (int i = 0; i < edges.Count; i++)
        {
            edges[i].isConnected = false;
        }
    }

}

