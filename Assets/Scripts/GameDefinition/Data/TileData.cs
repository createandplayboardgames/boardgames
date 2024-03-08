
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileData : MonoBehaviour
{
    private GameDefinitionManager gameDefinitionManager;

    public Transform tilePosition;
    public GameObject tile;


    public bool isEndingTile = false;

    private GameAction associatedAction = null;
    public Boolean shouldFinishGame = false;



    public bool IsEndingTile()
    {
        return associatedAction is FinishGameAction || shouldFinishGame;
    }
    public GameAction getAssociatedAction()
    {
        return associatedAction;
    }
}


