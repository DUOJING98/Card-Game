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
        //1ラウンド終了後に消えるカードの割り当て
        canMove = false;
        canExecute = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!currentCard.isAvailable) return;   //エネルギーが足りない場合はリターンする


        switch (currentCard.cardData.cardType)
        {
            case CardType.Attack:
                currentArrow = Instantiate(ArrowPrefab, transform.position, Quaternion.identity);
                break;
            case CardType.Defense:
            case CardType.Abilities:
                canMove = true;
                break;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!currentCard.isAvailable) return;   //エネルギーが足りない場合はリターンする

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
        if (!currentCard.isAvailable) return;   //エネルギーが足りない場合はリターンする

        if (currentArrow != null)
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
