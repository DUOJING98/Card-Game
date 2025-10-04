using System;
using UnityEngine;
using UnityEngine.UIElements;

public class GameplayPanel : MonoBehaviour
{
    [Header("���٥��")]
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
        turnLabelLabel.text = "���`���_ʼ";

    }

    private void OnTurnEnd()
    {
        playerTurnEnd.RaisedEvent(null, this);
    }

    #region UI����
    public void UpdateDrawDeckAmount(int amount)
    {
        drawAmountLabel.text = amount.ToString();    //�ɥ�`ɽ����UI����¤���
    }
    public void UpdateDiscardDeckAmount(int amount)
    {
        discardAmountLabel.text = amount.ToString(); //�Τ�����UI����¤���
    }

    public void UpdateEnergyAmount(int amount)
    {
        energyAmountLabel.text = amount.ToString();  //���ͥ륮�`��UI����¤���
    }
    #endregion
    public void OnEnemyTurnBegin()
    {
        endTurnButton.SetEnabled(false);    //���Υ��`���Фϥܥ��󤬟o���ˤʤ�
        turnLabelLabel.text = "���ͥߩ`\n���`��";
        turnLabelLabel.style.color = new StyleColor(Color.red);
    }


    public void OnPlayerTurnBegin()
    {
        endTurnButton.SetEnabled(true);     //�ץ쥤��`���`���Фϥܥ����Є��ˤʤ�

        turnLabelLabel.text = "�ץ쥤��`\n���`��";
        turnLabelLabel.style.color = new StyleColor(Color.white);
    }
}
