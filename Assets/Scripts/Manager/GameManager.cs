using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("マップ配置図")]
    public MapLayoutSo mapLayout;


    public List<Enemy> enemyList;

    [Header("Event")]
    public ObjectEventSo gameWinEvent;
    public ObjectEventSo gameLoseEvent;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void UpdateMapLayoutData(object value)
    {
        var roomVector = (Vector2Int)value;
        if (mapLayout.mapRoomDataList.Count == 0) return;
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

        enemyList.Clear();
    }

    public void OnRoomLoadedEvent(object obj)
    {
        var enemies = FindObjectsByType<Enemy>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (var enemy in enemies)
        {
            enemyList.Add(enemy);
        }
    }


    public void OnCharacterDeadEvent(object character)
    {
        if (character is Player)
        {
            //失敗の通知を送る
            StartCoroutine(EventDelayAction(gameLoseEvent));
        }

        if (character is Boss)
        {
            StartCoroutine(EventDelayAction(gameLoseEvent));
        }

        else if (character is Enemy)
        {
            enemyList.Remove(character as Enemy);

            if (enemyList.Count == 0)
            {
                //勝利の通知を送る
                StartCoroutine(EventDelayAction(gameWinEvent));
            }
        }

        if (character is Boss)
        {
            StartCoroutine(EventDelayAction(gameLoseEvent));
        }
    }

    IEnumerator EventDelayAction(ObjectEventSo eventSo)
    {
        yield return new WaitForSeconds(1.5f);
        eventSo.RaisedEvent(null, this);
    }

    public void OnNewGameEvent()
    {
        mapLayout.mapRoomDataList.Clear();
        mapLayout.linePositionList.Clear();
    }
}
