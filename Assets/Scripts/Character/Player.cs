using UnityEngine;

public class Player : CharacterBse
{
    public IntVariable playerMana;

    public int maxMana;

    public int currentMana { get => playerMana.currentValue; set => playerMana.SetValue(value); }


    private void OnEnable()
    {
        playerMana.maxValue = maxMana;
        currentMana = playerMana.maxValue;   //初期エネルギーを設定する
    }

    public void NewTurn()
    {
        Debug.Log($"[Energy] Before Refill: {currentMana}/{maxMana}");
        currentMana = maxMana;  //新しいターンでエネルギーを回復する 
        Debug.Log($"[Energy] After  Refill: {currentMana}/{maxMana}");


    }

    public void UpdateMana(int cost)
    {
        currentMana -= cost;
        if (currentMana <= 0)
        {
            currentMana = 0;
        }
    }

    public void NewGame()
    {
        CurrentHP = MaxHP;
        isDead = false;
        buffRound.currentValue = buffRound.maxValue;
        NewTurn();
    }
}
