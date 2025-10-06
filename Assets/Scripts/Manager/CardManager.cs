using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System;
using NUnit.Framework.Interfaces;

public class CardManager : MonoBehaviour
{
    public PoolTool poolTool;

    public List<CardDataSo> cardDataList; //���`���Х��`�ɥǩ`��

    [Header("���`�ɥꥹ��")]
    public CardLibrarySo newGameLibrary;//new���`��ʼ�ޤ�r���ڻ��Υ��`��
    public CardLibrarySo currentLibrary;//��Υ��`�ɥꥹ��


    private int previousIndex;

    private void Awake()
    {
        InitializeCardDataList();
        foreach (var item in newGameLibrary.cardLibraryList)
        {
            currentLibrary.cardLibraryList.Add(item);
        }
    }

    private void OnDisable()
    {
        currentLibrary.cardLibraryList.Clear();
    }




    private void InitializeCardDataList()
    {
        Addressables.LoadAssetsAsync<CardDataSo>("CardData", null).Completed += OnCardDataLoaded;
    }

    private void OnCardDataLoaded(AsyncOperationHandle<IList<CardDataSo>> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            cardDataList = new List<CardDataSo>(handle.Result);
        }
        else
        {
            Debug.LogError("NO CardData Found!");
        }
    }
    /// <summary>
    /// �鿨ʱ���õĿ��ƻ��GameObject
    /// </summary>

    public GameObject GetCardObject()
    {
        var cardObj = poolTool.GetObjectFromPool();
        cardObj.transform.localScale = Vector3.zero;
        return cardObj;
    }

    public void DiscardCard(GameObject cardObj)
    {
        poolTool.ReturnObjectToPool(cardObj);
    }


    public CardDataSo GetNewCardData()
    {
        var randomIndex = 0;
        do
        {
            randomIndex = UnityEngine.Random.Range(0, cardDataList.Count);
        } while (previousIndex == randomIndex);

        previousIndex = randomIndex;
        return cardDataList[randomIndex];
    }
    /// <summary>
    /// ����¿���
    /// </summary>
    /// <param name="newCardData"></param>

    public void AddNewCard(CardDataSo newCardData)
    {
        var newCard = new CardLibraryEntry
        {
            cardData = newCardData,
            amount = 1
        };
        if (currentLibrary.cardLibraryList.Contains(newCard))
        {
            var target = currentLibrary.cardLibraryList.Find(t => t.cardData == newCardData);
            target.amount++;
        }
        else
        {
            currentLibrary.cardLibraryList.Add(newCard);
        }
    }
}
