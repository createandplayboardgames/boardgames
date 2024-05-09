using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GameSaveManager : MonoBehaviour
{
    public static GameSaveManager Instance;
    private GameDefinitionManager gameDefinitionManager;

    private static string savePath = Application.persistentDataPath + "/gameSave.save";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        gameDefinitionManager = FindObjectOfType<GameDefinitionManager>();
    }

    public void SaveGame()
    {
        if (gameDefinitionManager == null)
        {
            Debug.LogError("GameDefinitionManager not found!");
            return;
        }

        SaveData data = new SaveData
        {
            players = gameDefinitionManager.cache.players,
            tiles = gameDefinitionManager.cache.tiles,
            actions = gameDefinitionManager.cache.actions
        };

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(savePath, FileMode.Create);
        formatter.Serialize(stream, data);
        stream.Close();
        Debug.Log("Game saved successfully to " + savePath);
    }

    public void LoadGame()
    {
        if (!File.Exists(savePath))
        {
            Debug.LogError("Save file not found in " + savePath);
            return;
        }

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(savePath, FileMode.Open);
        SaveData data = formatter.Deserialize(stream) as SaveData;
        stream.Close();

        if (data == null)
        {
            Debug.LogError("Failed to load game data.");
            return;
        }

        // Clear existing game state
        gameDefinitionManager.ClearGameState();

        // Apply the loaded data
        ApplyLoadedData(data);
        Debug.Log("Game loaded successfully from " + savePath);
    }

    private void ApplyLoadedData(SaveData data)
    {
        foreach (var playerData in data.players)
        {
            var player = gameDefinitionManager.CreatePlayer(); // Does CreatePlayer set the player's state?
            player.SetData(playerData);
        }

        foreach (var tileData in data.tiles)
        {
            var tile = gameDefinitionManager.CreateTile();
            tile.SetData(tileData);
        }

        foreach (var actionData in data.actions)
        {
            // Depending on the type of action, call the respective method to recreate the action
            CreateActionFromData(actionData);
        }
    }

    private void CreateActionFromData(ActionData actionData)
    {
        // Need to expand this for other actions
        if (actionData is FinishGameActionData finishActionData)
        {
            gameDefinitionManager.CreateFinishGameAction();
        }
    }
}
