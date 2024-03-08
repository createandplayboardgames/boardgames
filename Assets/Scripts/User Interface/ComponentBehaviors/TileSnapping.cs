using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class TileSnapping : MonoBehaviour
{
    public GameDefinitionManager gameDefinitionManager;
    public TileData data;
    public TileData otherTileData;
    public Transform left;
    public Transform right;
    public Transform up;
    public Transform down;

    public DragDrop dragdrop;

    public bool allowInteraction = false;

    public Vector3 otherNodePosition;
    public Vector3 nodePosition;

    private void Start()
    {
        data = gameObject.GetComponent<TileData>();
        dragdrop = gameObject.GetComponent<DragDrop>();

    }

    //Set tile to snap next to other tile if allowed
    public void GetPosition()
    {
        if (allowInteraction)
        {
            gameObject.transform.position += (otherNodePosition - nodePosition);
        }
        allowInteraction = false;
    }

    //add connection to ConnectableSide
    public void UpdateConnections(Transform node, Collider2D other)
    {
        ConnectableSide tile1 = gameObject.GetComponent<ConnectableSide>();
        ConnectableSide tile2 = other.transform.parent.GetComponent<ConnectableSide>();
        EdgeData node1 = node.transform.GetComponent<EdgeData>();
        EdgeData node2 = other.transform.GetComponent<EdgeData>();

        gameDefinitionManager.ConnectPorts(tile1, node1, tile2, node2);

    }




    //Triggers for adding connection and snapping tiles together
    private void OnTriggerStay2D(Collider2D other)
    {
        gameObject.GetComponent<TileSnapping>().allowInteraction = true;
         otherNodePosition = other.transform.position;

            if (other.CompareTag("RightPort") && !other.gameObject.GetComponent<EdgeData>().isConnected)
            {
                //
                left.gameObject.GetComponent<EdgeData>().isConnected = true;
                //
                nodePosition = left.transform.position;
                GetPosition();
            }
            else if (other.CompareTag("LeftPort") && !other.gameObject.GetComponent<EdgeData>().isConnected)
            {
                //update tile data
                right.gameObject.GetComponent<EdgeData>().isConnected = true;


                //
                nodePosition = right.transform.position;
                GetPosition();
            }
            else if (other.CompareTag("UpPort") && !other.gameObject.GetComponent<EdgeData>().isConnected)
            {
                //update tile data
                down.gameObject.GetComponent<EdgeData>().isConnected = true;

                //
                nodePosition = down.transform.position;
                GetPosition();
            }
            else if (other.CompareTag("DownPort") && !other.gameObject.GetComponent<EdgeData>().isConnected)
            {
                //update tile data
                up.gameObject.GetComponent<EdgeData>().isConnected = true;

                //
                nodePosition = up.transform.position;
                GetPosition();
            }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        gameObject.GetComponent<TileSnapping>().allowInteraction = false;
        if (other.CompareTag("RightPort"))
        {
            left.gameObject.GetComponent<EdgeData>().isConnected = false;

            Debug.Log("port_right exit");
        }
        if (other.CompareTag("LeftPort"))
        {
            right.gameObject.GetComponent<EdgeData>().isConnected = false;

            Debug.Log("port_left exit");
        }
        if (other.CompareTag("UpPort"))
        {
            down.gameObject.GetComponent<EdgeData>().isConnected = false;

            Debug.Log("port_top exit");
        }
        if (other.CompareTag("DownPort"))
        {
            up.gameObject.GetComponent<EdgeData>().isConnected = false;

            Debug.Log("port_bottom exit");
        }
    }


}
