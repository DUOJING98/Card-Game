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

    //�g�Ф��줿�g�H�΄���
    public List<EffectBase> effects;

}
