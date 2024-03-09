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
    private void OnEnable(){
        layoutManager = GetComponent<MenuLayoutManager>(); //TODO - circular reference? potential of errors?
        gameDefinitionManager = GameObject.Find("GameDefinitionManager").GetComponent<GameDefinitionManager>();
    }

    //---- Player 
    public void EditPlayer(PlayerData player){
        edititem_player = player;
    }
    public void SetPlayerName(string newValue){
        if (edititem_player == null) return;
        // TODO - we MUST enforce name uniqueness, or weird things will happen with mapping of IDs and playerNames
        edititem_player.playerName = newValue;
    }
    public void SetPlayerPoints(int newValue){
        if (edititem_player == null) return;
        edititem_player.points = newValue;
    }
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
        edititem_player.location = tileData;
        edititem_player.gameObject.GetComponent<PlayerDrop>().SnapToTile(tileData);
    }

    // --- Finish Game 
    public void EditFinishGameAction(FinishGameActionData finishGameActionData){
        edititem_finishGameAction = finishGameActionData;
    }
    public void SetFinishGameWinner(string newValue){
        edititem_finishGameAction.winner = GetPlayerFromPlayerName(newValue);
    }

    // --- Change Points

    public void EditChangePointsAction(ChangePointsActionData changePointsActionData){
        edititem_changePointsAction = changePointsActionData;
    }
    public void SetChangePointsPlayer(string newValue){
        //edititem_changePointsAction.playerID = GetPlayerIDFromName(newValue);
    }
    public void SetChangePointsOp(ChangePointsActionData.Operation newValue){
        edititem_changePointsAction.operation = newValue;
    }
    public void SetChangePointsValue(int newValue){
        edititem_changePointsAction.value = newValue;
    }

    // --- Move To
    public void EditMoveToAction(MoveToActionData moveToActionData){
        edititem_moveToAction = moveToActionData;
    }
    public void SetMoveToPlayer(string newValue){
        //TODO - player parsing!
    }
    public void FindMoveToLocation(){
        throw new NotImplementedException();
    }
    public void StartSetMoveToLocation(){
        throw new NotImplementedException();
    }


    // ---- PlayerData Management (TDOO: move this!)

    public Dictionary<string, Guid> playerNamesToIDs = new();
    private PlayerData GetPlayerFromPlayerName(string playerName){ return GetPlayerDataFromID(GetPlayerIDFromName(playerName)); }
    private Guid GetPlayerIDFromName(string playerName){
        if (!playerNamesToIDs.ContainsKey(playerName))
            return Guid.Empty; // TODO - error
        return playerNamesToIDs[playerName];
    }
    private PlayerData GetPlayerDataFromID(Guid id){
        foreach (PlayerData playerData in gameDefinitionManager.GetAllPlayersAndDummies())
            if (id.Equals(playerData.ID)) return playerData;
        return null;
    }

}

