using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public Vector3 playerPosition;

    // Lists to hold serialized data for players and tiles
    public List<SerializablePlayerData> playersData;
    public List<TileInfo> tilesInfo;

    // Constructor
    public SaveData(List<SerializablePlayerData> players, List<TileInfo> tiles)
    {
        playersData = players;
        tilesInfo = tiles;
    }
}

[System.Serializable]
public class SerializablePlayerData
{
    public Vector3 position;
    public string playerName;
    public string spritePath;  // Path to the sprite asset, for serialization

    public SerializablePlayerData(Vector3 position, string playerName, string spritePath)
    {
        this.position = position;
        this.playerName = playerName;
        this.spritePath = spritePath;
    }

    // Method to set player data from saved state, adding sprite handling
    public void SetData(SerializablePlayerData data)
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
    //Location of the tile
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
