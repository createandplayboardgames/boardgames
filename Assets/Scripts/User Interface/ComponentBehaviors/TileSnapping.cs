using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class TileSnapping : MonoBehaviour
{
    TileData data;
    public Transform left;
    public Transform right;
    public Transform up;
    public Transform down;

    public bool allowInteraction = false;

    public Vector3 otherNodePosition;
    public Vector3 nodePosition;


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
            nodePosition = left.transform.position;
            GetPosition();
        }
        else if (other.CompareTag("LeftPort"))
        {
            nodePosition = right.transform.position;
            GetPosition();
        }
        else if (other.CompareTag("UpPort"))
        {
            nodePosition = down.transform.position;
            GetPosition();
        }
        else if (other.CompareTag("DownPort"))
        {
            nodePosition = up.transform.position;
            GetPosition();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        allowInteraction = false;
        if (other.CompareTag("RightPort"))
        {
            Debug.Log("port_right");
        }
        if (other.CompareTag("LeftPort"))
        {
            Debug.Log("port_left");
        }
        if (other.CompareTag("UpPort"))
        {
            Debug.Log("port_top");
        }
        if (other.CompareTag("DownPort"))
        {
            Debug.Log("port_bottom");
        }
    }


}
