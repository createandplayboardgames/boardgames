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

    MovementControl movementControl;

    Spinner spinner;
    /* get number of players, spiner, and position on map.
     * 
     * TODO: link to data from set classes
    */
    
    public List<PlayerData> players = new();
    public List<TileData> tiles = new();
    int playerIndex = 0;

    //TODO: Get Playercache (number of players)

    private PlayerData currentPlayer;

    public TextMeshProUGUI turnText;
    public static bool gameOver = false;

    Ray ray;
    RaycastHit2D hit;
    GameObject currentHit;

    void Start()
    {
        manager = GameObject.Find("GameDefinitionManager").GetComponent<GameDefinitionManager>();
        layout = GameObject.Find("LayoutHelper").GetComponent<LayoutHelper>();
        movementControl = GameObject.Find("MovementControl").GetComponent<MovementControl>();
        spinner = GameObject.Find("Spinner").GetComponent<Spinner>();

        manager.cache.players = players;
        manager.cache.tiles = tiles;

        foreach(PlayerData player in manager.cache.players)
        {
            layout.SnapPlayerToTile(player, player.location);
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
            //movementControl.MouseClickChecker(ray, hit);
            MouseClickChecker(ray, hit);
        }

        if (currentPlayer.location.isEndingTile)
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
        currentPlayer = manager.cache.players[playerIndex];
        movementControl.currentPlayer = currentPlayer;
        movementControl.moveAllowed = true;
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

    public void MouseClickChecker(Ray ray, RaycastHit2D hit)
    {
        hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        if (hit.collider == null)
        {
            Debug.Log("nothing clicked");
        }
        else
        {
            if(hit.collider.CompareTag("Spinner"))
            {
                PlayerTurn();
            }
            if (hit.collider.CompareTag("Tiles"))
            {
                movementControl.Move(ray, hit);
                //if (pathOptions.Contains(hit.collider.GetComponent<TileData>()) && moveAllowed)
                //{
                //    Debug.Log("hit!");
                //    currentHit = hit.collider.gameObject;
                //    ClearColorDirection();
                //    currentPlayer.location = currentHit.gameObject.GetComponent<TileData>();
                //    layout.SnapPlayerToTile(currentPlayer, currentPlayer.location);
                //    moveAllowed = false;
                //}
            }
        }
    }

    /*
    * If players turn, move action and highlight move int player turn
    */
    public void PlayerTurn()
    {
        movementControl.pathOptions.Clear();
        movementControl.GetMovementOptions(spinner.finalState, currentPlayer.location);
        movementControl.ColorDirection();
    }
}
