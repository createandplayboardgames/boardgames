using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public static class EndScene
{
    public static PlayerData winner;
    public static string text;

    public static void AssignWinner(PlayerData player)
    {
        winner = player;
        text = player.name;

    }
    //FinishGameActionData endGame;

    //public static TextMeshProUGUI winnerText;

    // Change to PlayerData eventually
    //public PlayerData winner;

    //public static void Start()
    //{
    //    // change to FindObject().GetComponent once reorganized
    //    //endGame = gameObject.GetComponent<FinishGameActionData>();
    //}

    //public static void Update()
    //{
    //    //if(endGame.winner != null)
    //    //{
    //    //    winnerText.text = "Congratulations! " + endGame.winner + " has won the game!";
    //    //}
    //    //else { winnerText.text = "Congratulations Winner!"; }
    //    winnerText.text = $"Congratulations {winner}! You have won the Game!";
    //}
}