using UnityEngine;

public class TurnBaseManager : MonoBehaviour
{
    private bool isPlayerTurn = false;  //�ץ쥤��`�Υ��`��
    private bool isEnemyTurn = false;   //���Υ��`��
    public bool battleEnd = true;   //�Хȥ�ե��˥å���

    private float timeCounter;  //�����ީ`

    public float enemyTurnDuration;
    public float playerTurnDuration;

    [Header("���٥��")]
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
                //���ͥߩ`���`�󥨥��
                EnemyTurnEnd();
                //�ץ쥤��`���`�󥹥��`��
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

   
}
