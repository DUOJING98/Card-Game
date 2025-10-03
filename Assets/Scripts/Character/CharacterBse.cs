using UnityEngine;

public class CharacterBse : MonoBehaviour
{
    public int maxHP;
    public IntVariable hp;
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
    }

    public virtual void TakeDamage(int damage)
    {
        if (CurrentHP > damage)
        {
            CurrentHP -= damage;
            Debug.Log("currentHP" + CurrentHP);
        }
        else
        {
            CurrentHP = 0;
            //•≠•„•È•Ø•ø©`§¨À¿Õˆ§π§Î
            isDead = true;
        }
    }


}
