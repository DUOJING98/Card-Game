using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;


public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Item")]
    public TextMeshPro costText, descriptionText, typeText;
    public SpriteRenderer cardSprite;
    public CardDataSo cardData;


    [Header("Ԫ�Υǩ`��")]
    public Vector3 originalPosition;
    public Quaternion originalRotation;
    public int originalLayerOrder;

    public bool isAnimating;
    public Player player;

    [Header("���٥��")]
    public ObjectEventSo discardCardEvent;

    private void Start()
    {
        Init(cardData);
    }

    public void Init(CardDataSo data)
    {
        cardData = data;
        cardSprite.sprite = data.cardImage;
        costText.text = data.cost.ToString();
        descriptionText.text = data.description;
        typeText.text = data.cardType switch
        {
            CardType.Attack => "����",
            CardType.Defense => "������",
            CardType.Abilities => "����",
            _ => throw new System.NotImplementedException()
        };

        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    public void UpdatePositionRotation(Vector3 position, Quaternion rotation)
    {
        originalPosition = position;
        originalRotation = rotation;
        originalLayerOrder = GetComponent<SortingGroup>().sortingOrder;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        if (isAnimating) return; //�ƄӤ��Ƥ���r�ˤ��ж����ʤ�

        transform.position = originalPosition + Vector3.up;
        transform.rotation = Quaternion.identity;
        GetComponent<SortingGroup>().sortingOrder = 20;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isAnimating) return;//�ƄӤ��Ƥ���r�ˤ��ж����ʤ�
        RestCardTransform();
    }

    public void RestCardTransform()
    {
        transform.SetPositionAndRotation(originalPosition, originalRotation);
        GetComponent<SortingGroup>().sortingOrder = originalLayerOrder;
        isAnimating = false;
    }

    public void ExecuteCardEffect(CharacterBse from, CharacterBse target)
    {
        //TODO:���ꤹ�����M��p�餹,���`�ɤλ؅���֪ͨ����
        discardCardEvent.RaisedEvent(this, this);

        foreach (var eff in cardData.effects)
        {
            eff.Execute(from, target);
        }
    }
}
