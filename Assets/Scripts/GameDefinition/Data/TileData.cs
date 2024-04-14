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


    // Returns tile directly above current tile
    public TileData Up()
    {
        if (up != null)
        {
            EdgeData edge = up.gameObject.GetComponent<EdgeData>().connectedEdge;
            if (edge != null)
            {
                return edge.GetTile();
            }
        }
        return null;
    }

    // Returns tile directly below current tile
    public TileData Down()
    {
        if (down != null)
        {
            EdgeData edge = down.gameObject.GetComponent<EdgeData>().connectedEdge;
            if (edge != null)
            {
                return edge.GetTile();
            }
        }
        return null;

    }

    // Returns tile directly right of the current tile
    public TileData Right()
    {
        if (right != null)
        {
            EdgeData edge = right.gameObject.GetComponent<EdgeData>().connectedEdge;
            if (edge != null)
            {
                return edge.GetTile();
            }
        }
        return null;

    }

    // Returns tile directly left of the current tile
    public TileData Left()
    {
        if (left != null)
        {
            EdgeData edge = left.gameObject.GetComponent<EdgeData>().connectedEdge;
            if (edge != null)
            {
                return edge.GetTile();
            }
        }
        return null;
    }

}

