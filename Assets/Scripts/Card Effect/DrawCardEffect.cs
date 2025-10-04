using UnityEngine;

[CreateAssetMenu(fileName = "DrawCardEffect", menuName = "Card Effect/ DrawCardEffect")]
public class DrawCardEffect : EffectBase
{
    public IntEventSo drawCardEvent;
    public override void Execute(CharacterBse from, CharacterBse target)
    {
        drawCardEvent?.RaisedEvent(value,this);
    }
}
