using UnityEngine;

public class CharacterBse : MonoBehaviour
{
    public int maxHP;
    public IntVariable hp;
    public IntVariable defense;
    public int CurrentHP { get => hp.currentValue; set => hp.SetValue(value); }
    public int MaxHP { get => hp.maxValue; }
    protected Animator animator;
    public bool isDead;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        hp.maxValue = maxHP;
        CurrentHP = MaxHP;

        ReDefense();
    }

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
        }
        else
        {
            CurrentHP = 0;
            //キャラクターが死亡する
            isDead = true;
        }
    }
    public void UpdateDefense(int amount)
    {
        var value = defense.currentValue + amount;
        defense.SetValue(value);
    }

    public void ReDefense()
    {
        defense.SetValue(0);
    }

}
