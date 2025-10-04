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
        //1�饦��ɽK����������륫�`�ɤθ�굱��
        canMove = false;
        canExecute = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!currentCard.isAvailable) return;   //���ͥ륮�`�����ʤ����Ϥϥ꥿�`�󤹤�


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
        if (!currentCard.isAvailable) return;   //���ͥ륮�`�����ʤ����Ϥϥ꥿�`�󤹤�

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
        if (!currentCard.isAvailable) return;   //���ͥ륮�`�����ʤ����Ϥϥ꥿�`�󤹤�

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
