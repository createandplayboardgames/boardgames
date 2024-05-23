using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;

public class EndManager : MonoBehaviour
{
    void Update()
    {
        GameObject.Find("WinnerText").GetComponent<TextMeshProUGUI>().text = $"Congratulations {EndScene.text}!\n You have won the Game!";
    }
}