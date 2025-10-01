using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CardLayoutManager : MonoBehaviour
{
    public bool isHorizontal;
    public float maxWidth = 7;
    public float cardSpacing = 2;
    public Vector3 centerPoint;

    [SerializeField] List<Vector3> cardPos = new();
    [SerializeField] List<Quaternion> cardRotation = new();

    public CardTransform GetCardTransform(int index, int totalCards)
    {
        CalculatePos(totalCards, isHorizontal);
        return new CardTransform(cardPos[index], cardRotation[index]);
    }

    private void CalculatePos(int number, bool horizontal)
    {
        //リセット
        cardPos.Clear();
        cardRotation.Clear();

        if (horizontal)
        {
            float currentWidth = cardSpacing * (number - 1);
            float totalWidth = Mathf.Min(currentWidth, maxWidth);

            float currentSpacing = totalWidth > 0 ? totalWidth / (number - 1) : 0;

            for (int i = 0; i < number; i++)
            {
                float xPos = 0 - (totalWidth / 2) + (i * currentSpacing);

                var pos = new Vector3(xPos, centerPoint.y, 0);

                var rotation = Quaternion.identity;

                cardPos.Add(pos);
                cardRotation.Add(rotation);
            }
        }
    }



}
