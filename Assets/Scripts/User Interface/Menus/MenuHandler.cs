using System;
using UnityEngine;
using UnityEngine.UIElements;

public class MenuHandler : MonoBehaviour
{

    private VisualElement root;
    private Button closeButton;
    //--- Main Menus
    private VisualElement tileOptionSquare;
    private VisualElement playerOptionPawn;
    private Button addActionButton;
    //--- Dynamic Menus
    private VisualElement dynamicMenusRoot;
    private VisualElement infoMenuPlayer;
    //players
    private PlayerData currentPlayerData = null;
    private TextField playerNameField;
    private IntegerField playerPointsField;
    private Button playerSetLocationButton;
    public Boolean isRequestingPlayerLocationSet = false;


    public void OnEnable()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        closeButton = root.Q("CloseButton") as Button;
        closeButton.RegisterCallback<ClickEvent>(evt => { SetMainMenuVisible(false); });
        //--- setup Main Menus
        // Tiles
        tileOptionSquare = root.Q("TileOptionSquare");
        tileOptionSquare.RegisterCallback<ClickEvent>(evt => { //TODO: this approach doesn't unregister - problematic?
            GameObject.Find("GameDefinitionManager").GetComponent<GameDefinitionManager>().CreateTile(); 
        });
        // Players
        playerOptionPawn= root.Q("PlayerOptionPawn");
        playerOptionPawn.RegisterCallback<ClickEvent>(evt => {  
            GameObject.Find("GameDefinitionManager").GetComponent<GameDefinitionManager>().CreatePlayer();
        });
        // Actions
        addActionButton = root.Q("AddActionButton") as Button;
        addActionButton.RegisterCallback<ClickEvent>(evt => {
            // TODO - open Create Actions menu
        });

        //--- setup Dynamic Menus
        dynamicMenusRoot = root.Q("DynamicMenus");
        infoMenuPlayer = root.Q("InfoMenuPlayer");
        //player
        playerNameField = root.Q("PlayerNameField") as TextField;
        playerNameField.RegisterValueChangedCallback<string>(evt => { //set name 
            if (currentPlayerData == null) return;
            currentPlayerData.playerName = evt.newValue;
        });
        playerPointsField = root.Q("PlayerPointsField") as IntegerField;
        playerPointsField.RegisterValueChangedCallback<int>(evt => { //set points
            if (currentPlayerData == null) return;
            currentPlayerData.points = evt.newValue;
        });
        playerSetLocationButton = root.Q("PlayerSetLocationButton") as Button;
        playerSetLocationButton.RegisterCallback<ClickEvent>(evt => { //set location
            if (!isRequestingPlayerLocationSet) StartSetPlayerLocation();
            else FinishSetPlayerLocation(null); 
        });

        //initialize
        HideAllDynamicMenus();
        SetMainMenuVisible(false);
    }



    private void RefreshPlayerSetLocationButtonText(){
        playerSetLocationButton.text = isRequestingPlayerLocationSet ? "Setting..." : "Set";
        Debug.Log("setting to " + playerSetLocationButton.text);

    }



    //----- Layout Helpers

    public void SetMainMenuVisible(bool visible)
    {
        GameObject.Find("MainMenu").GetComponent<UIDocument>().rootVisualElement.visible = visible;
    }
    private void HideAllDynamicMenus()
    {
        foreach (var child in dynamicMenusRoot.Children())
            child.style.visibility = Visibility.Hidden;
        dynamicMenusRoot.style.visibility = Visibility.Hidden;

    }
    internal void ShowInforMenuPlayer(PlayerData playerData)
    {
        HideAllDynamicMenus();
        dynamicMenusRoot.style.visibility = Visibility.Visible;
        currentPlayerData = playerData;
        infoMenuPlayer.style.visibility = Visibility.Visible;
        playerNameField.value = playerData.playerName;
        playerPointsField.value = playerData.points;
    }

    internal void ShowInfoMenuTile(TileData tileData)
    {
        throw new NotImplementedException();
    }


    //----- IDK
    private void StartSetPlayerLocation()
    {
        isRequestingPlayerLocationSet = true;
        RefreshPlayerSetLocationButtonText();
    }
    public void FinishSetPlayerLocation(TileData tileData)
    {
        isRequestingPlayerLocationSet = false;
        RefreshPlayerSetLocationButtonText();
        if (currentPlayerData == null || tileData == null) return; 
        currentPlayerData.location = tileData;
        currentPlayerData.gameObject.GetComponent<PlayerDrop>().SnapToTile(tileData);
    }
}
