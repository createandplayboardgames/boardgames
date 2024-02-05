using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    /* get number of players, spiner, and position on map.
     * TODO: link to data from set classes
     */
    private static GameObject player1, player2;

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


    /*
     * Update players turn, and activate game over scene.
     * TODO: activate game over scene and player turns, also make text to say whos turn it is.
     */
    void Update()
    {
        if (player1.GetComponent<Movement>().tileIndex > player1position + spinner)
        {
            player1.GetComponent<Movement>().moveAllowed = false;
            player1position = player1.GetComponent<Movement>().tileIndex - 1;
        }
        if (player2.GetComponent<Movement>().tileIndex > player2position + spinner)
        {
            player2.GetComponent<Movement>().moveAllowed = false;
            player2position = player2.GetComponent<Movement>().tileIndex - 1;
        }
        if (player1.GetComponent<Movement>().tileIndex == player1.GetComponent<Movement>().tiles.Length)
        {
            gameOver = true;
        }
        if (player2.GetComponent<Movement>().tileIndex == player2.GetComponent<Movement>().tiles.Length)
        {
            gameOver = true;
        }

    }

    /*
     * If players turn, move action
     */
    public static void MovePlayer(int playerturn)
    {
        switch (playerturn)
        {
            case 1:
                player1.GetComponent<Movement>().moveAllowed = true;
                break;
            case 2:
                player2.GetComponent<Movement>().moveAllowed = true;
                break;

        }
    }
}
