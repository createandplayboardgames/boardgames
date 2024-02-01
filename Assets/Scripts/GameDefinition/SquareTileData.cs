
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SquareTileData : MonoBehaviour
{
    public ConnectableSide port_left = new();
    public ConnectableSide port_right = new();
    public ConnectableSide port_top = new();
    public ConnectableSide port_bottom = new();
    public Boolean isEndingTile = false;
}

public class ConnectableSide
{
    public OutgoingPort outgoing;
    public IncomingPort incoming;
    public class OutgoingPort { public IncomingPort connectedTo =  null; }
    public class IncomingPort { public Boolean hasConnection = false;  }

}