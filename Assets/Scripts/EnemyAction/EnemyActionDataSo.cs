using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyActionDataSo", menuName = "Enemy/ EnemyActionDataSo")]
public class EnemyActionDataSo : ScriptableObject
{
    public List<EnemyAction> actions;
}

[System.Serializable]
public struct EnemyAction
{
    public Sprite intentSprite;
    public EffectBase effect;
}
