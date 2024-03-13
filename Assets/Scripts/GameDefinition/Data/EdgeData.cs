using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EdgeData : MonoBehaviour
{
    
    public Transform edge;

    public EdgeData connectedEdge;
    public bool isConnected = false;

    //Get Parent Object Tile of current edge
    public TileData GetTile()
    {
        return edge.transform.parent.gameObject.GetComponent<TileData>();
    }
}