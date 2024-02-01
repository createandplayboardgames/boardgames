using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public String playerName;
    public int points = 0;
    public SquareTileData location;
    public PlayerData(String playerName)
    {
        this.playerName = playerName;
    }
}
