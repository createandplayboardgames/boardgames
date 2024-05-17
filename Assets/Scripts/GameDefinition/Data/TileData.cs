using System.Collections.Generic;
using UnityEngine;

public class TileData : MonoBehaviour
{
    public Transform left, right, up, down;
    public Transform tilePosition;
    private ActionData associatedAction = null;
    public bool shouldFinishGame = false;
    public string spritePath;  // Added for sprite save/load

    public bool IsEndingTile(){
        return shouldFinishGame; //TODO - also actions
    }

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

    /* TODO - save-load module
    public void SetData(TileInfo data)
    {
        this.transform.position = data.tileLocation;
        this.isEndingTile = data.isEndingTile;
        // For simplicity, only setting the flags without handling Transform connections
        this.left = data.leftOutgoing ? this.left : null;
        this.right = data.rightOutgoing ? this.right : null;
        this.up = data.upOutgoing ? this.up : null;
        this.down = data.downOutgoing ? this.down : null;
    }    
    */
}


