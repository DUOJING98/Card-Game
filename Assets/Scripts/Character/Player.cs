using UnityEngine;

public class Player : CharacterBse
{
    public IntVariable playerMana;

    public int maxMana;

    public int currentMana { get => playerMana.currentValue; set => playerMana.SetValue(value); }


    private void OnEnable()
    {
        playerMana.maxValue = maxMana;
        currentMana = playerMana.maxValue;   //���ڥ��ͥ륮�`���O������
    }

    public void NewTurn()
    {
        currentMana = maxMana;  //�¤������`��ǥ��ͥ륮�`��؏ͤ���
    }

    public void UpdateMana(int cost)
    {
        currentMana -= cost;
        if (currentMana <= 0)
        {
            currentMana = 0;
        }
    }
}
