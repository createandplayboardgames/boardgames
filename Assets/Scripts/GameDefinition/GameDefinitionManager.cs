using System.Collections.Generic;
using UnityEngine;

public class GameDefinitionManager : MonoBehaviour
{
    // TODO - this is data, and should go elsewhere
    public List<PlayerData> players = new List<PlayerData>();
    private int MAX_PLAYER_COUNT = 4;
    public int tilecount = 0;

    private GameObject loadGamePiece(string pieceName, string parentName)
    {
        var parent = GameObject.Find(parentName);
        GameObject gamePiece = Instantiate(Resources.Load(pieceName),
            parent.transform.position, parent.transform.rotation) as GameObject;
        gamePiece.transform.parent = parent.transform;
        gamePiece.GetComponent<SpriteRenderer>().sortingLayerName = parent.GetComponent<SpriteRenderer>().sortingLayerName;
        //TODO - because the camera can pan and zoom, we should instantiate objects in the middle of the camera 
        //ideally without overlapping others
        gamePiece.GetComponent<Select>()?.SelectPiece();
        return gamePiece;
    }

    // --- Players 
    public void CreatePlayer()
    {
        if (players.Count > MAX_PLAYER_COUNT){
            //TODO - display message
            return; 
        }
        GameObject player = loadGamePiece("Player", "PlayerArea");
        players.Add(player.GetComponent<PlayerData>());
    }
    void DeletePlayer(PlayerData player)
    {
        // TODO - delete PlayerData's GameObject, delete from cache
    }
    void MovePlayer(PlayerData player, TileData tile)
    {
        //TODO - move PlayerData's GameObject to TileData's GameObject
    }


    // --- Tiles
    public void CreateTile()
    {
        loadGamePiece("Tile", "TileArea");
        tilecount++;
    }



    public void DeleteTile()
    {
        // TODO - delete TileData's GameObject
        tilecount--;
       
    }

    public void ConnectPorts(ConnectableSide.OutgoingPort outgoing, ConnectableSide.IncomingPort incoming)
    {
        // TODO - validation
        outgoing.connectedTo = incoming;
        // TODO - update views
    }


    // --- Spinner
    public int SpinSpinner()
    {
        // TODO - animate spinner (?)
        return 1;
    }

 
}