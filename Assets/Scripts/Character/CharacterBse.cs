using System.Buffers;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterBse : MonoBehaviour
{
    public int maxHP;
    public IntVariable hp;
    public IntVariable defense;
    public IntVariable buffRound;

    [Header("メッセージ")]
    public ObjectEventSo characterDeadEvent;

    public int CurrentHP { get => hp.currentValue; set => hp.SetValue(value); }
    public int MaxHP { get => hp.maxValue; }
    protected Animator animator;
    public bool isDead;

    public GameObject buff;
    public GameObject debuff;

    //力量に関する
    public float basePower = 1f;
    private float powerEffect = 0.5f;

    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        hp.maxValue = maxHP;
        CurrentHP = MaxHP;
        buffRound.currentValue = 0;
        ReDefense();
    }

    protected virtual void Update()
    {
        animator.SetBool("isDead",isDead);
    }

    #region TakeDamage
    public virtual void TakeDamage(int damage)
    {
        // 実際のダメージ値を計算する：攻撃ダメージから防御値を引いた結果が0以上の場合はその値を使用し、0未満の場合は0にする（負のダメージを防ぐ）
        var currentDamage = (damage - defense.currentValue) >= 0 ? (damage - defense.currentValue) : 0;
        var currentDefense = (damage - defense.currentValue) >= 0 ? 0 : (defense.currentValue - damage);
        defense.SetValue(currentDefense);

        if (CurrentHP > currentDamage)
        {
            CurrentHP -= currentDamage;
            //Debug.Log("currentHP" + CurrentHP);
            animator.SetTrigger("hit");
        }
        else
        {
            CurrentHP = 0;
            //キャラクターが死亡する
            isDead = true;
            characterDeadEvent.RaisedEvent(this, this);
        }
    }
    #endregion


    public void UpdateDefense(int amount)
    {
        var value = defense.currentValue + amount;
        defense.SetValue(value);
    }

    public void ReDefense()
    {
        defense.SetValue(0);
    }

    public void HealHealth(int amount)
    {

        CurrentHP += amount;
        //CurrentHP = CurrentHP > MaxHP ? MaxHP : CurrentHP;
        CurrentHP = Mathf.Min(CurrentHP, MaxHP);
        buff.SetActive(true);
    }


    public void SetupPower(int round, bool isPositive)
    {
        if (isPositive)
        {
            float newPower = basePower + powerEffect;
            basePower = Mathf.Min(newPower, 1.5f);  //力量の重ね掛けを防ぐ
            buff.SetActive(true);
        }
        else
        {
            debuff.SetActive(true);
            basePower = 1 - powerEffect;
        }

        var currentRound = buffRound.currentValue + round;
        if (basePower == 1)
        {
            buffRound.SetValue(0);
        }
        else
        {
            buffRound.SetValue(currentRound);
        }
    }
    /// <summary>
    /// ターンの切り替え
    /// </summary>
    public void UpdatePowerRound()
    {
        buffRound.SetValue(buffRound.currentValue - 1);
        if (buffRound.currentValue <= 0)
        {
            buffRound.SetValue(0);
            basePower = 1;
        }

    }
}
