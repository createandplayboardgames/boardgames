using System.Collections.Generic;

public class GameDefinitionCache  
{
    public List<PlayerData> players = new();
    public List<TileData> tiles = new(); 
    public List<ActionData> actions = new(); 
    public int numberOfItemsLoaded = 0;

    // ====== Helper Methods
    public List<PlayerData> GetPlayersOnTile(TileData tile){
        List<PlayerData> playersOnTile = new();
        foreach (PlayerData player in players)
            if (player.location == tile) { playersOnTile.Add(player); }
        return playersOnTile;
    }
    public List<ActionData> GetActionsOnTile(TileData tile){
        List<ActionData> actionsOnTile = new();
        foreach (ActionData action in actions)
            if (action.location == tile) { actionsOnTile.Add(action); }
        return actionsOnTile;
    }


}