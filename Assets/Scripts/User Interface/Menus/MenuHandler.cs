using System;
using System.Xml.Linq;
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
    private VisualElement infoMenuTile;
    //players 
    private PlayerData currentPlayerData = null;
    private TextField playerNameField;
    private IntegerField playerPointsField;
    private Button playerSetLocationButton;
    public Boolean isRequestingPlayerLocationSet = false;


    public void OnEnable()
    {
        root = GameObject.Find("MainMenu").GetComponent<UIDocument>().rootVisualElement;
        closeButton = root.Q("CloseButton") as Button;
        closeButton.RegisterCallback<ClickEvent>(evt => { SetMenuRootShown(false); });
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
        infoMenuTile = root.Q("InfoMenuTile");
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
        SetMenuRootShown(false);
        SetAllDynamicMenusUnShown();
    }

    private void RefreshPlayerSetLocationButtonText(){
        playerSetLocationButton.text = isRequestingPlayerLocationSet ? "Setting..." : "Set";
        Debug.Log("setting to " + playerSetLocationButton.text);
    }


    //----- Layout Helpers
    // Basic Methods to Show/Hide
    private void SetElementVisible(VisualElement element, bool visible){
        element.style.visibility = visible ? Visibility.Visible : Visibility.Hidden;
    }
    private void SetElementDisplayed(VisualElement element, bool displayed){
        element.style.display = displayed ? DisplayStyle.Flex : DisplayStyle.None;
    }

    // Show/Hides
    public void SetMenuRootShown(bool shown){
        SetElementDisplayed(root, shown);
    }
    public void SetDynamicMenuElementShown(VisualElement element, bool shown){
        SetElementDisplayed(element, shown); 
    }
    private void SetAllDynamicMenusUnShown(){
        foreach (var child in dynamicMenusRoot.Children())
            SetDynamicMenuElementShown(child, false);
    }
    public void ShowInforMenuPlayer(PlayerData playerData){
        SetAllDynamicMenusUnShown();
        SetDynamicMenuElementShown(infoMenuPlayer, true);
        currentPlayerData = playerData;
        playerNameField.value = playerData.playerName;
        playerPointsField.value = playerData.points;
    }

    public void ShowInfoMenuTile(TileData tileData){
        SetAllDynamicMenusUnShown();
        SetDynamicMenuElementShown(infoMenuTile, true);
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
