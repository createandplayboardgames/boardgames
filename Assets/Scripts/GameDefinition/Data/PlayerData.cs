using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : NonTileData {
    public String playerName = "placeholder";
    public int points = 0;

    public string spritePath;  // Added for sprite save/load

    // Method to set data from a SerializablePlayerData object
    public void SetData(SerializablePlayerData data)
    {
        this.playerName = data.playerName;
        this.spritePath = data.spritePath;
        this.transform.position = data.position;

        // Load the sprite if spritePath is valid
        Sprite newSprite = Resources.Load<Sprite>(data.spritePath);
        if (newSprite)
        {
            this.GetComponent<SpriteRenderer>().sprite = newSprite;
        }
    }
}

