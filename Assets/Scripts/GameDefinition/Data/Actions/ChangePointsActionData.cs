using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePointsActionData : ActionData {
    public enum Operation { EQUALS, PLUS }
    public PlayerData player;
    public void Start() { player = GameObject.Find(Keywords.RANDOM_PLAYER_DUMMY).GetComponent<PlayerData>(); }
    public Operation operation = Operation.EQUALS;
    public int value = 0;

}
 
