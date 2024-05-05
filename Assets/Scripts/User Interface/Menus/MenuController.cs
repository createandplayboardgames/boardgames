using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{

    MenuLayoutManager layoutManager;
    GameDefinitionManager gameDefinitionManager;

    // These are the items that may be edited through the menu...
    private PlayerData              edititem_player = null;
    private FinishGameActionData    edititem_finishGameAction = null;
    private ChangePointsActionData  edititem_changePointsAction = null;
    private MoveToActionData        edititem_moveToAction = null;
    public Boolean isRequestingPlayerLocationSet = false;
    public Dictionary<string, Guid> playerNameIDMap = new();

    private void Start(){
        layoutManager = GetComponent<MenuLayoutManager>(); //TODO - circular reference? potential of errors?
        gameDefinitionManager = GameObject.Find("GameDefinitionManager").GetComponent<GameDefinitionManager>();
    }

    // ===== Player 
    public void EditPlayer(PlayerData player){
        edititem_player = player;
    }
    public void SetPlayerName(string newValue){
        if (edititem_player == null) return; 
        foreach (PlayerData player in gameDefinitionManager.cache.players){ // check name uniqueness
            if (player.playerName != newValue) 
                continue; 
            Debug.Log("name not unique!"); //TODO - show error text!
            layoutManager.input_player_playerName.value = edititem_player.playerName; // revert text field
            return;
        }
        edititem_player.playerName = newValue;
    }
    public void SetPlayerPoints(int newValue){
        if (edititem_player == null) return;
        edititem_player.points = newValue;
    }
    /* Feature Disabled, currently
    public void TogglePlayerLocationSet(){
        if (!isRequestingPlayerLocationSet) StartSetPlayerLocation();
        else FinishSetPlayerLocation(null);
    }
    public void StartSetPlayerLocation(){
        if (edititem_player == null) return;
        isRequestingPlayerLocationSet = true;
    }
    public void FinishSetPlayerLocation(TileData tileData){
        isRequestingPlayerLocationSet = false;
        if (edititem_player == null || tileData == null) return;
        //edititem_player.location = tileData;
        //TODO - update tile information
        //gameDefinitionManager.SnapToTile(tileData);
    }
    */


    // ===== Finish Game 
    public void EditFinishGameAction(FinishGameActionData finishGameActionData){
        edititem_finishGameAction = finishGameActionData;
    }
    public void SetFinishGameWinner(string newValue){
        edititem_finishGameAction.winner = GetPlayer(newValue);
    }

    // ===== Change Points
    public void EditChangePointsAction(ChangePointsActionData changePointsActionData){
        edititem_changePointsAction = changePointsActionData;
    }
    public void SetChangePointsPlayer(string newValue){
        edititem_changePointsAction.player= GetPlayer(newValue);
    }
    public void SetChangePointsOp(ChangePointsActionData.Operation newValue){
        edititem_changePointsAction.operation = newValue;
    }
    public void SetChangePointsValue(int newValue){
        edititem_changePointsAction.value = newValue;
    }

    // ===== Move To
    public void EditMoveToAction(MoveToActionData moveToActionData){
        edititem_moveToAction = moveToActionData;
    }
    public void SetMoveToPlayer(string newValue){
        //TODO - copy from above
    }
    public void StartSetMoveToLocation(){
        // TODO - click on tile
    }





    // ===== Helper Methods

    public PlayerData GetPlayer(Guid playerID){
        foreach (PlayerData playerData in GetAllPlayersAndDummies())
            if (playerID.Equals(playerData.ID))
                return playerData;
        return null;
    }

    public List<PlayerData> GetAllPlayersAndDummies()
    {
        List<PlayerData> all = new();
        all.Add(GameObject.Find(Keywords.CURRENT_PLAYER_DUMMY).GetComponent<PlayerData>()); //add dummy classes
        all.Add(GameObject.Find(Keywords.RANDOM_PLAYER_DUMMY).GetComponent<PlayerData>());
        all.AddRange(gameDefinitionManager.cache.players); //copy
        return all;
    }

    public PlayerData GetPlayer(string playerName){
        return GetPlayer(GetPlayerID(playerName));
    }
    public Guid GetPlayerID(string playerName){
        if (!playerNameIDMap.ContainsKey(playerName))
            return Guid.Empty; // TODO - error
        return playerNameIDMap[playerName];
    }


}

