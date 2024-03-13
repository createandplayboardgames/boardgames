using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class TriggerUpdates : MonoBehaviour
{
    public GameDefinitionManager gameDefinitionManager;

    public TileData data;
    public TileData otherTileData;

    public DragDrop dragdrop;

    public Transform left;
    public Transform right;
    public Transform up;
    public Transform down;
    public Transform center;


    private void Start()
    {
        gameDefinitionManager = GameObject.Find("GameDefinitionManager").GetComponent<GameDefinitionManager>();
        data = gameObject.GetComponent<TileData>();
        dragdrop = gameObject.GetComponent<DragDrop>();
    }


    //Triggers for adding connection and checking location
    private void OnTriggerStay2D(Collider2D other)
    {
        if ( other.CompareTag("Center"))
        {
            dragdrop.overObject = true;
        }

        if (other.CompareTag("RightPort"))
        {       
            left.gameObject.GetComponent<EdgeData>().isConnected = true;      
            other.gameObject.GetComponent<EdgeData>().isConnected = true;
            gameDefinitionManager.UpdateConnections(left, other);
        }
        else if (other.CompareTag("LeftPort"))
        {
            //update tile data
            right.gameObject.GetComponent<EdgeData>().isConnected = true;
            other.gameObject.GetComponent<EdgeData>().isConnected = true;
            gameDefinitionManager.UpdateConnections(right, other);

        }
        else if (other.CompareTag("UpPort"))
        {
                //update tile data
            down.gameObject.GetComponent<EdgeData>().isConnected = true;
            other.gameObject.GetComponent<EdgeData>().isConnected = true;
            gameDefinitionManager.UpdateConnections(down, other);

        }
        else if (other.CompareTag("DownPort"))
        {
            //update tile data
            up.gameObject.GetComponent<EdgeData>().isConnected = true;
            other.gameObject.GetComponent<EdgeData>().isConnected = true;
            gameDefinitionManager.UpdateConnections(up, other);

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Center"))
        {
            dragdrop.overObject = false;
        }
        if (other.CompareTag("RightPort"))
        {
            left.gameObject.GetComponent<EdgeData>().isConnected = false;
            other.gameObject.GetComponent<EdgeData>().isConnected= false;
            gameDefinitionManager.UpdateConnections(left, other);
        }
        if (other.CompareTag("LeftPort"))
        {
            right.gameObject.GetComponent<EdgeData>().isConnected = false;
            other.gameObject.GetComponent<EdgeData>().isConnected = false;
            gameDefinitionManager.UpdateConnections(right, other);
        }
        if (other.CompareTag("UpPort"))
        {
            down.gameObject.GetComponent<EdgeData>().isConnected = false;
            other.gameObject.GetComponent<EdgeData>().isConnected = false;
            gameDefinitionManager.UpdateConnections(down, other);
        }
        if (other.CompareTag("DownPort"))
        {
            up.gameObject.GetComponent<EdgeData>().isConnected = false;
            other.gameObject.GetComponent<EdgeData>().isConnected = false;
            gameDefinitionManager.UpdateConnections(up, other);
        }
    }


}
