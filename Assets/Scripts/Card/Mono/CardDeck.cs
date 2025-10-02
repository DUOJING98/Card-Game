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
        //����åե�
        ShuffleDeck();
    }
    [ContextMenu("Test")]
    public void TestDraw()
    {
        DrawCard(1);
    }

    /// <summary>
    /// ���`�ɤ�����
    /// </summary>
    /// <param name="amount">�����</param>
    private void DrawCard(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            CardDataSo currentCardData = drawDeck[0];
            drawDeck.RemoveAt(0);
            //�ɥ�`ɽ���˥��`�ɤ��ʤ����ϡ��Τ������饷��åե뤷�ƥɥ�`ɽ���ˑ���
            if (drawDeck.Count == 0)
            {
                foreach (var item in discardDeck)
                {
                    drawDeck.Add(item);
                }
                ShuffleDeck();
            }
            var card = cardManager.GetCardObject().GetComponent<Card>(); //�֤�����
            //���ڻ�
            card.Init(currentCardData);
            card.transform.position = deckPosition;

            handCardList.Add(card);
            var delay = i * 0.2f;
            SetCardLayout(delay);
        }

    }
    /// <summary>
    /// ���`�ɤΥ쥤�����Ȥ��O������
    /// </summary>
    /// <param name="delay">�W�ӕr�g</param>
    private void SetCardLayout(float delay)
    {
        for (int i = 0; i < handCardList.Count; i++)
        {
            Card currentCard = handCardList[i];
            CardTransform cardTransform = layoutManager.GetCardTransform(i, handCardList.Count);

            //currentCard.transform.SetPositionAndRotation(cardTransform.pos, cardTransform.rotation);

            //���`�ɤ������Ƥ���
            currentCard.isAnimating = true;

            currentCard.transform.DOScale(Vector3.one, 0.2f).SetDelay(delay).onComplete = () => //�����ЄI���K�����顢���δΤ��Ф��I�����
            {
                currentCard.transform.DOMove(cardTransform.pos, 0.6f).onComplete = () => currentCard.isAnimating = false;
                currentCard.transform.DORotateQuaternion(cardTransform.rotation, 0.5f);
            };

            //���`�ɤ΁K��혤��O��
            currentCard.GetComponent<SortingGroup>().sortingOrder = i;
            currentCard.UpdatePositionRotation(cardTransform.pos, cardTransform.rotation);
        }
    }


    private void ShuffleDeck()
    {
        discardDeck.Clear();
        //TODO:UI�α�ʾ������¤���

        for (int i = 0; i < drawDeck.Count; i++)
        {
            CardDataSo temp = drawDeck[i];
            int randomIndex = Random.Range(i, drawDeck.Count);
            drawDeck[i] = drawDeck[randomIndex];
            drawDeck[randomIndex] = temp;
        }
    }
   
    
    /// <summary>
    /// �Τ����Υ��å�
    /// </summary>
    /// <param name="card"></param>

    public void DiscardCard(Card card)
    {
        discardDeck.Add(card.cardData);
        handCardList.Remove(card);

        cardManager.DiscardCard(card.gameObject);

        SetCardLayout(0f);
    }
}
