using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using UnityEngine;

public class GameSaveManager : MonoBehaviour
{
    //public static GameSaveManager Instance;
    //private GameDefinitionManager gameDefinitionManager;

    //private static string savePath = Application.persistentDataPath + "/gameSave.save";

    //private void Awake()
    //{
    //    if (Instance == null)
    //    {
    //        Instance = this;
    //        DontDestroyOnLoad(gameObject);
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }

    //    gameDefinitionManager = FindObjectOfType<GameDefinitionManager>();
    //}

    //public void SaveGame()
    //{
    //    if (gameDefinitionManager == null)
    //    {
    //        Debug.LogError("GameDefinitionManager not found!");
    //        return;
    //    }

    //    List<SerializablePlayerData> playerDataList = new List<SerializablePlayerData>();
    //    foreach (PlayerData player in gameDefinitionManager.cache.players) {
    //        Vector3 playerPosition = player.gameObject.transform.position;  // Correctly accessing the position
    //        string spritePath = player.GetComponent<SpriteRenderer>().sprite.name;
    //        SerializablePlayerData serializablePlayer = new SerializablePlayerData(playerPosition, player.playerName, player.spritePath);
    //        playerDataList.Add(serializablePlayer);
    //    }

    //    List<TileInfo> tileInfoList = new List<TileInfo>();
    //    foreach (TileData tile in gameDefinitionManager.cache.tiles) {
    //        Vector3 tilePosition = tile.transform.position;  // Correctly accessing the position
    //        TileInfo tileInfo = new TileInfo(tilePosition, tile.left, tile.right, tile.up, tile.down, tile.isEndingTile);
    //        tileInfoList.Add(tileInfo);
    //    }


    //    SaveData data = new SaveData(playerDataList, tileInfoList);

    //    BinaryFormatter formatter = new BinaryFormatter();
    //    FileStream stream = new FileStream(savePath, FileMode.Create);
    //    formatter.Serialize(stream, data);
    //    stream.Close();
    //    Debug.Log("Game saved successfully to " + savePath);
    //}

    //public void LoadGame()
    //{
    //    if (!File.Exists(savePath))
    //    {
    //        Debug.LogError("Save file not found in " + savePath);
    //        return;
    //    }

    //    BinaryFormatter formatter = new BinaryFormatter();
    //    FileStream stream = new FileStream(savePath, FileMode.Open);
    //    SaveData data = formatter.Deserialize(stream) as SaveData;
    //    stream.Close();

    //    if (data == null)
    //    {
    //        Debug.LogError("Failed to load game data.");
    //        return;
    //    }

    //    // Apply the loaded data
    //    ApplyLoadedData(data);
    //    Debug.Log("Game loaded successfully from " + savePath);
    //}

    //private void ApplyLoadedData(SaveData data)
    //{
    //    foreach (SerializablePlayerData playerData in data.playersData)
    //    {
    //        var player = gameDefinitionManager.CreatePlayer(); // Does CreatePlayer set the player's state?
    //        player.SetData(playerData);
    //    }

    //    foreach (TileInfo tileData in data.tilesInfo)
    //    {
    //        var tile = gameDefinitionManager.CreateTile();
    //        tile.SetData(tileData);
    //    }

    //}

}
