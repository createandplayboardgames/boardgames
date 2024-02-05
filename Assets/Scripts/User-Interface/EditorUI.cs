using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.Tilemaps;

public class EditorUI : Singleton<EditorUI>
{
    // underlying maps for tile movement and placement
    [SerializeField] Tilemap previewMap, defaultMap;

    // defined mouse input
    PlayerInput playerInput;

    TileBase tileBase;

    // selected tile or piece in this script we can call the data for each tile
    BuildingObject selectedObj;

    // mouse position to camera
    Camera _camera;
    Vector2 mousePos;
    Vector3Int currentGridPosition;
    Vector3Int lastGridPosition;

    protected override void Awake()
    {
        base.Awake();
        playerInput = new PlayerInput();
        _camera = Camera.main;
    }

    /*
     * Enable editor player input
     */
    private void OnEnable()
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
    private void OnDisable()
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
    private BuildingObject SelectedObj
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
    private void Update()
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

                //PlaceObject();
            }
        }
    }


    /* 
     * Updatepreview shows tile being moved around while not yet placed.
     * Tile location updated to follow cursor.
    */
    private void UpdatePreview()
    {
        //Remove old tile if existing
        previewMap.SetTile(lastGridPosition, null);

        //Set current tile
        previewMap.SetTile(currentGridPosition, tileBase);
    }

    /*
     * Get mouse position
     */
    private void OnMouseMove(InputAction.CallbackContext ctx)
    {
        mousePos = ctx.ReadValue<Vector2>();
    }

    /*
     * Left click to select tile placement if not over tile option bar.
     */
    private void OnLeftClick(InputAction.CallbackContext ctx)
    {
        if (selectedObj != null && !EventSystem.current.IsPointerOverGameObject())
        {
            PlaceObject();

        }
    }

    /*
     *  Right Click to remove tile from scene.
     */
    private void OnRightClick(InputAction.CallbackContext ctx)
    {
        if (defaultMap.HasTile(currentGridPosition))
        {
            defaultMap.SetTile(currentGridPosition, null);
        }
    }

    /*
     * Selected tile or other component
     */
    public void ObjectSelected(BuildingObject obj)
    {
        SelectedObj = obj;

    }


    /*
     * Place tile on default map.
     */
    private void PlaceObject()
    {
        if (selectedObj != null)
        {
            defaultMap.SetTile(currentGridPosition, tileBase);
        }
    }

}
