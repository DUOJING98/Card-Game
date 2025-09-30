using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Room : MonoBehaviour
{
    public int column;
    public int line;


    private SpriteRenderer spriteRenderer;
    public RoomDataSo roomData;
    public RoomState roomState;
    public List<Vector2Int> linkTo = new();


    [Header("Í¨Öª")]
    public ObjectEventSo loadRoomEvent;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

 

    private void OnMouseDown()
    {
        //Debug.Log("Ñº¤¹¤È£º" + roomData.roomType);
        if (roomState == RoomState.Attainable)
            loadRoomEvent.RaisedEvent(this, this);
    }

    public void SetupRoom(int column, int line, RoomDataSo roomData)
    {
        this.column = column;
        this.line = line;
        this.roomData = roomData;

        spriteRenderer.sprite = roomData.roomIcon;

        spriteRenderer.color = roomState switch
        {
            RoomState.Locked => new Color(0.5f, 0.5f, 0.5f, 1f),
            RoomState.Visited => new Color(0.5f, 0.8f, 0.5f, 0.5f),
            RoomState.Attainable => new Color(1f, 1f, 1f, 1f),
            _ => throw new System.NotImplementedException()
        };
    }

}
