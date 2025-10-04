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

    [Header("���٥��")]
    public IntEventSo drawCountEvent;
    public IntEventSo discardCountEvent;


    private void Start()
    {
        InitializeDeck();


    }

    /// <summary>
    /// ���ڻ�
    /// </summary>
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

        ShuffleDeck();              //����åե�
    }


    [ContextMenu("Test")]
    public void TestDraw()
    {
        DrawCard(1);
    }



    public void NewTurnDrawCard()
    {
        DrawCard(4);
    }

    /// <summary>
    /// ���`�ɤ�����
    /// </summary>
    /// <param name="amount">�����</param>
    private void DrawCard(int amount)
    {
        for (int i = 0; i < amount; i++)
        {


            //�ɥ�`ɽ���˥��`�ɤ��ʤ����ϡ��Τ������饷��åե뤷�ƥɥ�`ɽ���ˑ���
            if (drawDeck.Count == 0)
            {
                foreach (var item in discardDeck)
                {
                    drawDeck.Add(item);
                }
                ShuffleDeck();
            }
            CardDataSo currentCardData = drawDeck[0];
            drawDeck.RemoveAt(0);
            //UI����������¤���
            drawCountEvent.RaisedEvent(drawDeck.Count, this);
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


            //���`�ɤΥ��ͥ륮�`�ж�
            currentCard.UpdateCardState();


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
        //UI�α�ʾ������¤���
        drawCountEvent.RaisedEvent(drawDeck.Count, this);
        discardCountEvent.RaisedEvent(discardDeck.Count, this);

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

    public void DiscardCard(object obj)
    {
        Card card = obj as Card;

        discardDeck.Add(card.cardData); //�Τ����˼Ӥ���
        handCardList.Remove(card);  //�����ꥹ�Ȥ�����������

        cardManager.DiscardCard(card.gameObject);      ////������؅�����
        discardCountEvent.RaisedEvent(discardDeck.Count, this); //�Τ�����֪ͨ���ơ��Τ�����ö������¤���
        SetCardLayout(0f);
    }

    public void OnPlayerTurnEnd()
    {
        //�ץ쥤��`�Υ��`�󤬽K�ˤ����r�����٤Ƥ�������؅�����
        for (int i = 0; i < handCardList.Count; i++)  //�F�ڤΤ��٤Ƥ��������`�פ���
        {
            discardDeck.Add(handCardList[i].cardData);  //�F�ڤ�������Τ����˼Ӥ���
            cardManager.DiscardCard(handCardList[i].gameObject);    //������؅�����
        }
        handCardList.Clear();   //���٤Ƥ������򥯥ꥢ����
        discardCountEvent.RaisedEvent(discardDeck.Count, this); //�Τ�����֪ͨ���ơ��Τ�����ö������¤���
    }
}
