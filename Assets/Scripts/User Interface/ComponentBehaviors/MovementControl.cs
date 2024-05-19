using UnityEngine;
using System.Collections.Generic;

public class MovementControl : MonoBehaviour
{
    public PlayerData currentPlayer;
    public LayoutHelper layout;

    public List<TileData> pathOptions = new();

    public GameObject currentHit;

    public void Start()
    {
        layout = GameObject.Find("LayoutHelper").GetComponent<LayoutHelper>();
    }

    // Find all available paths.
    public void GetMovementOptions(int roll, TileData tile)
    {
        if (roll <= 0)
        {
            return;
        }
        List<TileData> nextTiles = tile.gameObject.GetComponent<TileData>().GetAllIncomingConnections();
        foreach (TileData nextTile in nextTiles)
        {
            if (!pathOptions.Contains(nextTile))
            {
                pathOptions.Add(nextTile);
            }
            GetMovementOptions(roll - 1, nextTile);
        }
    }

    // Highlight Paths.
    public void ColorDirection()
    {
        foreach (TileData tile in pathOptions)
        {
            tile.gameObject.GetComponent<SpriteRenderer>().color = new Color(0.5f, 1.5f, 1.5f, 0.5f);
        }
    }

    // Clear highlighted paths.
    public void ClearColorDirection()
    {
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tiles");
        foreach (GameObject tile in tiles)
        {
            tile.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1.5f);
        }
    }

    // Move Player to clicked tile.
    public void Move(Ray ray, RaycastHit2D hit)
    {
        if (pathOptions.Contains(hit.collider.GetComponent<TileData>()))
        {
            Debug.Log("hit!");
            currentHit = hit.collider.gameObject;
            ClearColorDirection();
            currentPlayer.location = currentHit.gameObject.GetComponent<TileData>();
            layout.SnapPlayerToTile(currentPlayer, currentPlayer.location);
        }
    }

}

