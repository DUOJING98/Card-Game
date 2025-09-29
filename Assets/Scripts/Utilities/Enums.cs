using System;

[Flags]
public enum RoomType
{
    MinorEnemy = 1,

    EliteEnemy = 2,

    shop = 4,

    Treasure = 8,

    RestRoom = 16,

    Boss = 32
}


public enum RoomState
{
    Locked,
    Visited,
    Attainable
}

