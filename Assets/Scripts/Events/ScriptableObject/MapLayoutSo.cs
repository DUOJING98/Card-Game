using UnityEngine;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "MapLayoutSo", menuName = "Map/MapLayoutSo")]
public class MapLayoutSo : ScriptableObject
{
    public List<MapRoomData> mapRoomDataList = new();
    public List<LinePosition> linePositionList = new();
}

[System.Serializable]
public class MapRoomData
{
    public float posX, posY;
    public int column, line;

    public RoomDataSo roomData;
    public RoomState roomState;

    public List<Vector2Int> linkTo;
}

[System.Serializable]
public class LinePosition
{
    public SerializeVector3 startPos, endPos;
}
