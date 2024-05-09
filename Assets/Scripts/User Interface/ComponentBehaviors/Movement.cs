using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Moves player pieces along board
 */
public class Movement : MonoBehaviour
{
    public TileData tileData;
    public List<TileData> travelled = new List<TileData>();
    public List<TileData> pathOptions = new List<TileData>();

    Transform currentPos;

    // tile location
    [HideInInspector] public int tileIndex = 0;

    public bool moveAllowed = false;

    // initialize
    void Start()
    {
        tileData = tileData.GetComponent<TileData>();
        travelled.Add(tileData);
    }

    //TODO: Fix the travelled list
    void Update()
    {
        if (moveAllowed) 
        { 
            if (!travelled.Contains(tileData))
            {
                travelled.Add(tileData);
            } 
        }
    }

    public void updateCurrentTile(GameObject tile)
    {
        tileData = tile.GetComponent<TileData>();
    }

    public void GetCurrentPos(GameObject tile)
    {
        currentPos.position = tile.transform.position;
    }

}

