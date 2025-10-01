using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Card : MonoBehaviour
{
    [Header("Item")]
    public TextMeshPro costText,descriptionText,typeText;
    public SpriteRenderer cardSprite;
    public CardDataSo cardData;


    private void Start()
    {
        Init(cardData);
    }

    public void Init(CardDataSo data)
    {
        cardData = data;
        cardSprite.sprite =data.cardImage ;
        costText.text = data.cost.ToString() ;
        descriptionText.text = data.description;
        typeText.text = data.cardType switch
        {
            CardType.Attack => "¹¥“Ä",
            CardType.Defense => "¥¹¥­¥ë",
            CardType.Abilities => "ÄÜÁ¦",
            _ => throw new System.NotImplementedException()
        };
    }
}
