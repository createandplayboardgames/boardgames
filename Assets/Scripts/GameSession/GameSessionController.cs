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
using UnityEngine.WSA;


/*
 * Coordinates "Play Game" Sessions
 */
public class GameSessionController : MonoBehaviour
{
    GameDefinitionManager manager;
    PlayerData currentPlayer = null;
    int currentPlayerIndex = 0;

    /*
    private void TaylorsUpdate()
    {
        //find current player
        currentPlayer = manager.cache.players[currentPlayerIndex];

        PerformPerTurnActions();

        //move player
        int spacesToMove = 1; //TODO - spin spinner instead
        while (spacesToMove != 0){
            MovePlayerOneSpace(currentPlayer);
            PerformPerMoveActions();
            spacesToMove--;
        }

        //update current player
        currentPlayerIndex = (currentPlayerIndex + 1) % manager.players.Count;
    }
    */

    
    private void PerformPerMoveActions()
    {
        /*
        // Perform Actions
        var action = currentPlayer.location.GetAssociatedAction();
        if (action is FinishGameAction || currentPlayer.location.shouldFinishGame)
        {
            SceneManager.LoadScene("EndGame");
            return;
        }
        else if (action is MovePlayerToLocationAction)
        {
            MovePlayerToLocationAction a = (MovePlayerToLocationAction) action;
            PlayerData player = ParsePlayerFromAction(a.player); //convert dummy players
            MovePlayerToLocation(player, a.location);
        }
        else if (action is ChangePlayerPointsAction)
        {
            ChangePlayerPointsAction a = (ChangePlayerPointsAction) action;
            PlayerData player = ParsePlayerFromAction(a.player); //convert dummy players
            ChangePlayerPoints(player, 0);
        }
        */
        // TODO - perform action associated with player
    }


    private PlayerData ParsePlayerFromAction(PlayerData player)
    {
        //player might dummy-type
        /*
        if (player is RandomPlayer)
            return manager.players[ (new System.Random()).Next(0, manager.players.Count) ];
        if (player is CurrentPlayer)
            return currentPlayer;
        */
        return player;
    }

    private void PerformPerTurnActions()
    {
        // TODO - nothing yet!
    }







    private void MovePlayerOneSpace(PlayerData player)
    {
        //TODO 
    }
    private void MovePlayerToLocation(PlayerData player, TileData location){
        //TODO - animate player to location
        //TODO - update data classes (?)
    }
    private void ChangePlayerPoints(PlayerData player, int points)
    {
        player.points = points;
        //TODO - animation?
    }









    // --------------------------------------------------------




    /* get number of players, spiner, and position on map.
     * 
     * TODO: link to data from set classes
    */
    
    public List<PlayerData> players = new();
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
        if(players.Count > 0)
        {
            Debug.Log(players.Count);
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

    //Start Turn for active player.
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
        activePlayer = players[playerIndex].gameObject;
        activePlayer.GetComponent<Movement>().moveAllowed = true;
    }

    //End Turn for active player, switch player and start turn for new player.
    public void EndTurn()
    {
        playerIndex++;
        if (players.Count <= playerIndex)
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

    //TODO: Highlight tiles while mouse hovers over
    //public void MouseHoverChecker(Ray ray, RaycastHit2D hit)
    //{
    //    hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
    //    if (hit.collider == null)
    //    {
    //        Debug.Log("nothing hit");

    //    }
    //    else
    //    {
            
    //        //Make Separate Function for color
    //        if (hit.collider.CompareTag("Tiles"))
    //        {
                
    //            print(hit.collider.name);
    //            currentHit = hit.collider.gameObject;
    //            print(currentHit.name);
    //            //highlight tile
    //            //currentHit.GetComponent<SpriteRenderer>().color = new Color(0.5f, 1.5f, 1.5f, 0.5f);

    //        }
    //    }
        
    //}

    //Check player click is over tile option then move
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
                    activePlayer.GetComponent<Movement>().Move(currentHit);
                    activePlayer.GetComponent<Movement>().moveAllowed = false;
                }

                //TODO make validation and movement separate
            }
        }
    }

    //Recursive get list of available moves for active player.
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

                //TODO: Update path travelled to use section below
                //if (!player.GetComponent<Movement>().travelled.Contains(nextTiles[i]))
                //{
                //    if (!player.GetComponent<Movement>().pathOptions.Contains(nextTiles[i]))
                //    {
                //        player.GetComponent<Movement>().pathOptions.Add(nextTiles[i]);
                //    }
                //    roll -= 1;
                //    GetMovementOptions(roll, nextTiles[i], player);
                //}
        }
    }

    // Highlight available moves for active player
    public static void colorDirection(GameObject player)
    {
        for (int i = 0; i < player.GetComponent<Movement>().pathOptions.Count; i++)
        {
            Debug.Log(player.GetComponent<Movement>().pathOptions[i]);
            GameObject tileObject = player.GetComponent<Movement>().pathOptions[i].gameObject;
            tileObject.GetComponent<SpriteRenderer>().color = new Color(0.5f, 1.5f, 1.5f, 0.5f);
        }

    }

    // Clear highlighted tiles
    public void ClearColorDirection()
    {
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tiles");
        foreach (GameObject tile in tiles)
        {
            tile.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1.5f);
        }
    }

}
