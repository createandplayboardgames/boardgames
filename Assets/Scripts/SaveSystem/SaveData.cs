using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    // Position of the player in the game world
    public SerializableVector3 playerPosition;
    
    // Information about each tile in the game
    public List<TileInfo> tilesInfo = new List<TileInfo>();
    
    // Simplified actions taken or to be taken in the game
    public List<ActionInfo> actionsInfo = new List<ActionInfo>();

    // Constructor for creating a SaveData object
    public SaveData(Vector3 playerPos, List<TileInfo> tiles, List<ActionInfo> actions)
    {
        playerPosition = playerPos;
        tilesInfo = tiles;
        actionsInfo = actions;
    }
}

[System.Serializable]
public class PlayerData
{
    public Vector3 position;
    public string playerName;
    public string spritePath;  // Path to the sprite asset, for serialization

    public PlayerData(Vector3 position, string playerName, string spritePath)
    {
        this.position = position;
        this.playerName = playerName;
        this.spritePath = spritePath;
    }

    // Method to set player data from saved state, adding sprite handling
    public void SetData(PlayerData data)
    {
        this.position = data.position;
        this.playerName = data.playerName;
        this.spritePath = data.spritePath;
        // Does this save/load the sprite?
    }
}

[System.Serializable]
public class TileInfo
{
    // Location of the tile
    public Vector3 tileLocation;
    // Directional flags indicating outgoing paths from this tile
    public bool leftOutgoing, rightOutgoing, upOutgoing, downOutgoing;
    // Flag indicating if this tile is an ending point
    public bool isEndingTile;

    // Constructor for creating a TileInfo object
    public TileInfo(Vector3 location, bool left, bool right, bool up, bool down, bool isEnd)
    {
        tileLocation = location;
        leftOutgoing = left;
        rightOutgoing = right;
        upOutgoing = up;
        downOutgoing = down;
        isEndingTile = isEnd;
    }
}

[System.Serializable]
public class ActionInfo
{
    // Type of action (e.g., "MovePlayer", "ChangePoints")
    public string actionType;
    // Identifier for the player this action applies to
    public int playerID;
    // Location related to the action, if applicable
    public Vector3 actionLocation;
    // Points change related to the action, if applicable
    public int pointsChange;

    // Constructor for creating an ActionInfo object
    public ActionInfo(string type, int playerID, Vector3 location, int points)
    {
        actionType = type;
        this.playerID = playerID;
        actionLocation = location;
        pointsChange = points;
    }
}
