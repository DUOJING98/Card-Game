using UnityEngine;

[CreateAssetMenu(fileName = "DamageEffect", menuName = "Card Effect/DamageEffect")]
public class DamageEffect : EffectBase
{
    public override void Execute(CharacterBse from, CharacterBse target)
    {
        if (target == null) return;
        switch (targetType)
        {
            case EffectTargetType.Target:
                target.TakeDamage(value);
                Debug.Log($"÷¥––¡À{value}µ„…À∫¶!");
                break;
            case EffectTargetType.All:
                foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
                {
                    enemy.GetComponent<CharacterBse>().TakeDamage(value);
                }
                break;
        }
    }
}
