using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class GameAction { public PlayerData player; }
// ===== Actions
public class FinishGameAction : GameAction {

    public FinishGameAction(PlayerData winner){
        this.player = winner;
    }
}


public class MovePlayerToLocationAction : GameAction
{

    public TileData location;
    public MovePlayerToLocationAction(PlayerData player, TileData location)
    {
        this.player = player;
        this.location = location;
    }
}

public class ChangePlayerPointsAction : GameAction
{

    //TODO - operation
    public ChangePlayerPointsAction(PlayerData player)
    {
        this.player = player;
    }
}



// ===== Dummy Classes
public class CurrentPlayer : PlayerData { }
public class RandomPlayer : PlayerData { }

