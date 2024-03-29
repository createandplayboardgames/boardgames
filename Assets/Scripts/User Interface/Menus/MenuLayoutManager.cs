using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MenuLayoutManager : MonoBehaviour
{

    private MenuController controller;
    private GameDefinitionManager gameDefinitionManager;

    private VisualElement root;
    private Button closeButton;
    //--- Info Menus 
    private VisualElement infoMenu_root;
    private VisualElement infoMenu_player;
    private VisualElement infoMenu_tile;
    private VisualElement infoMenu_finishGame;
    private VisualElement infoMenu_changePoints;
    private VisualElement infoMenu_moveTo;
    // --- Inputs
    private VisualElement input_add_tile_square;
    private VisualElement input_add_player_pawn;
    private VisualElement input_add_action_finishGame;
    private VisualElement input_add_action_changePoints;
    private VisualElement input_add_action_moveTo;
    private TextField       input_player_playerName;
    private IntegerField    input_player_points;
    private Button          input_player_setLocation;
    private DropdownField   input_finishGame_winner;
    private DropdownField   input_changePoints_player;
    private EnumField       input_changePoints_op;
    private IntegerField    input_changePoints_val;
    private DropdownField   input_moveTo_player;
    private Button          input_moveTo_set;
    private Button          input_moveTo_find;

    public void Start(){
        
        // Initialize external dependencies
        controller = GetComponent<MenuController>();
        gameDefinitionManager = GameObject.Find("GameDefinitionManager").GetComponent<GameDefinitionManager>();

        // Initialize User-Interface
        root = GameObject.Find("MainMenu").GetComponent<UIDocument>().rootVisualElement;
        closeButton = root.Q("CloseButton") as Button;
        closeButton.RegisterCallback<ClickEvent>(evt => { SetRootShown(false); });
        //--- Info Menus
        infoMenu_root = root.Q("InfoMenus");
        infoMenu_tile           = root.Q("infoMenu_tile");
        infoMenu_player         = root.Q("infoMenu_player");
        infoMenu_finishGame     = root.Q("infoMenu_finishGame");
        infoMenu_changePoints   = root.Q("infoMenu_changePoints");
        infoMenu_moveTo         = root.Q("infoMenu_moveTo");
        //--- Inputs
        // add
        input_add_tile_square = root.Q("input_add_tile_square");
        input_add_tile_square.RegisterCallback<ClickEvent>(evt => { //TODO: this approach doesn't unregister - problematic?
            gameDefinitionManager.CreateTile();
        });
        input_add_player_pawn = root.Q("input_add_player_pawn");
        input_add_player_pawn.RegisterCallback<ClickEvent>(evt => {
            gameDefinitionManager.CreatePlayer();
        });
        input_add_action_finishGame = root.Q("input_add_action_finishGame");
        input_add_action_finishGame.RegisterCallback<ClickEvent>(evt => {
            gameDefinitionManager.CreateFinishGameAction();
        });
        input_add_action_changePoints = root.Q("input_add_action_changePoints");
        input_add_action_changePoints.RegisterCallback<ClickEvent>(evt => {
            gameDefinitionManager.CreateChangePointsAction();
        });
        input_add_action_moveTo = root.Q("input_add_action_moveTo");
        input_add_action_moveTo.RegisterCallback<ClickEvent>(evt => {
            gameDefinitionManager.CreateMoveToAction();
        });
        // player
        input_player_playerName  = root.Q("input_player_playerName") as TextField;
        input_player_playerName.RegisterValueChangedCallback<string>(evt => {
            controller.SetPlayerName(evt.newValue);
        });
        input_player_points = root.Q("input_player_points") as IntegerField;
        input_player_points.RegisterValueChangedCallback<int>(evt => {
            controller.SetPlayerPoints(evt.newValue);
        });
        input_player_setLocation = root.Q("input_player_setLocation") as Button;
        input_player_setLocation.RegisterCallback<ClickEvent>(evt => {
        });
        // finish-game
        input_finishGame_winner = root.Q("input_finishGame_winner") as DropdownField;
        input_finishGame_winner.choices.Add("hello");
        input_finishGame_winner.RegisterValueChangedCallback<string>(evt =>{
            controller.SetFinishGameWinner(evt.newValue);
        });
        // change points 
        input_changePoints_player = root.Q("input_changePoints_player") as DropdownField;
        input_changePoints_player.RegisterValueChangedCallback<string>(evt => {
            controller.SetChangePointsPlayer(evt.newValue);
        });
        input_changePoints_op = root.Q("input_changePoints_op") as EnumField;
        input_changePoints_op.RegisterValueChangedCallback<Enum>(evt =>{
            controller.SetChangePointsOp((ChangePointsActionData.Operation)evt.newValue);
        });
        input_changePoints_val = root.Q("input_changePoints_val") as IntegerField;
        input_changePoints_val.RegisterValueChangedCallback<int>(evt => {
            controller.SetChangePointsValue(evt.newValue);
        });
        // move player
        input_moveTo_player = root.Q("input_moveTo_player") as DropdownField;
        input_moveTo_player.RegisterValueChangedCallback<string>(evt => {
            controller.SetMoveToPlayer(evt.newValue);
        });
        input_moveTo_set = root.Q("input_moveTo_set") as Button ;
        input_moveTo_set.RegisterCallback<ClickEvent>(evt => {
            controller.StartSetMoveToLocation();
        }); 
        input_moveTo_find = root.Q("input_moveTo_find") as Button ;
        input_moveTo_find.RegisterCallback<ClickEvent>(evt => {
            //controller.FindMoveToLocation();
        });

        //initialize
        SetRootShown(false);
        HideAllInfoMenus();
    }


    // ---- Show/Hide Info Menus
    public void ShowInforMenuPlayer(PlayerData playerData){
        HideAllInfoMenus();
        SetInfoMenuShown(infoMenu_player, true);
        controller.EditPlayer(playerData);
        //populate menu
        input_player_playerName.value = playerData.playerName;
        input_player_points.value = playerData.points;
    }
    public void ShowInfoMenuTile(TileData tileData){
        HideAllInfoMenus();
        SetInfoMenuShown(infoMenu_tile, true);
        //TODO - populate menu
    }
    public void ShowInfoMenuFinishGameAction(FinishGameActionData finishGameActionData){
        HideAllInfoMenus();
        SetInfoMenuShown(infoMenu_finishGame, true);
        controller.EditFinishGameAction(finishGameActionData);
        //populate menu
        RefreshPlayerDropdownField(input_finishGame_winner);
        Debug.Log(input_finishGame_winner.choices);
        Debug.Log(input_finishGame_winner.choices.IndexOf(finishGameActionData.winner.playerName));
        input_finishGame_winner.index = input_finishGame_winner.choices.IndexOf(finishGameActionData.winner.playerName);
    }
    public void ShowInfoMenuChangePointsAction(ChangePointsActionData changePointsActionData){
        HideAllInfoMenus();
        SetInfoMenuShown(infoMenu_changePoints, true);
        controller.EditChangePointsAction(changePointsActionData);
        //populate menu
        RefreshPlayerDropdownField(input_changePoints_player);
        input_changePoints_player.index = input_finishGame_winner.choices.IndexOf(changePointsActionData.player.playerName);
        input_changePoints_op.value = changePointsActionData.operation;
        input_changePoints_val.value = changePointsActionData.value;
    }
    public void ShowInfoMenuMoveToAction(MoveToActionData moveToActionData){
        HideAllInfoMenus();
        SetInfoMenuShown(infoMenu_moveTo, true);
        controller.EditMoveToAction(moveToActionData);
        //TODO - populate menu
    }
    // ---- Show/Hide Others
    public void SetRootShown(bool shown) { 
        HideAllInfoMenus();
        SetElementDisplayed(root, shown);
    }
    private void HideAllInfoMenus(){
        foreach (var child in infoMenu_root.Children())
            SetInfoMenuShown(child, false);
    }
    public void SetInfoMenuShown(VisualElement infoMenu, bool shown){
        SetElementDisplayed(infoMenu, shown);
    }
    // ---- Show/Hide Essentials
    private void SetElementVisible(VisualElement element, bool visible){
        element.style.visibility = visible ? Visibility.Visible : Visibility.Hidden;
    }
    private void SetElementDisplayed(VisualElement element, bool displayed){
        element.style.display = displayed ? DisplayStyle.Flex : DisplayStyle.None;
    }

    // ---- Player Dropdown Helpers 
    private void RefreshPlayerDropdownField(DropdownField dropdownField){
        dropdownField.choices.Clear(); 
        controller.playerNameIDMap.Clear(); //clear
        foreach (PlayerData playerData in controller.GetAllPlayersAndDummies()) {
            Debug.Log("adding " + playerData.playerName + " to dropdown");
            controller.playerNameIDMap.Add(playerData.playerName, playerData.ID); //map name-id
            dropdownField.choices.Add(playerData.playerName); //add name
        }

    }

}
