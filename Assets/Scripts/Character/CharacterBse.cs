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
        // �g�H�Υ���`������Ӌ�㤹�룺���ĥ���`��������������������Y����0���ϤΈ��ϤϤ��΂���ʹ�ä���0δ���Έ��Ϥ�0�ˤ��루ؓ�Υ���`���������
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
            //����饯���`����������
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
