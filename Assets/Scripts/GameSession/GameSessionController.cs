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
    
    int playerIndex = 0;
    private PlayerData currentPlayer;
    public TextMeshProUGUI turnText;
    Ray ray;
    RaycastHit2D hit;

    void Start()
    {
        SaveAndLoadHandler loader = GameObject.Find("SaveAndLoadHandler").GetComponent<SaveAndLoadHandler>();
        if (!loader.LoadGame()){
            return; //TODO - show error text
        }

        gameDefinitionManager = GameObject.Find("GameDefinitionManager").GetComponent<GameDefinitionManager>();
        layoutHelper = GameObject.Find("LayoutHelper").GetComponent<LayoutHelper>();
        movementControl = GameObject.Find("MovementControl").GetComponent<MovementControl>();
        spinner = GameObject.Find("Spinner").GetComponent<Spinner>();
        turnText = GameObject.Find("TurnText").GetComponent<TextMeshProUGUI>();
            
        StartGame();
    }

    void Update()    
    {
        //check mouse input
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Input.GetMouseButtonDown(0)) 
            MouseClickHandler(ray, hit);
    }

    public void MouseClickHandler(Ray ray, RaycastHit2D hit)
    {
        hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        Debug.Log("item clicked: " + hit.collider.name);
        if (hit.collider == null) { Debug.Log("nothing clicked"); }
        else {
            // Wait for roll to finish
            if(hit.collider.CompareTag("Spinner")){
                StartCoroutine(WaitForRoll());      
            }
            // Move if allowed
            if (hit.collider.CompareTag("Tiles")){
                movementControl.Move(ray, hit);      
                EndTurn();
            }
        }
    }

    public void StartGame()
    {
        StartTurn(playerIndex);
    }
    public void StartTurn(int playerIndex)
    {
        // initialize environment for active player's turn.
        currentPlayer = gameDefinitionManager.cache.players[playerIndex];
        movementControl.currentPlayer = currentPlayer;
        Spinner.spinAllowed = true;
        turnText.text = $"{currentPlayer.playerName}: Your Turn.";
    }
    public void EndTurn()
    {
        // finish trun ffor active player, start turn for new player.
        PerformActions();

        playerIndex = (playerIndex + 1) % gameDefinitionManager.cache.players.Count;
        StartTurn(playerIndex);
    }
    public void EndGame()
    {
        EndScene.AssignWinner(currentPlayer);
        SceneManager.LoadScene("EndGame");
    }

    public IEnumerator WaitForRoll()
    {
        while(!spinner.finished) // Wait for roll to finish before Movement.
            yield return new WaitForSeconds(0.1f);
        HandlePlayerMovement();
    }
    public void HandlePlayerMovement()
    {
        // perform move, and highlight movement options
        movementControl.pathOptions.Clear();
        movementControl.GetMovementOptions(spinner.finalState, currentPlayer.location);
        movementControl.ColorDirection();
    }
    public void PerformActions()
    {
        var actionsOnTile = gameDefinitionManager.cache.GetActionsOnTile(currentPlayer.location);
        if (actionsOnTile.Count == 0)
            return;
        ActionData actionToPerform = actionsOnTile[0];
        Debug.Log("to perform " + actionToPerform.gameObject.name);
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