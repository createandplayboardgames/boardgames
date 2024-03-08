using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class ConnectableSide : MonoBehaviour
{
    private List<EdgeData> incomingEdges;
    private List<EdgeData> outgoingEdges;

    public ConnectableSide() 
    {
        incomingEdges = new List<EdgeData>();
        outgoingEdges = new List<EdgeData>();
    }

    public void ConnectIncomingEdge(EdgeData node)
    {
        incomingEdges.Add(node);
    }

    public void ConnectOutGoingEdge(EdgeData node)
    {
        outgoingEdges.Add(node);
    }
    
    public void GetAllOutgoingConnections()
    {
        foreach(EdgeData i in outgoingEdges)
        {
            Debug.Log(i);
        }
    }

    public void GetAllIncomingConnections()
    {
        foreach(EdgeData i in incomingEdges)
        {
            Debug.Log(i);
        }
    }

    public void Up()
    {
        //If Up node is connected, find Down Node in connections and get parent of node
        EdgeData edge = GetEdge("Down");
        Debug.Log(edge.GetTile());

    }

    public void Down()
    {
        //If Down node is connected, find Up Node in connections and get parent of node
        EdgeData edge = GetEdge("Up");
        Debug.Log(edge.GetTile());
    }

    public void Right()
    {
        //If Right node is connected, find Left Node in connections and get parent of node
        EdgeData edge = GetEdge("Left");
        Debug.Log(edge.GetTile());
    }

    public void Left()
    {
        //If Left node is connected, find Right Node in connections and get parent of node
        EdgeData edge = GetEdge("Right");
        Debug.Log(edge.GetTile());
    }

    public EdgeData GetEdge(string nodeName)
    {
        foreach(EdgeData i in incomingEdges)
        {
            if (i.name == nodeName)
            {
                return i;
            }
        }
        return null;
    }
}