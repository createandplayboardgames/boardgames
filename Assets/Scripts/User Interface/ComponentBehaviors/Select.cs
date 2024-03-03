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
        MenuHandler menuHandler = GameObject.Find("MainMenu").GetComponent<MenuHandler>();
        //check type, then perform action - depedent on application state
        PlayerData playerData = GetComponent<PlayerData>();
        if (playerData)
        {
            if (!menuHandler.isRequestingPlayerLocationSet)
                GameObject.Find("MainMenu").GetComponent<MenuHandler>().ShowInforMenuPlayer(playerData);
            return;
        }
        TileData tileData = GetComponent<TileData>();
        if (tileData)
        {
            if (menuHandler.isRequestingPlayerLocationSet) {
                menuHandler.FinishSetPlayerLocation(tileData);
            } else
            {
                menuHandler.ShowInfoMenuTile(tileData);
            }

            return;
        }

        //TODO - action
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
