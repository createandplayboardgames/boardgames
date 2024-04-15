using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UI;


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
    public TextMeshProUGUI turnText;
    public static int spinner = 0;
    public static int player1position = 0;
    public static int player2position = 0;
    public static bool gameOver = false;

    void Start()
    {
        player1 = GameObject.Find("Player1");
        player2 = GameObject.Find("Player2");

        player1.GetComponent<Movement>().moveAllowed = false;
        player2.GetComponent<Movement>().moveAllowed = false;

    }

    public static void GetMovementOptions(int roll, TileData tile, GameObject player)
    {
        List<TileData>nextTiles = new List<TileData>();
        nextTiles = tile.gameObject.GetComponent<TileData>().GetAllIncomingConnections();
        //foreach (var item in nextTiles)
        //{
           // Debug.Log(item.ToString());
       // }
        if (roll <= 0)
        {
            return;
            //GameObject tileObject = tile.gameObject;
            //tileObject.GetComponent<SpriteRenderer>().color = new Color(0.5f, 1.5f, 1.5f, 0.5f);
        }
        for (int i = 0; i < nextTiles.Count; i++)
        {
            if (!player.GetComponent<Movement>().travelled.Contains(nextTiles[i]))
            {
                if (!player.GetComponent<Movement>().pathOptions.Contains(nextTiles[i]))
                {
                    player.GetComponent<Movement>().pathOptions.Add(nextTiles[i]);
                    //Debug.Log(nextTiles[i]);
                }
                roll -= 1;
                //Debug.Log(nextTiles[i]);
                GetMovementOptions(roll, nextTiles[i], player);
            }
        }


    }

    public void ClearColorDirection(GameObject player)
    {
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tiles");
        foreach (GameObject tile in tiles)
        {
            tile.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1.5f);
        }
    }

    public static void colorDirection(GameObject player)
    {
        for(int i = 0; i < player.GetComponent<Movement>().pathOptions.Count; i++)
        {
            Debug.Log(player.GetComponent<Movement>().pathOptions[i]);
            GameObject tileObject = player.GetComponent<Movement>().pathOptions[i].gameObject;
            tileObject.GetComponent<SpriteRenderer>().color = new Color(0.5f, 1.5f, 1.5f, 0.5f);
        }
        //if (player1.GetComponent<Movement>().tiles.Length >= roll)
        //{
        //    GameObject tileObject = tile[roll].transform.gameObject;
        //    tileObject.GetComponent<SpriteRenderer>().color = new Color(0.5f, 1.5f, 1.5f, 0.5f);
        //}
    }



    /*
     * Update players turn, and activate game over scene.
     * Source: https://www.youtube.com/watch?v=W8ielU8iURI&ab_channel=AlexanderZotov
     * 
     * TODO: activate game over scene and player turns, also make text to say whos turn it is.
     */
    void Update()
    {

        if (player1.GetComponent<Movement>().tileIndex > player1position + spinner)
        {
            player1.GetComponent<Movement>().moveAllowed = false;
            ClearColorDirection(player1);
            player1position = player1.GetComponent<Movement>().tileIndex - 1;
            turnText.text = "Computer's Turn";
        }
        if (player2.GetComponent<Movement>().tileIndex > player2position + spinner)
        {
            player2.GetComponent<Movement>().moveAllowed = false;
            player2position = player2.GetComponent<Movement>().tileIndex - 1;
            turnText.text = "Your Turn";
        }
        if (player1.GetComponent<Movement>().tileIndex == player1.GetComponent<Movement>().tiles.Length)
        {
            gameOver = true;
            Debug.Log("End Game");
            //TODO: Assign Winner
            SceneManager.LoadScene("EndGame");
        }
        if (player2.GetComponent<Movement>().tileIndex == player2.GetComponent<Movement>().tiles.Length)
        {
            gameOver = true;
            Debug.Log("End Game");
            //TODO: Assign Winner
            SceneManager.LoadScene("EndGame");
        }

    }

    /*
     * If players turn, move action and highlight move
     */
    public static void MovePlayer(int playerturn)
    {
        switch (playerturn)
        {
            case 1:
                player1.GetComponent<Movement>().pathOptions.Clear();
                GetMovementOptions(spinner, player1.GetComponent<Movement>().tileData, player1);
                colorDirection(player1);

                player1.GetComponent<Movement>().moveAllowed = true;
                break;
            case 2:
                player2.GetComponent<Movement>().moveAllowed = true;
                break;

        }
    }
}
