using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Select : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData){
        SelectPiece();
    }
    public void SelectPiece()
    {
        MenuLayoutManager layoutManager = GameObject.Find("MenuManager").GetComponent<MenuLayoutManager>();
        switch (GameDefinitionManager.GetGamePieceData(gameObject)){
            case PlayerData data:               layoutManager.ShowInforMenuPlayer(data); break;
            case TileData data:                 layoutManager.ShowInfoMenuTile(data); break;
            case FinishGameActionData data:     layoutManager.ShowInfoMenuFinishGameAction(data); break;
            case ChangePointsActionData data:   layoutManager.ShowInfoMenuChangePointsAction(data); break;
            case MoveToActionData data:         layoutManager.ShowInfoMenuMoveToAction(data); break;
            case BlockPathActionData data:      layoutManager.ShowInfoMenuBlockPathAction(data); break;
            default:                            Debug.Log("invalid item selected"); break;
        }
    }   
}
