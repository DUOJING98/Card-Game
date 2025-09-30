using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("¥Ş¥Ã¥×ÅäÖÃ‡í")]
    public MapLayoutSo mapLayout;

    public void UpdateMapLayoutData(object value)
    {
        var roomVector = (Vector2Int)value;
        var currenRoom = mapLayout.mapRoomDataList.Find(r => r.column == roomVector.x && r.line == roomVector.y);
        currenRoom.roomState = RoomState.Visited;

        var sameColumnRooms = mapLayout.mapRoomDataList.FindAll(r => r.column == currenRoom.column);

        foreach (var room in sameColumnRooms)
        {
            if (room.line != roomVector.y)
                room.roomState = RoomState.Locked;
        }

        foreach (var link in currenRoom.linkTo)
        {
            var linkedRoom = mapLayout.mapRoomDataList.Find(r => r.column == link.x && r.line == link.y);
            linkedRoom.roomState = RoomState.Attainable;
        }
    }
}
