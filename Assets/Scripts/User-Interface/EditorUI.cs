using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.Tilemaps;

/*
 * EditorUI is the basis of user interface for the Create page/Drag and Drop Editor. 
 * It allows for in game tilemap tile placement, with a new gameObject tile created on tile. 
 * 
 * Makes reference to PlayerInput via mouse
 *                    BuildingObject for tiles and player pieces
 *                    
 * Sources Cited: https://www.youtube.com/@VelvaryGames/videos
 *                https://catlikecoding.com/unity/tutorials/basics/game-objects-and-scripts/
 *                https://www.youtube.com/watch?v=qFXnBourCQk&ab_channel=MuddyWolf
 */

public class EditorUI : Singleton<EditorUI>
{
    // Set up instance of GameDefinitionManager
    GameDefinitionManager manager = new GameDefinitionManager();

    SquareTileData tileData;

    // underlying maps for tile movement and placement
    [SerializeField] Tilemap previewMap, defaultMap;

    // defined mouse input
    PlayerInput playerInput;

    TileBase tileBase;

    // selected tile or piece from menu bar
    MenuObject selectedObj;

    // mouse position to camera
    Camera _camera;
    Vector2 mousePos;
    Vector3Int currentGridPosition;
    Vector3Int lastGridPosition;

    protected override void Awake()
    {
        playerInput = new PlayerInput();
        _camera = Camera.main;
    }

    /*
     * Enable editor player input
     */
    public void OnEnable()
    {
        playerInput.Enable();

        playerInput.GamePlay.MousePosition.performed += OnMouseMove;
        playerInput.GamePlay.MouseLeftClick.performed += OnLeftClick;
        playerInput.GamePlay.MouseLeftClick.started += OnLeftClick;
        playerInput.GamePlay.MouseLeftClick.canceled += OnLeftClick;
        playerInput.GamePlay.MouseRightClick.performed += OnRightClick;
    }

    /*
     * Disable editor player input
     */
    public void OnDisable()
    {
        playerInput.Disable();

        playerInput.GamePlay.MousePosition.performed -= OnMouseMove;
        playerInput.GamePlay.MouseLeftClick.performed -= OnLeftClick;
        playerInput.GamePlay.MouseLeftClick.started -= OnLeftClick;
        playerInput.GamePlay.MouseLeftClick.canceled -= OnLeftClick;
        playerInput.GamePlay.MouseRightClick.performed -= OnRightClick;
    }

    /* 
     * Get player selected piece and show preview with mouse movement
     * 
    */
    public MenuObject SelectedObj
    {
        set
        {
            selectedObj = value;
            tileBase = selectedObj != null ? selectedObj.TileBase : null;

            UpdatePreview();
        }
    }

    /*
     * Get Mouse position, show tile preview, 
     * update position on grid
     */
    public void Update()
    {
        // if tile selected, show preview 
        if (selectedObj != null)
        {
            Vector3 pos = _camera.ScreenToWorldPoint(mousePos);
            Vector3Int gridPos = previewMap.WorldToCell(pos);

            if (gridPos != currentGridPosition)
            {
                lastGridPosition = currentGridPosition;
                currentGridPosition = gridPos;

                //update preview
                UpdatePreview();
            }
        }
    }


    /* 
     * Updatepreview shows tile being moved around while not yet placed.
     * Tile location updated to follow cursor.
    */
    public void UpdatePreview()
    {
        //Remove old tile if existing
        previewMap.SetTile(lastGridPosition, null);

        //Set current tile
        previewMap.SetTile(currentGridPosition, tileBase);
    }

    /*
     * Get mouse position
     */
    public void OnMouseMove(InputAction.CallbackContext ctx)
    {
        mousePos = ctx.ReadValue<Vector2>();
    }

    /*
     * Left click to select tile placement if not over tile option bar.
     * TODO: Add gameObject tile
     */
    public void OnLeftClick(InputAction.CallbackContext ctx)
    {
        if (selectedObj != null && !EventSystem.current.IsPointerOverGameObject())
        {
            PlaceObject();
            /* TODO:
            tileData = tileData.AddComponent<SquareTileData>();
            manager.CreateTile();
            */
        }
    }

    /*
     *  Right Click to remove tile from scene.
     *  TODO: Remove gameObject tile
     */
    public void OnRightClick(InputAction.CallbackContext ctx)
    {
        if (defaultMap.HasTile(currentGridPosition))
        {
            defaultMap.SetTile(currentGridPosition, null);
            /* TODO:
            manager.DeleteTile(tileData);
            */

        }
    }

    /*
     * Selected tile or other component
     */
    public void ObjectSelected(MenuObject obj)
    {
        SelectedObj = obj;

    }


    /*
     * Place tile on default map. 
     */
    public void PlaceObject()
    {
        if (selectedObj != null)
        {
            defaultMap.SetTile(currentGridPosition, tileBase);
        }
    }

}
