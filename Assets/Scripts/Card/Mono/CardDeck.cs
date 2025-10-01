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
    private List<CardDataSo> drawDeck = new();      //山札を引く
    private List<CardDataSo> discardDeck = new();   //捨て札の山

    private List<Card> handCardList = new();        //手札(毎ターン)


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

        //TODO:シャッフル
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
                //シャッフル
            }
            CardDataSo currentCardData = drawDeck[0];
            drawDeck.RemoveAt(0);
            var card = cardManager.GetCardObject().GetComponent<Card>(); //手に入れる
            //初期化
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

            
            currentCard.transform.DOScale(Vector3.one, 0.2f).SetDelay(delay).onComplete = () => //この行処理し終えたら、その次の行が処理される
            {
                currentCard.transform.DOMove(cardTransform.pos, 0.6f);
                currentCard.transform.DORotateQuaternion(cardTransform.rotation, 0.5f);
            };

            //カードの並び順の設定
            currentCard.GetComponent<SortingGroup>().sortingOrder = i;
        }
    }
}
