using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;

public class EndManager : MonoBehaviour
{
    public TextMeshProUGUI winnerText;

    void Update()
    {
        winnerText.text = $"Congratulations {EndScene.text}! You have won the Game!";
    }
}
