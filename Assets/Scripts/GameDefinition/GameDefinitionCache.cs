using System.Collections.Generic;

public class GameDefinitionCache  
{
    public List<PlayerData> players = new();
    public List<TileData> tiles = new(); 
    public List<ActionData> actions = new(); 
    public int itemsLoadedCount = 0;

    // ====== Helper Methods
    public List<PlayerData> GetPlayersOnTile(TileData tile){
        List<PlayerData> playersOnTile = new();
        foreach (PlayerData player in players)
            if (player.location == tile) { playersOnTile.Add(player); }
        return playersOnTile;
    }

}