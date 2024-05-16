using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Search;
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
    public List<ActionData> actions = new();

    int playerIndex = 0;

    //TODO: Get Playercache (number of players)

    private PlayerData currentPlayer;
    public TextMeshProUGUI turnText;

    public static bool gameOver = false;

    Ray ray;
    RaycastHit2D hit;

    void Start()
    {
        manager = GameObject.Find("GameDefinitionManager").GetComponent<GameDefinitionManager>();
        layout = GameObject.Find("LayoutHelper").GetComponent<LayoutHelper>();
        movementControl = GameObject.Find("MovementControl").GetComponent<MovementControl>();
        spinner = GameObject.Find("Spinner").GetComponent<Spinner>();

        manager.cache.players = players;
        manager.cache.tiles = tiles;
        manager.cache.actions = actions;

        if (manager.cache.players.Count > 0)
        {
            Debug.Log(manager.cache.players.Count);
            StartGame();
        }
    }

    /*
     * Check Mouse Input
     */
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Input.GetMouseButtonDown(0))
        {
            MouseClickChecker(ray, hit);
        }

    }

    /*
    * Start Game Sequence.
    */
    public void StartGame()
    {
        foreach (PlayerData player in manager.cache.players)
        {
            layout.SnapPlayerToTile(player, player.location);
        }
        StartTurn(playerIndex);
    }

    /*
     * Start Turn for active player.
     */
    public void StartTurn(int playerIndex)
    {
        currentPlayer = manager.cache.players[playerIndex];
        turnText.text = $"{currentPlayer.name}: Your Turn. Click Dice to Roll.";
        Spinner.spinAllowed = true;
        movementControl.currentPlayer = currentPlayer;
    }

    /*
     * End Turn for active player, start turn for new player.
     */
    public void EndTurn()
    {
        if (currentPlayer.location.isEndingTile)
        {
            EndGame();
        }

        //Next Player turn
        playerIndex++;
        if (manager.cache.players.Count <= playerIndex)
        {
            playerIndex = 0;
        }
        StartTurn(playerIndex);

    }

    /*
     * End Game Sequence.
     */
    public void EndGame()
    {
        gameOver = true;
        Debug.Log("End Game");
        EndScene.AssignWinner(currentPlayer);
        Debug.Log(EndScene.winner);
        SceneManager.LoadScene("EndGame");
    }

    /*
     * Check Ray hit.
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
            // Wait for roll to finish
            if(hit.collider.CompareTag("Spinner"))
            {
                StartCoroutine(nameof(WaitForRoll));
            }
            // Move if allowed
            if (hit.collider.CompareTag("Tiles"))
            {
                movementControl.Move(ray, hit);
            }
        }
    }

    /*
     * Wait for roll to finish before Movement.
     */
    public IEnumerator WaitForRoll()
    {
        while(!spinner.finished)
        {
            yield return new WaitForSeconds(0.1f);
        }
        HandlePlayerMovement();
    }

    /*
    * Move action and highlight move int player turn.
    */
    public void HandlePlayerMovement()
    {
        turnText.text = $"{currentPlayer.name}: Your Turn. Click Tile to Move.";
        movementControl.pathOptions.Clear();
        movementControl.GetMovementOptions(spinner.finalState, currentPlayer.location);
        movementControl.ColorDirection();
    }
}
