using NUnit.Framework;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Card currentCard;
    private bool canMove;
    private bool canExecute;
    public GameObject ArrowPrefab;
    private GameObject currentArrow;

    private CharacterBse targetCharacter;
    private void Awake()
    {
        currentCard = GetComponent<Card>();

    }
    private void OnDisable()
    {
        //1¥é¥¦¥ó¥É½KÁËáá¤ËÏû¤¨¤ë¥«©`¥É¤Î¸î¤êµ±¤Æ
        canMove = false;
        canExecute = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        switch (currentCard.cardData.cardType)
        {
            case CardType.Attack:
                currentArrow = Instantiate(ArrowPrefab,transform.position,Quaternion.identity);
                break;
            case CardType.Defense:
            case CardType.Abilities:
                canMove = true;
                break;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (canMove)
        {
            currentCard.isAnimating = true;
            Vector3 screenPos = new(Input.mousePosition.x, Input.mousePosition.y, 10);
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);

            currentCard.transform.position = worldPos;
            canExecute = worldPos.y > 0;
        }
        else
        {
            if (eventData.pointerEnter == null) return;

            if (eventData.pointerEnter.CompareTag("Enemy"))
            {
                canExecute = true;
                targetCharacter = eventData.pointerEnter.GetComponent<CharacterBse>();
                return;
            }
            canExecute = false;
            targetCharacter = null;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(currentArrow != null)
        {
            Destroy(currentArrow);
        }

        if (canExecute)
        {
            currentCard.ExecuteCardEffect(currentCard.player, targetCharacter);
        }
        else
        {
            currentCard.RestCardTransform();
        }

    }
}
