using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Moves player pieces along board
 */
public class Movement : MonoBehaviour
{
    public TileData tileData;
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
        transform.position = tiles[tileIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (moveAllowed) 
        { 
            tileData = tiles[tileIndex].GetComponent<TileData>();
            //GetMovementOptions(tileIndex, tileData);
            Move(); 
        }
    }

    public void GetCurrentPos()
    {
        currentPos.position = tiles[tileIndex].transform.position;
    }



    public void Move()
    {
        if (tileIndex <= tiles.Length - 1)
        {
            transform.position = Vector2.MoveTowards(transform.position,
                tiles[tileIndex].transform.position, movementSpeed * Time.deltaTime);

            if (transform.position == tiles[tileIndex].transform.position)
            {
                tileIndex += 1;
            }
        }
    }
}

