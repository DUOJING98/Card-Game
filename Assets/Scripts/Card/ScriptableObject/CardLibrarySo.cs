using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName ="CardLibrarySo",menuName ="Card/CardLibrarySo")]
public class CardLibrarySo : ScriptableObject
{
    public List<CardLibraryEntry> cardLibraryList;
}

[System.Serializable]
public struct CardLibraryEntry
{
    public CardDataSo cardData;
    public int amount;
}

