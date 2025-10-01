using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.Rendering;
using DG.Tweening;

public class CardDeck : MonoBehaviour
{
    public CardManager cardManager;
    public CardLayoutManager layoutManager;

    public Vector3 deckPosition;
    private List<CardDataSo> drawDeck = new();      //ɽ��������
    private List<CardDataSo> discardDeck = new();   //�Τ�����ɽ

    private List<Card> handCardList = new();        //����(�����`��)


    private void Start()
    {           //Test
        InitializeDeck();

        DrawCard(3);
    }
    public void InitializeDeck()
    {
        drawDeck.Clear();
        foreach (var entry in cardManager.currentLibrary.cardLibraryList)
        {
            for (int i = 0; i < entry.amount; i++)
            {
                drawDeck.Add(entry.cardData);
            }
        }

        //TODO:����åե�
    }
    [ContextMenu("Test")]
    public void TestDraw()
    {
        DrawCard(1);
    }


    private void DrawCard(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            if (drawDeck.Count == 0)
            {
                //����åե�
            }
            CardDataSo currentCardData = drawDeck[0];
            drawDeck.RemoveAt(0);
            var card = cardManager.GetCardObject().GetComponent<Card>(); //�֤�����
            //���ڻ�
            card.Init(currentCardData);
            card.transform.position = deckPosition;

            handCardList.Add(card);
            var delay = i * 0.2f;
            SetCardLayout(delay);
        }

    }

    private void SetCardLayout(float delay)
    {
        for (int i = 0; i < handCardList.Count; i++)
        {
            Card currentCard = handCardList[i];
            CardTransform cardTransform = layoutManager.GetCardTransform(i, handCardList.Count);

            //currentCard.transform.SetPositionAndRotation(cardTransform.pos, cardTransform.rotation);

            
            currentCard.transform.DOScale(Vector3.one, 0.2f).SetDelay(delay).onComplete = () => //�����ЄI���K�����顢���δΤ��Ф��I�����
            {
                currentCard.transform.DOMove(cardTransform.pos, 0.6f);
                currentCard.transform.DORotateQuaternion(cardTransform.rotation, 0.5f);
            };

            //���`�ɤ΁K��혤��O��
            currentCard.GetComponent<SortingGroup>().sortingOrder = i;
        }
    }
}
