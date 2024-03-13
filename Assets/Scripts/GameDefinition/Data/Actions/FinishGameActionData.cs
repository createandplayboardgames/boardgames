using System;
using UnityEngine;

public class FinishGameActionData : ActionData {
    public PlayerData winner;
    public void Start() { winner = GameObject.Find(Keywords.CURRENT_PLAYER_DUMMY).GetComponent<PlayerData>(); }
}
