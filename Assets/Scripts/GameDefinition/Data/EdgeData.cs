using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EdgeData : MonoBehaviour
{
    
    public Transform edge;

    public bool isConnected = false;

    public GameObject GetTile()
    {
        return edge.transform.parent.gameObject;
    }
}