using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScene : MonoBehaviour
{
    //FinishGameActionData endGame;

    public TextMeshProUGUI winnerText;

    // Change to PlayerData eventually
    //public PlayerData winner;

    public void Start()
    {
        // change to FindObject().GetComponent once reorganized
        //endGame = gameObject.GetComponent<FinishGameActionData>();
    }

    public void Update()
    {
        //if(endGame.winner != null)
        //{
        //    winnerText.text = "Congratulations! " + endGame.winner + " has won the game!";
        //}
        //else { winnerText.text = "Congratulations Winner!"; }
        winnerText.text = "Congratulations Winner!";
    }
}
