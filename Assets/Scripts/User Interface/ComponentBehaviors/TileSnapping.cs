using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    public bool allowInteraction = false;

    public Vector3 otherNodePosition;
    public Vector3 nodePosition;

    private void Start()
    {
        data = GetComponent<TileData>();
    }

    private void Update()
    {

    }

    private void GetPosition()
    {
        if (allowInteraction)
        {
            gameObject.transform.position += (otherNodePosition - nodePosition);
        }
    }



    private void OnTriggerStay2D(Collider2D other)
    {
        allowInteraction = true;
        otherNodePosition = other.transform.position;
        if (other.CompareTag("RightPort"))
        {
            //update tile data
            data.left_outgoing = true;
            data.newTile = true;
            data.adjacentTile = other.transform.parent.gameObject;
            //
            nodePosition = left.transform.position;
            GetPosition();
        }
        else if (other.CompareTag("LeftPort"))
        {
            //update tile data
            data.right_outgoing = true;
            data.newTile = true;
            data.adjacentTile = other.transform.parent.gameObject;
            //
            nodePosition = right.transform.position;
            GetPosition();
        }
        else if (other.CompareTag("UpPort"))
        {
            //update tile data
            data.down_outgoing = true;
            data.newTile = true;
            data.adjacentTile = other.transform.parent.gameObject;
            //
            nodePosition = down.transform.position;
            GetPosition();
        }
        else if (other.CompareTag("DownPort"))
        {
            //update tile data
            data.up_outgoing = true;
            data.newTile = true;
            data.adjacentTile = other.transform.parent.gameObject;
            //
            nodePosition = up.transform.position;
            GetPosition();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        allowInteraction = false;
        if (other.CompareTag("RightPort"))
        {
            data.adjacentTile = other.transform.parent.gameObject;
            data.newTile = false;
            data.left_outgoing = false;
            Debug.Log("port_right exit");
        }
        if (other.CompareTag("LeftPort"))
        {
            data.adjacentTile = other.transform.parent.gameObject;
            data.newTile = false;
            data.right_outgoing = false;
            Debug.Log("port_left exit");
        }
        if (other.CompareTag("UpPort"))
        {
            data.adjacentTile = other.transform.parent.gameObject;
            data.newTile = false;
            data.down_outgoing = false;
            Debug.Log("port_top exit");
        }
        if (other.CompareTag("DownPort"))
        {
            data.adjacentTile = other.transform.parent.gameObject;
            data.newTile = false;
            data.up_outgoing = false;
            Debug.Log("port_bottom exit");
        }
    }


}
