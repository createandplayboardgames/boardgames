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
    GameDefinitionManager gameDefinitionManager;
    LayoutHelper layoutHelper;
    MovementControl movementControl;
    Spinner spinner;
    
    //TODO: Get Playercache (number of players)
    int playerIndex = 0;
    private PlayerData currentPlayer;
    public TextMeshProUGUI turnText;
    Ray ray;
    RaycastHit2D hit;

    void Start()
    {
        SaveAndLoadHandler loader = GameObject.Find("SaveAndLoadHandler").GetComponent<SaveAndLoadHandler>();
        if (!loader.LoadGame()){
            //TODO - show error text
        }

        gameDefinitionManager = GameObject.Find("GameDefinitionManager").GetComponent<GameDefinitionManager>();
        layoutHelper = GameObject.Find("LayoutHelper").GetComponent<LayoutHelper>();
        movementControl = GameObject.Find("MovementControl").GetComponent<MovementControl>();
        spinner = GameObject.Find("Spinner").GetComponent<Spinner>();
        turnText = GameObject.Find("TurnText").GetComponent<TextMeshProUGUI>();

        if (gameDefinitionManager.cache.players.Count > 0)
        {
            Debug.Log(gameDefinitionManager.cache.players.Count);
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
            MouseClickHandler(ray, hit);
    }

    /*
    * Start Game Sequence.
    */
    public void StartGame()
    {
        StartTurn(playerIndex);
    }

    /*
     * Start Turn for active player.
     */
    public void StartTurn(int playerIndex)
    {
        currentPlayer = gameDefinitionManager.cache.players[playerIndex];
        turnText.text = $"{currentPlayer.playerName}: Your Turn.";
        Spinner.spinAllowed = true;
        movementControl.currentPlayer = currentPlayer;
    }

    /*
     * End Turn for active player, start turn for new player.
     */
    public void EndTurn()
    {
        PerformActions();
        //Next Player turn
        playerIndex = (playerIndex + 1) % gameDefinitionManager.cache.players.Count;
        StartTurn(playerIndex);
    }

    /*
     * End Game Sequence.
     */
    public void EndGame()
    {
        EndScene.AssignWinner(currentPlayer);
        SceneManager.LoadScene("EndGame");
    }

    /*
     * Check Ray hit.
     */
    public void MouseClickHandler(Ray ray, RaycastHit2D hit)
    {
        hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        Debug.Log("!!!!!!!!!!!!!MouseClickHandler");
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
        movementControl.pathOptions.Clear();
        movementControl.GetMovementOptions(spinner.finalState, currentPlayer.location);
        movementControl.ColorDirection();
    }

    /*
     * Perform actions for player.
     */
    public void PerformActions()
    {
        var actionsOnTile = gameDefinitionManager.cache.GetActionsOnTile(currentPlayer.location);
        if (actionsOnTile.Count == 0)
            return;
        ActionData actionToPerform = actionsOnTile[0];
        if (actionToPerform is FinishGameActionData)
        {
            EndGame();
        }
        else if (actionToPerform is ChangePointsActionData)
        {
            // Perform changing player points.
            // TODO             
        }
    }

}