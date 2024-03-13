using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToActionData : ActionData {
    public PlayerData playerToMove;
    public void Start() { playerToMove = GameObject.Find(Keywords.CURRENT_PLAYER_DUMMY).GetComponent<PlayerData>(); }
    public TileData targetLocation;
}
