using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;



public class SaveAndLoadHandler : MonoBehaviour
{
    private GameDefinitionManager gameDefinitionManager;
    private string SavePath;

    private void Start()
    {
        gameDefinitionManager = GameObject.Find("GameDefinitionManager").GetComponent<GameDefinitionManager>();
        SavePath = Path.Combine(Application.persistentDataPath, "my_boardgame.save");
    }

    public bool SaveGame()
    {
        // validate game data 
        foreach (PlayerData player in gameDefinitionManager.cache.players)
            if (player.location == null) return false;
        foreach (ActionData action in gameDefinitionManager.cache.actions)
            if (action.location == null) return false;

        // save it!
        GameState state = CreateSaveData(); 
        string json = JsonUtility.ToJson(state);
        try { 
            File.WriteAllText(SavePath, json); 
            return true; 
        }
        catch (Exception e)  {  
            return false;
        }
    }

    public bool LoadGame()
    {
        // validate the save data
        if (!File.Exists(SavePath))
        {
            Debug.LogError(SavePath + " doesn't exist!");
            return false;
        }
        // load it!
        string json = File.ReadAllText(SavePath);
        GameState state = JsonUtility.FromJson<GameState>(json);
        ApplySaveData(state);
        return true;
    }

    // ---------------------------------------------------------------
    // Helper Functions
    // ---------------------------------------------------------------
    // Save
    private GameState CreateSaveData(){
        List<TileState> tiles = new List<TileState>();
        foreach (TileData tile in gameDefinitionManager.cache.tiles) 
        {
            tiles.Add(new TileState(
                tile.ID.ToString(), tile.gameObject.transform.position, GetFormattedSpritePath(tile, "Tiles/")
            ));
        }
        List<PlayerState> players = new List<PlayerState>();
        foreach (PlayerData player in gameDefinitionManager.cache.players) 
        {
            players.Add(new PlayerState(
                player.ID.ToString(), player.gameObject.transform.position, GetFormattedSpritePath(player, "Players/"),
                player.location == null ? "" : player.location.ID.ToString(),
                player.playerName, player.points
            ));
        }
        List<FinishGameActionState> finishGameActions = new List<FinishGameActionState>();
        foreach (ActionData action in gameDefinitionManager.cache.actions) 
        {
            if (action is FinishGameActionData){
                finishGameActions.Add(new FinishGameActionState(
                    action.ID.ToString(), action.gameObject.transform.position, GetFormattedSpritePath(action, "Actions/"),
                    action.location == null ? "" : action.location.ID.ToString(),
                    ((FinishGameActionData) action).winner.ID.ToString()
                ));                
            }
        }
        return new GameState(tiles, players, finishGameActions);
    }

    // Load
    private void ApplySaveData(GameState state)
    {
        foreach (TileState tileState in state.tiles) //Tiles MUST be re-created first
        {
            var tileData = gameDefinitionManager.CreateTile();
            ApplyTileStateToTileData(tileData, tileState);
        }
        foreach (PlayerState playerState in state.players)
        {
            var playerData = gameDefinitionManager.CreatePlayer();
            ApplyPlayerStateToPlayerData(playerData, playerState);
        }
        foreach (FinishGameActionState finishGameActionState in state.finishGameActions)
        {
            var finishGameActionData = gameDefinitionManager.CreateFinishGameAction();
            ApplyNonTileStateToNonTileData(finishGameActionData, finishGameActionState);
        }
    }
    private void ApplyTileStateToTileData(TileData tile, TileState state){
        ApplyStateToGameItem(tile, state);
    }
    private void ApplyPlayerStateToPlayerData(PlayerData player, PlayerState state)
    {
        ApplyStateToGameItem(player, state);
        ApplyNonTileStateToNonTileData(player, state);
        player.playerName = state.playerName;
        player.points = state.points;
    }
    private void ApplyNonTileStateToNonTileData(NonTileData item, NonTileState state){
        ApplyStateToGameItem(item, state);
        item.location = GetTileFromID(state.tileID);
    }
    private void ApplyStateToGameItem(GameItemData item, GameItemState state)
    {
        item.ID = Guid.Parse(state.ID);
        item.gameObject.transform.position = state.worldLocation;
        item.gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(state.spritePath);  
        //disable some scripts
        var dragdrop = item.gameObject.GetComponent<DragDrop>();   
        var select = item.gameObject.GetComponent<Select>();   
        if (dragdrop) dragdrop.enabled = false;
        if (select) select.enabled = false;
    }



    // ---------------------------------------------------------------
    // Miscellaenous
    // ---------------------------------------------------------------
    public TileData GetTileFromID(string targetID){
        foreach (TileData tile in gameDefinitionManager.cache.tiles)
            if (tile.ID == Guid.Parse(targetID)) return tile;
        return null;
    }
    public string GetFormattedSpritePath(GameItemData item, string subpath){
        string path = "Assets/Resources/images/" + subpath + item.GetComponent<SpriteRenderer>().sprite.ToString();
        Debug.Log(path.ToString().Replace(" (UnityEngine.Sprite)", ""));
        return path.ToString().Replace("Assets/Resources/", "").Replace(" (UnityEngine.Sprite)", "");

        //string absolutePath = AssetDatabase.GetAssetPath(item.GetComponent<SpriteRenderer>().sprite);
        //var absolutePath = Resources.Load("images/" + subpath + item.GetComponent<SpriteRenderer>().sprite);
        //return absolutePath.ToString().Replace("Assets/Resources/", "").Replace(".png", ""); //loading sprites requires assets to be in this form

    }

}
