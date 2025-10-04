using Unity.Mathematics;
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
                var damage = (int)math.round(value * from.basePower);
                target.TakeDamage(damage);
                Debug.Log($"÷¥––¡À{damage}µ„…À∫¶!");
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
