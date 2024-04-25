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
        * TODO: link to data from set classes
    */
    private static GameObject player1, player2;
    private static GameObject activePlayer;
    private int playerTurn = 1;
    public TextMeshProUGUI turnText;
    public static int spinner = 0;
    public static int player1position = 0;
    public static int player2position = 0;
    public static bool gameOver = false;

    bool mouseOver;
    Ray ray;
    RaycastHit2D hit;
    GameObject currentHit;

    void Start()
    {
        player1 = GameObject.Find("Player1");
        player2 = GameObject.Find("Player2");

        player1.GetComponent<Movement>().moveAllowed = false;
        player2.GetComponent<Movement>().moveAllowed = false;
    }

    /*
     * Update players turn, and activate game over scene.
     * 
     * TODO: activate game over scene and player turns, also make text to say whos turn it is.
     */
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        MouseHoverChecker(ray, hit);

        if(Input.GetMouseButtonDown(0))
        {
            MouseClickChecker(ray, hit);
        }

        if (player1.GetComponent<Movement>().tileData.isEndingTile)
        {
            gameOver = true;
            Debug.Log("End Game");
            //TODO: Assign Winner
            SceneManager.LoadScene("EndGame");
        }
        if (player2.GetComponent<Movement>().tileData.isEndingTile)
        {
            gameOver = true;
            Debug.Log("End Game");
            //TODO: Assign Winner
            SceneManager.LoadScene("EndGame");
        }

    }

    //Start Turn for active player.
    public void StartTurn(int playerturn)
    {
        switch (playerturn)
        {
            case 1:
                turnText.text = "Player 1: Your Turn";
                activePlayer = GameObject.Find("Player1");
                playerTurn = 2;
                break;
            case 2:
                turnText.text = "Player 2: Your Turn";
                activePlayer = GameObject.Find("Player2");
                playerTurn = 1;
                break;

        }
    }

    //End Turn for active player, switch player and start turn for new player.
    public void EndTurn()
    {
        //activePlayer.GetComponent<Movement>().moveAllowed = false;
        if (playerTurn == 1)
        {
            StartTurn(1);
        }
        else if (playerTurn == 2)
        {
            StartTurn(2);
        }
    }

    //TODO: Highlight tiles while mouse hovers over
    public void MouseHoverChecker(Ray ray, RaycastHit2D hit)
    {
        hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        if (hit.collider == null)
        {
            Debug.Log("nothing hit");

        }
        else
        {
            
            //Make Separate Function for color
            if (hit.collider.CompareTag("Tiles"))
            {
                
                print(hit.collider.name);
                currentHit = hit.collider.gameObject;
                print(currentHit.name);
                //highlight tile
                //currentHit.GetComponent<SpriteRenderer>().color = new Color(0.5f, 1.5f, 1.5f, 0.5f);

            }
        }
        
    }

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
                if (player1.GetComponent<Movement>().pathOptions.Contains(hit.collider.GetComponent<TileData>()) && player1.GetComponent<Movement>().moveAllowed)
                {
                    Debug.Log("hit!");
                    currentHit = hit.collider.gameObject;
                    ClearColorDirection();
                    player1.GetComponent<Movement>().Move(currentHit);
                    player1.GetComponent<Movement>().moveAllowed = false;
                }
                else if (player2.GetComponent<Movement>().pathOptions.Contains(hit.collider.GetComponent<TileData>()) && player2.GetComponent<Movement>().moveAllowed)
                {
                    Debug.Log("hit!");
                    currentHit = hit.collider.gameObject;
                    ClearColorDirection();
                    player2.GetComponent<Movement>().Move(currentHit);
                    player2.GetComponent<Movement>().moveAllowed = false;
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
    public static void ClearColorDirection()
    {
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tiles");
        foreach (GameObject tile in tiles)
        {
            tile.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1.5f);
        }
    }

    /*
     * If players turn, move action and highlight move
     */
    public static void PlayerTurn(int playerturn)
    {
        switch (playerturn)
        {
            case 1:
                
                player1.GetComponent<Movement>().moveAllowed = true;
                player1.GetComponent<Movement>().pathOptions.Clear();
                GetMovementOptions(spinner, player1.GetComponent<Movement>().tileData, player1);
                colorDirection(player1);
                break;
            case 2:
                player2.GetComponent<Movement>().moveAllowed = true;
                player2.GetComponent<Movement>().pathOptions.Clear();
                GetMovementOptions(spinner, player2.GetComponent<Movement>().tileData, player2);
                colorDirection(player2);
                break;

        }
    }
}
