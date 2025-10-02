using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CardLayoutManager : MonoBehaviour
{
    public bool isHorizontal;
    public float maxWidth = 7;
    public float cardSpacing = 2;

    [Header("說及由仿丟奈正")]
    public float angleBetweenCard = 7f;
    public float radius = 17f;

    public Vector3 centerPoint;

    [SerializeField] List<Vector3> cardPos = new();
    [SerializeField] List<Quaternion> cardRotation = new();


    private void Awake()
    {
        centerPoint = isHorizontal ? Vector3.up * 4.5f : Vector3.up * -21.5f;
    }

    public CardTransform GetCardTransform(int index, int totalCards)
    {
        CalculatePos(totalCards, isHorizontal);
        return new CardTransform(cardPos[index], cardRotation[index]);
    }

    private void CalculatePos(int number, bool horizontal)
    {
        //伉本永玄
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
        else
        {
            float cardAngle = (number - 1) * angleBetweenCard / 2;

            for (int i = 0; i < number; i++)
            {
                var pos = FanCardPosition(cardAngle - i * angleBetweenCard);

                var rotation = Quaternion.Euler(0, 0, cardAngle - i * angleBetweenCard);

                cardPos.Add(pos);
                cardRotation.Add(rotation);
            }

        }



    }

    private Vector3 FanCardPosition(float angle)
    {
        return new Vector3(
            centerPoint.x - Mathf.Sin(Mathf.Deg2Rad * angle) * radius,
            centerPoint.y + Mathf.Cos(Mathf.Deg2Rad * angle) * radius,
            0
            );
    }

}
