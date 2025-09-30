using UnityEngine;
using UnityEngine.Events;

public class BaseEventSo<T> : ScriptableObject
{
    [TextArea]
    public string description;

    public UnityAction<T> OnEventRaised;

    public string lastSender;

    public void RaisedEvent(T value,object sender)
    {
        OnEventRaised?.Invoke(value);
        lastSender = sender.ToString(); 
    }
}
