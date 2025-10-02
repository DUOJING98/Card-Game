using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="CardDataSo",menuName ="Card/CardDatoSo")]
public class CardDataSo : ScriptableObject
{
    public string cardName;
    public Sprite cardImage;

    public int cost;

    public CardType cardType;
    [TextArea]
    public string description;

    //ŒgÐÐ¤µ¤ì¤¿ŒgëH¤Î„¿¹û
    public List<EffectBase> effects;

}
