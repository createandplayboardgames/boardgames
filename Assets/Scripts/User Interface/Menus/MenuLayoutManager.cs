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
    private VisualElement infoMenu_blockPath;
    // --- Inputs
    public VisualElement input_add_tile_square;
    public VisualElement input_add_tile_grass;
    public VisualElement input_add_tile_dirt;
    public VisualElement input_add_tile_water;
    public VisualElement input_add_player_pawn;
    public VisualElement input_add_player_dog;
    public VisualElement input_add_player_tractor;
    public VisualElement input_add_player_hat;
    public VisualElement input_add_action_finishGame;
    public VisualElement input_add_action_changePoints;
    public VisualElement input_add_action_moveTo;
    public VisualElement input_add_action_blockPath;
    public TextField       input_player_playerName;
    public IntegerField    input_player_points;
    public Button          input_player_setLocation;
    public DropdownField   input_finishGame_winner;
    public DropdownField   input_changePoints_player;
    public EnumField       input_changePoints_op;
    public IntegerField    input_changePoints_val;
    public DropdownField   input_moveTo_player;
    public Button          input_moveTo_set;
    public Button          input_moveTo_find;

    public void Start(){
        
        // Initialize external dependencies
        controller = GetComponent<MenuController>();
        gameDefinitionManager = GameObject.Find("GameDefinitionManager").GetComponent<GameDefinitionManager>();

        // Initialize User-Interface
        root = GameObject.Find("EditMenu").GetComponent<UIDocument>().rootVisualElement;
        closeButton = root.Q("CloseButton") as Button;
        closeButton.RegisterCallback<ClickEvent>(evt => { SetRootShown(false); });
        //--- Info Menus
        infoMenu_root           = root.Q("InfoMenus");
        infoMenu_tile           = root.Q("infoMenu_tile");
        infoMenu_player         = root.Q("infoMenu_player");
        infoMenu_finishGame     = root.Q("infoMenu_finishGame");
        infoMenu_changePoints   = root.Q("infoMenu_changePoints");
        infoMenu_moveTo         = root.Q("infoMenu_moveTo");
        infoMenu_blockPath      = root.Q("infoMenu_blockPath");
        //--- Inputs
        // add
        input_add_tile_square = root.Q("input_add_tile_square");
        input_add_tile_square.RegisterCallback<ClickEvent>(evt => { //TODO: this approach doesn't unregister - problematic?
            gameDefinitionManager.CreateTile();
        });
        input_add_tile_grass = root.Q("input_add_tile_grass");
        input_add_tile_grass.RegisterCallback<ClickEvent>(evt => {
            var obj = gameDefinitionManager.CreateTile();
            gameDefinitionManager.AssignSprite(obj.gameObject, "images/Tiles/grass");
        });
        input_add_tile_dirt = root.Q("input_add_tile_dirt");
        input_add_tile_dirt.RegisterCallback<ClickEvent>(evt => {
            var obj = gameDefinitionManager.CreateTile();
            gameDefinitionManager.AssignSprite(obj.gameObject, "images/Tiles/dirt");
        });
        input_add_tile_water = root.Q("input_add_tile_water");
        input_add_tile_water.RegisterCallback<ClickEvent>(evt => {
            var obj = gameDefinitionManager.CreateTile();
            gameDefinitionManager.AssignSprite(obj.gameObject, "images/Tiles/water");
        });
       
       
        input_add_player_pawn = root.Q("input_add_player_pawn");
        input_add_player_pawn.RegisterCallback<ClickEvent>(evt => {
            gameDefinitionManager.CreatePlayer();
        });
        input_add_player_dog = root.Q("input_add_player_dog");
        input_add_player_dog.RegisterCallback<ClickEvent>(evt => {
            var obj = gameDefinitionManager.CreatePlayer();
            if (obj) gameDefinitionManager.AssignSprite(obj.gameObject, "images/Players/dog");
        });
        input_add_player_tractor = root.Q("input_add_player_tractor");
        input_add_player_tractor.RegisterCallback<ClickEvent>(evt => {
            var obj = gameDefinitionManager.CreatePlayer();
            if (obj) gameDefinitionManager.AssignSprite(obj.gameObject, "images/Players/tractor");
        });
        input_add_player_hat = root.Q("input_add_player_hat");
        input_add_player_hat.RegisterCallback<ClickEvent>(evt => {
            var obj = gameDefinitionManager.CreatePlayer();
            if (obj) gameDefinitionManager.AssignSprite(obj.gameObject, "images/Players/hat");
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
        input_add_action_blockPath = root.Q("input_add_action_blockPath");
        input_add_action_blockPath.RegisterCallback<ClickEvent>(evt => {
            gameDefinitionManager.CreateBlockPathAction();
        });
        // player
        input_player_playerName  = root.Q("input_player_playerName") as TextField;
        input_player_playerName.RegisterCallback<BlurEvent>(evt => { 
            controller.SetPlayerName(input_player_playerName.value); // called on editing finished 
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
    public void ShowInfoMenuBlockPathAction(BlockPathActionData blockPathActionData){
        HideAllInfoMenus();
        //SetInfoMenuShown(infoMenu_blockPath, true);
        //empty, at the moment
    }
    // ---- Show/Hide Others
    public void SetRootShown(bool shown) { 
        HideAllInfoMenus();
        SetElementDisplayed(root, shown);
    }
    public void HideAllInfoMenus(){
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
            controller.playerNameIDMap.Add(playerData.playerName, playerData.ID); //map name-id
            dropdownField.choices.Add(playerData.playerName); //add name
        }

    }

}
