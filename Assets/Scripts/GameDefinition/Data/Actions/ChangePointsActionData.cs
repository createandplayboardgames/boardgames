using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePointsActionData : MonoBehaviour
{
    public enum Operation { EQUALS, PLUS }
    public PlayerData player = GameDefinitionManager.CURRENT_PLAYER_DUMMY;
    public Operation operation = Operation.EQUALS;
    public int value = 0;
}

