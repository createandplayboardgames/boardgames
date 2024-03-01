
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileData : MonoBehaviour
{

    GameDefinitionManager gameDefinitionManager;

    // Get nodes and components from tile node points
    public Transform left;
    public Transform right;
    public Transform up;
    public Transform down;
    public Transform center;

    Collider2D sourceCollider;

    // For two direction options: left to right or up to down.
    public bool left_incoming;
    public bool right_outgoing;
    public bool up_incoming;
    public bool down_outgoing;
    
    public ConnectableSide port_left;
    public ConnectableSide port_right;
    public ConnectableSide port_top;
    public ConnectableSide port_bottom;

    private GameAction associatedAction = null;
    public Boolean shouldFinishGame = false;
        
    private void Start()
    {
        
    }

    public void Update()
    {
        
    }

    /*
    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("RightPort"))
        {
            left_incoming = true;
            Debug.Log("port_right");
        }
        if (other.CompareTag("LeftPort"))
        {
            right_outgoing = true;
            Debug.Log("port_left");
        }
        if (other.CompareTag("UpPort"))
        {
            down_outgoing = true;
            Debug.Log("port_top");
        }
        if (other.CompareTag("DownPort"))
        {
            up_incoming = true;
            Debug.Log("port_bottom");
        }
        
    }
    */

    public bool IsEndingTile()
    {
        return associatedAction is FinishGameAction || shouldFinishGame;
    }
    public GameAction getAssociatedAction(){
        return associatedAction;
    }
}

public class ConnectableSide
{
    public OutgoingPort outgoing;
    public IncomingPort incoming;
    public class OutgoingPort { public IncomingPort connectedTo = null; }
    public class IncomingPort { public Boolean hasConnection = false; }

}
