using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameState
{
    public List<TileState>   tiles;
    public List<PlayerState> players;
    public List<FinishGameActionState> finishGameActions;
    public GameState(List<TileState> tiles, List<PlayerState> players, List<FinishGameActionState> finishGameActions){
        this.tiles   = tiles;
        this.players = players;
        this.finishGameActions = finishGameActions;
    }
}

[System.Serializable]
public class GameItemState {
    public string ID;
    public Vector3 worldLocation;
    public string spritePath;  
}
[System.Serializable]
public class NonTileState : GameItemState {
    public string tileID;
}



[System.Serializable]
public class TileState : GameItemState
{
    public TileState(string ID, Vector3 worldLocation, string spritePath)
    {
        this.ID = ID;
        this.worldLocation = worldLocation;
        this.spritePath = spritePath;
  
    }
}
[System.Serializable]
public class PlayerState : NonTileState
{
    public string playerName;
    public int points;
    public PlayerState(
        string ID, Vector3 worldLocation, string spritePath, 
        string tileID, 
        string playerName, int points)
    {
        this.ID = ID;
        this.worldLocation = worldLocation;
        this.spritePath = spritePath;
        this.tileID = tileID;
        this.playerName = playerName;
        this.points = points;
    }
}

[System.Serializable]
public class FinishGameActionState : NonTileState 
{
    public string winnerID;
    public FinishGameActionState(
        string ID, Vector3 worldLocation, string spritePath, 
        string tileID,
        string winnerID) 
    {
        this.ID = ID;
        this.worldLocation = worldLocation;
        this.spritePath = spritePath;
        this.tileID = tileID;
        this.winnerID = winnerID;
    }
}