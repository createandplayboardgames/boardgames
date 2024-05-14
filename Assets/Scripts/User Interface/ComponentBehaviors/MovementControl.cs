using UnityEngine;
using System.Collections.Generic;

public class MovementControl : MonoBehaviour
{
    public PlayerData currentPlayer;
    public LayoutHelper layout;

    public List<TileData> travelled = new();
    public List<TileData> pathOptions = new();
    public bool moveAllowed = false;
    public static int spinner = 0;
    Ray ray;
    RaycastHit2D hit;

    public GameObject currentHit;

    public void Start()
    {
        layout = GameObject.Find("LayoutHelper").GetComponent<LayoutHelper>();
    }

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

    public void ColorDirection()
    {
        foreach (TileData tile in pathOptions)
        {
            tile.gameObject.GetComponent<SpriteRenderer>().color = new Color(0.5f, 1.5f, 1.5f, 0.5f);
        }
    }

    public void ClearColorDirection()
    {
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tiles");
        foreach (GameObject tile in tiles)
        {
            tile.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1.5f);
        }
    }

    public void Move(Ray ray, RaycastHit2D hit)
    {
        if (pathOptions.Contains(hit.collider.GetComponent<TileData>()) && moveAllowed)
        {
            Debug.Log("hit!");
            currentHit = hit.collider.gameObject;
            ClearColorDirection();
            currentPlayer.location = currentHit.gameObject.GetComponent<TileData>();
            layout.SnapPlayerToTile(currentPlayer, currentPlayer.location);
            moveAllowed = false;
        }
    }
    //public void MouseClickChecker(Ray ray, RaycastHit2D hit)
    //{
    //    hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
    //    if (hit.collider == null)
    //    {
    //        Debug.Log("nothing clicked");
    //    }
    //    else
    //    {
    //        if (hit.collider.CompareTag("Tiles"))
    //        {
    //            if (pathOptions.Contains(hit.collider.GetComponent<TileData>()) && moveAllowed)
    //            {
    //                Debug.Log("hit!");
    //                currentHit = hit.collider.gameObject;
    //                ClearColorDirection();
    //                currentPlayer.location = currentHit.gameObject.GetComponent<TileData>();
    //                layout.SnapPlayerToTile(currentPlayer, currentPlayer.location);
    //                moveAllowed = false;
    //            }
    //        }
    //    }
    //}
}

