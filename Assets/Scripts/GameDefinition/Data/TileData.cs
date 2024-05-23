using System.Collections.Generic;
using UnityEngine;

public class TileData : GameItemData
{

    // Get all current tile edges that have a connection.
    public List<EdgeData> GetAllOutgoingConnections()
    {
        List<EdgeData> outgoingConnections = new List<EdgeData>();
        for (int i = 0; i < gameObject.transform.childCount; i++){
            GameObject child = gameObject.transform.GetChild(i).gameObject;
            if (child.TryGetComponent<EdgeData>(out EdgeData edge) && edge.isConnected)
                outgoingConnections.Add(edge);
        }
        return outgoingConnections;
    }
    // Get all connected tiles
    public List<TileData> GetAllIncomingConnections()
    {
        List<TileData> incomingConnections = new List<TileData>();
        for (int i = 0; i < gameObject.transform.childCount; i++){
            GameObject child = gameObject.transform.GetChild(i).gameObject;
            if (child.TryGetComponent<EdgeData>(out EdgeData edge) && edge.isConnected)
                incomingConnections.Add(edge.GetComponent<EdgeData>().connectedEdge.GetTile()); 
        }
        return incomingConnections;
    }


}


