using UnityEngine;

public abstract class EffectBase : ScriptableObject
{
    public int value;

    public EffectTargetType targetType;

    public abstract void Execute(CharacterBse from, CharacterBse target);
}
