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
    // get tiles
    public Transform[] tiles;

    Transform currentPos;

    [SerializeField] private float movementSpeed = 1f;

    // tile location
    [HideInInspector] public int tileIndex = 0;

    public bool moveAllowed = false;

    // initialize
    void Start()
    {
        tileData = tiles[tileIndex].GetComponent<TileData>();
        travelled.Add(tileData);
        transform.position = tiles[tileIndex].transform.position;
    }

    // TODO: FIX Update is called once per frame
    void Update()
    {
        if (moveAllowed) 
        { 
            if (!travelled.Contains(tileData))
            {
                travelled.Add(tileData);
                //Debug.Log(tileData);
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



    public void Move(GameObject tile)
    {
        transform.position = tile.transform.position;
        updateCurrentTile(tile);

    }
}

