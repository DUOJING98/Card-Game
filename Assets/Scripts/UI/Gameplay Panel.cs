using System;
using UnityEngine;
using UnityEngine.UIElements;

public class GameplayPanel : MonoBehaviour
{
    [Header("イベント")]
    public ObjectEventSo playerTurnEnd;

    [Header("UI")]
    private VisualElement rootElement;
    private Label energyAmountLabel, drawAmountLabel, discardAmountLabel, turnLabelLabel;
    private Button endTurnButton;


    private void OnEnable()
    {
        rootElement = GetComponent<UIDocument>().rootVisualElement;

        energyAmountLabel = rootElement.Q<Label>("EnergyAmount");
        drawAmountLabel = rootElement.Q<Label>("DrawAmount");
        discardAmountLabel = rootElement.Q<Label>("DiscardAmount");
        turnLabelLabel = rootElement.Q<Label>("TurnLabel");
        endTurnButton = rootElement.Q<Button>("EndTurn");
        endTurnButton.clicked += OnTurnEnd;

  
        energyAmountLabel.text = "0";
        drawAmountLabel.text = "0";
        discardAmountLabel.text = "0";
        turnLabelLabel.text = "ゲーム開始";

    }

    private void OnTurnEnd()
    {
        playerTurnEnd.RaisedEvent(null, this);
    }

    #region UI更新
    public void UpdateDrawDeckAmount(int amount)
    {
        drawAmountLabel.text = amount.ToString();    //ドロー山札のUIを更新する
    }
    public void UpdateDiscardDeckAmount(int amount)
    {
        discardAmountLabel.text = amount.ToString(); //捨て札のUIを更新する
    }

    public void UpdateEnergyAmount(int amount)
    {
        energyAmountLabel.text = amount.ToString();  //エネルギーのUIを更新する
    }
    #endregion
    public void OnEnemyTurnBegin()
    {
        endTurnButton.SetEnabled(false);    //敵のターン中はボタンが無効になる
        turnLabelLabel.text = "エネミー\nターン";
        turnLabelLabel.style.color = new StyleColor(Color.red);
    }


    public void OnPlayerTurnBegin()
    {
        endTurnButton.SetEnabled(true);     //プレイヤーターン中はボタンが有効になる

        turnLabelLabel.text = "プレイヤー\nターン";
        turnLabelLabel.style.color = new StyleColor(Color.white);
    }
}
