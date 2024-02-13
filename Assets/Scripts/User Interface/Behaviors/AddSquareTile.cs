using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * TODO: add square tile prefab to board, 
 * connect to drag and drop, 
 * connect to sqauretiledata
 */
public class AddSquareTile : MonoBehaviour
{
    private int i = 0;
    public void SpawnTile()
    {
        GameObject newTile = Instantiate(Resources.Load("SquareTile"), new Vector3(-2, -6, 0), Quaternion.identity) as GameObject;
        newTile.transform.SetParent(GameObject.FindGameObjectWithTag("Tiles").transform, false);
        newTile.name = "SquareTile" + i;
        i++;
    }

    public void ConnectToData()
    {
        //TODO: Add tile to dictionary with SquareTileData
    }

    public void RemoveTile()
    {
        i--;
        //TODO: Remove from dictionary
    }
}
