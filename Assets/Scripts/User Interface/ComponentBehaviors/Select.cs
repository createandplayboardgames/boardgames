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
        MenuController controller = GameObject.Find("MenuManager").GetComponent<MenuController>();
        MenuLayoutManager layoutManager = GameObject.Find("MenuManager").GetComponent<MenuLayoutManager>();
        // Get piece's type (by checking existence of associated data), then, perform actions

        PlayerData playerData = GetComponent<PlayerData>();
        if (playerData){
            if (!controller.isRequestingPlayerLocationSet) //TODO - move these checks into controller
                layoutManager.ShowInforMenuPlayer(playerData);
            return;
        }
        TileData tileData = GetComponent<TileData>();
        if (tileData){
            if (controller.isRequestingPlayerLocationSet) {
                controller.FinishSetPlayerLocation(tileData);
            } else {
                layoutManager.ShowInfoMenuTile(tileData);
            }
            return;
        }
        FinishGameActionData finishGameActionData = GetComponent<FinishGameActionData>();
        if (finishGameActionData){
            layoutManager.ShowInfoMenuFinishGameAction(finishGameActionData);
            return;
        }
        ChangePointsActionData changePointsActionData = GetComponent<ChangePointsActionData>();
        if (changePointsActionData){
            layoutManager.ShowInfoMenuChangePointsAction(changePointsActionData);
            return;
        }
        MoveToActionData moveToActionData = GetComponent<MoveToActionData>();
        if (moveToActionData){
            layoutManager.ShowInfoMenuMoveToAction(moveToActionData);
            return;
        }
    }   
    
}
