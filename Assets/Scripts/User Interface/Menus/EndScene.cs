using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScene : MonoBehaviour
{
    public TextMeshProUGUI winnerText;

    public int winner = 0;
    // TODO: Get winner

    public void Update()
    {
        winnerText.text = "Congratulations! Winner" + winner + "Wins!";
    }
}
