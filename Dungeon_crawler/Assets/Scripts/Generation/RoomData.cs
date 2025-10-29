using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomData
{
    public BoundsInt bounds;
    public Vector2Int center;
    public RoomType type;

    public RoomData(BoundsInt bounds, RoomType type = RoomType.Normal)
    {
        this.bounds = bounds;
        this.center = new Vector2Int(Mathf.FloorToInt(bounds.center.x), Mathf.FloorToInt(bounds.center.y));
        this.type = type;
    }
}

public enum RoomType
{
    Normal,
    Start,
    Boss,
    Treasure,
    Shop
}

