using UnityEngine;

[CreateAssetMenu(fileName ="DefenseEffect",menuName ="Card Effect/DefenseEffect")]
public class DefenseEffect : EffectBase
{
    public override void Execute(CharacterBse from, CharacterBse target)
    {
        if(targetType == EffectTargetType.Self)
        {
            from.UpdateDefense(value);
        }

        if(targetType == EffectTargetType.Target)
        {
            target.UpdateDefense(value);
        }
    }
}
