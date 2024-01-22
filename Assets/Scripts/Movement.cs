using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Transform[] tiles; // Update with created GameBoard

    private float movementSpeed = 1f; // Update with movement speed for game

    public Vector3 playerPosition;

    public bool movementAllow = false;


    private void Update()
    {
        if (movementAllow)
            // get player position
            Move();
    }

    private void Move()
    {
            // Do some movement
            // Get position x,y coordinates
            // Change cooridinates and move and save
    }
}
