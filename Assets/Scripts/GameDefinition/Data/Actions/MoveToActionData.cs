using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToActionData : MonoBehaviour{
    public PlayerData playerToMove = GameDefinitionManager.CURRENT_PLAYER_DUMMY;
    public TileData location;
}
