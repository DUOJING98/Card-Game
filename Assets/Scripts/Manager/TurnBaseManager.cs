using UnityEngine;

public class TurnBaseManager : MonoBehaviour
{
    public GameObject playerObj;


    private bool isPlayerTurn = false;  //¥×¥ì¥¤¥ä©`¤Î¥¿©`¥ó
    private bool isEnemyTurn = false;   //”³¤Î¥¿©`¥ó
    public bool battleEnd = true;   //¥Ð¥È¥ë¥Õ¥£¥Ë¥Ã¥·¥å

    private float timeCounter;  //¥¿¥¤¥Þ©`

    public float enemyTurnDuration;
    public float playerTurnDuration;

    [Header("¥¤¥Ù¥ó¥È")]
    public ObjectEventSo playerTurnBegin;
    public ObjectEventSo enemyTurnBegin;
    public ObjectEventSo enemyTurnEnd;

    private void Update()
    {
        if (battleEnd) { return; }

        if (isEnemyTurn)
        {
            timeCounter += Time.deltaTime;
            if (timeCounter > enemyTurnDuration)
            {
                timeCounter = 0;
                //¥¨¥Í¥ß©`¥¿©`¥ó¥¨¥ó¥É
                EnemyTurnEnd();
                //¥×¥ì¥¤¥ä©`¥¿©`¥ó¥¹¥¿©`¥È
                isPlayerTurn = true;
            }
        }

        if (isPlayerTurn)
        {
            timeCounter += Time.deltaTime;
            if (timeCounter > playerTurnDuration)
            {
                timeCounter = 0;
                PlayerTurnBegin();
                isPlayerTurn = false;
            }
        }
    }

    [ContextMenu("Game Start")]
    public void GameStart()
    {
        isPlayerTurn = true;
        isEnemyTurn = false;
        battleEnd = false;
        timeCounter = 0;
    }

    public void PlayerTurnBegin()
    {

        playerTurnBegin.RaisedEvent(null, this);
    }

    public void EnemyTurnBegin()
    {
        isEnemyTurn = true;
        enemyTurnBegin.RaisedEvent(null, this);
    }

    public void EnemyTurnEnd()
    {
        isEnemyTurn = false;
        enemyTurnEnd.RaisedEvent(null, this);
    }

    public void OnRoomLoadedEvent(object obj)
    {
        Room room = obj as Room;

        switch (room.roomData.roomType)
        {
            case RoomType.MinorEnemy:
            case RoomType.EliteEnemy:
            case RoomType.Boss:
                playerObj.SetActive(true);
                GameStart();
                break;

            case RoomType.Shop:
            case RoomType.Treasure:
                playerObj.SetActive(false);
                break;

            case RoomType.RestRoom:
                playerObj.SetActive(true);
                break;

        }
    }

    public void OnLoadedMap()
    {
        battleEnd = true;
        playerObj.SetActive(false);
    }

}
