using System;
using UnityEngine;

public class NonTileData : GameItemData {
    public Guid ID = Guid.NewGuid();
    public TileData location;
}