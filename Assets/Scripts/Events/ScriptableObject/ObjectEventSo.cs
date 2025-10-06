using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectEventSo", menuName = "Event/ObjectEventSo")]
public class ObjectEventSo : BaseEventSo<object>
{
    public Action<object, object> OnRaised { get; internal set; }
}
