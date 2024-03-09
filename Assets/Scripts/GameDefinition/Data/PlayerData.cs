using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public Guid ID = Guid.NewGuid();
    public String playerName = "no name";
    public int points = 0;
    public TileData location;
    public PlayerData() {}
    public PlayerData(String name) { this.playerName = name; }
}

