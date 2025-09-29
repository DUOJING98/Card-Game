using Unity.VisualScripting;
using UnityEngine;

public class Room : MonoBehaviour
{
    public int column;
    public int line;


    private SpriteRenderer spriteRenderer;
    public RoomDataSo roomData;
    public RoomState roomState;
    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        SetupRoom(0, 0, roomData);
    }

    private void OnMouseDown()
    {
        Debug.Log("Ñº¤¹¤È£º" + roomData.roomType);
    }

    public void SetupRoom(int column, int line, RoomDataSo roomData)
    {
        this.column = column;
        this.line = line;
        this.roomData = roomData;

        spriteRenderer.sprite = roomData.roomIcon;
    }

}
