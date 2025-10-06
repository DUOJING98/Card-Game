using UnityEngine;

public class TurnBaseManager : MonoBehaviour
{
    public GameObject playerObj;


    private bool isPlayerTurn = false;  //プレイヤーのターン
    private bool isEnemyTurn = false;   //敵のターン
    public bool battleEnd = true;   //バトルフィニッシュ

    private float timeCounter;  //タイマー

    public float enemyTurnDuration;
    public float playerTurnDuration;

    [Header("イベント")]
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
                //エネミーターンエンド
                EnemyTurnEnd();
                isPlayerTurn = true;
            }
        }

        if (isPlayerTurn)
        {
            timeCounter += Time.deltaTime;
            if (timeCounter > playerTurnDuration)
            {
                //プレイヤーターンスタート
                PlayerTurnBegin();
                timeCounter = 0;
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
        var p = playerObj ? playerObj.GetComponent<Player>() : null;
        if (p != null)
        {
            p.NewTurn(); // 这里面会把 currentMana = max，并（建议）广播 manaChangedEvent
        }
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
                playerObj.GetComponent<PlayerAnim>().SetSleepAction();
                break;

        }
    }

    public void OnLoadedMap()
    {
        battleEnd = true;
        playerObj.SetActive(false);
    }

    public void NewGame()
    {
        playerObj.GetComponent<Player>().NewGame();
    }
}
