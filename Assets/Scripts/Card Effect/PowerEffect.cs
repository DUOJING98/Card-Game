using UnityEngine;


[CreateAssetMenu(fileName = "PowerEffect", menuName = "Card Effect/ PowerEffect")]
public class PowerEffect : EffectBase
{
    public override void Execute(CharacterBse from, CharacterBse target)
    {
        switch (targetType)
        {
            case EffectTargetType.Self:
                from.SetupPower(value, true);
                break;
                
            case EffectTargetType.Target:
                target.SetupPower(value, false);
                break;
        }
    }
}
