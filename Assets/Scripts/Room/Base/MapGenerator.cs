using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [Header("マップ配置表")]
    public MapConfigSo mapConfig;

    [Header("Prefab")]
    public Room roomPrefab;
    public LineRenderer linePrefab;

    private float screenHeight;
    private float screenWidth;

    private float columnWidth;
    public float border;
    private Vector3 generatePoint;

    private List<Room> rooms = new();
    private List<LineRenderer> lines = new();

    private void Awake()
    {
        screenHeight = Camera.main.orthographicSize * 2;
        screenWidth = screenHeight * Camera.main.aspect;

        columnWidth = screenWidth / (mapConfig.roomBlueprints.Count);
    }

    private void Start()
    {
        CreateMap();
    }

    public void CreateMap()
    {
        //前のルームのリスト
        List<Room> previousColumnRooms = new();

        for (int column = 0; column < mapConfig.roomBlueprints.Count; column++)
        {
            var blueprint = mapConfig.roomBlueprints[column];

            var amount = Random.Range(blueprint.min, blueprint.max);

            var startHeight = screenHeight / 2 - screenHeight / (amount + 1);

            generatePoint = new Vector3(-screenWidth / 2 + border + columnWidth * column, startHeight, 0);

            var newPos = generatePoint;
            //今のルームのリスト
            List<Room> currentColumnRooms = new();


            var roomGapY = screenHeight / (amount + 1);

            for (int i = 0; i < amount; i++)
            {
                //最後の列、右に備えて、ボースルーム
                if (column == mapConfig.roomBlueprints.Count - 1)
                {
                    newPos.x = screenWidth / 2 - border * 2;
                }
                else if (column != 0)
                {
                    newPos.x = generatePoint.x + Random.Range(-border / 2, border / 2);
                }
                newPos.y = startHeight - roomGapY * i;
                var room = Instantiate(roomPrefab, newPos, Quaternion.identity, transform);

                rooms.Add(room);
                currentColumnRooms.Add(room);
            }

            if (previousColumnRooms.Count > 0)
            {
                CreateConnections(previousColumnRooms, currentColumnRooms);
            }

            previousColumnRooms = currentColumnRooms;
        }
    }

    private void CreateConnections(List<Room> column1, List<Room> column2)
    {
        HashSet<Room> connectedColumn2Rooms = new();

        foreach (Room room in column1)
        {
            var targetRoom = ConnectedToRandomRoom(room, column2);
            connectedColumn2Rooms.Add(targetRoom);
        }

        foreach (Room room in column2)
        {
            if (!connectedColumn2Rooms.Contains(room))
            {
                ConnectedToRandomRoom(room, column1);
            }
        }
    }

    private Room ConnectedToRandomRoom(Room room, List<Room> column2)
    {
        Room targetRoom;

        targetRoom = column2[Random.Range(0, column2.Count)];

        var line = Instantiate(linePrefab, transform);
        line.SetPosition(0,room.transform.position);
        line.SetPosition(1,targetRoom.transform.position);
        lines.Add(line);
        return targetRoom;
    }



    //reMap
    [ContextMenu(itemName: "ReRoom")]
    public void ReGenerateRoom()
    {
        foreach (var room in rooms)
        {
            Destroy(room.gameObject);
        }

        foreach (var line in lines)
        {
            Destroy(line.gameObject);
        }
        rooms.Clear();
        lines.Clear();
        CreateMap();
    }
}



