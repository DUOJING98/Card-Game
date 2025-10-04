using UnityEngine;


[CreateAssetMenu(fileName = "HealEffect", menuName = "Card Effect/HealEffect")]

public class HealEffect : EffectBase
{
    public override void Execute(CharacterBse from, CharacterBse target)
    {
        if (targetType == EffectTargetType.Self)
        {
            from.HealHealth(value);
        }

        if (targetType == EffectTargetType.Target)
        {
            target.HealHealth(value);
        }
    }
}
