using UnityEngine;

[CreateAssetMenu(fileName = "DrawCardEffect", menuName = "Card Effect/ DrawCardEffect")]
public class DrawCardEffect : EffectBase
{
    public IntEventSo drawCardEvent;
    public override void Execute(CharacterBse from, CharacterBse target)
    {
        Debug.Log($"[DrawCardEffect] Execute called! value={value}, event={(drawCardEvent != null)}");
        drawCardEvent?.RaisedEvent(value, this);
    }
}
