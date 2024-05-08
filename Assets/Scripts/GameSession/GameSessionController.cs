using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UI;


/*
 * Coordinates "Play Game" Sessions
 */
public class GameSessionController : MonoBehaviour
{
    LayoutHelper layout;

    GameDefinitionManager manager;
    /* get number of players, spiner, and position on map.
     * 
     * TODO: link to data from set classes
    */
    
    public List<PlayerData> players = new();
    public List<TileData> tiles = new();
    int playerIndex = 0;

    //TODO: Get Playercache (number of players)

    private static GameObject activePlayer;
    public TextMeshProUGUI turnText;
    public static int spinner = 0;
    public static bool gameOver = false;

    bool mouseOver;
    Ray ray;
    RaycastHit2D hit;
    GameObject currentHit;

    void Start()
    {
        manager = GameObject.Find("GameDefinitionManager").GetComponent<GameDefinitionManager>();
        layout = GameObject.Find("LayoutHelper").GetComponent<LayoutHelper>();
        manager.cache.players = players;
        manager.cache.tiles = tiles;

        foreach(PlayerData player in manager.cache.players)
        {
            layout.SnapPlayerToTile(player, player.gameObject.GetComponent<Movement>().tileData);
        }

        if (manager.cache.players.Count > 0)
        {
            Debug.Log(manager.cache.players.Count);
            StartTurn(playerIndex);
        }

    }

    /*
     * Update players turn, and activate game over scene.
     * 
     * TODO: activate game over scene and player turns, also make text to say whos turn it is.
     */
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //MouseHoverChecker(ray, hit);

        if(Input.GetMouseButtonDown(0))
        {
            MouseClickChecker(ray, hit);
        }

        if (activePlayer.GetComponent<Movement>().tileData.isEndingTile)
        {
            gameOver = true;
            Debug.Log("End Game");
            //TODO: Assign Winner
            SceneManager.LoadScene("EndGame");
        }

    }

    /*
     * Start Turn for active player.
     */
    public void StartTurn(int playerIndex)
    {
        switch (playerIndex)
        {
            case 0:
                turnText.text = "Player 1: Your Turn";
                break;
            case 1:
                turnText.text = "Player 2: Your Turn";
                break;
            case 2:
                turnText.text = "Player 3: Your Turn";
                break;
            case 3:
                turnText.text = "Player 4: Your Turn";
                break;
        }
        Spinner.coroutineAllowed = true;
        activePlayer = manager.cache.players[playerIndex].gameObject;
        activePlayer.GetComponent<Movement>().moveAllowed = true;
    }

    /*
     * End Turn for active player, switch player and start turn for new player.
     */
    public void EndTurn()
    {
        playerIndex++;
        if (manager.cache.players.Count <= playerIndex)
        {
            playerIndex = 0;
        }
        Debug.Log(playerIndex);
        StartTurn(playerIndex);

    }

    /*
    * If players turn, move action and highlight move int player turn
    */
    public static void PlayerTurn()
    {
        activePlayer.GetComponent<Movement>().pathOptions.Clear();
        GetMovementOptions(spinner, activePlayer.GetComponent<Movement>().tileData, activePlayer);
        colorDirection(activePlayer);
    }

    /*
     * Check player click is over tile option then move
     */
    public void MouseClickChecker(Ray ray, RaycastHit2D hit)
    {
        hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        if (hit.collider == null)
        {
            Debug.Log("nothing clicked");

        }
        else
        {
            //Make Separate Function for color
            if (hit.collider.CompareTag("Tiles"))
            {

                print(hit.collider.name);
                if (activePlayer.GetComponent<Movement>().pathOptions.Contains(hit.collider.GetComponent<TileData>()) && activePlayer.GetComponent<Movement>().moveAllowed)
                {
                    Debug.Log("hit!");
                    currentHit = hit.collider.gameObject;
                    ClearColorDirection();
                    activePlayer.GetComponent<Movement>().updateCurrentTile(currentHit);
                    layout.SnapPlayerToTile(activePlayer.GetComponent<PlayerData>(), currentHit.GetComponent<TileData>());
                    activePlayer.GetComponent<Movement>().moveAllowed = false;
                }

            }
        }
    }

    /*
     * Recursive get list of available moves for active player.
     */
    public static void GetMovementOptions(int roll, TileData tile, GameObject player)
    {
        List<TileData> nextTiles = new List<TileData>();
        nextTiles = tile.gameObject.GetComponent<TileData>().GetAllIncomingConnections();

        if (roll <= 0)
        {
            return;

        }
        for (int i = 0; i < nextTiles.Count; i++)
        {
            if (!player.GetComponent<Movement>().pathOptions.Contains(nextTiles[i]))
            {
                player.GetComponent<Movement>().pathOptions.Add(nextTiles[i]);
            }
            roll -= 1;
            GetMovementOptions(roll, nextTiles[i], player);
        }
    }

    /*
     * Highlight available moves for active player
     */
    public static void colorDirection(GameObject player)
    {
        for (int i = 0; i < player.GetComponent<Movement>().pathOptions.Count; i++)
        {
            Debug.Log(player.GetComponent<Movement>().pathOptions[i]);
            GameObject tileObject = player.GetComponent<Movement>().pathOptions[i].gameObject;
            tileObject.GetComponent<SpriteRenderer>().color = new Color(0.5f, 1.5f, 1.5f, 0.5f);
        }

    }

    /*
     * Clear highlighted tiles
     */
    public void ClearColorDirection()
    {
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tiles");
        foreach (GameObject tile in tiles)
        {
            tile.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1.5f);
        }
    }

}
