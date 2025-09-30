using System;
using UnityEngine;
using UnityEngine.Events;

public class BaseEventListener<T> : MonoBehaviour
{
    public BaseEventSo<T> eventSo;
    public UnityEvent<T> response;

    private void OnEnable()
    {
        if (eventSo != null)
            eventSo.OnEventRaised += OnEventRaised;

        
    }


    private void OnDisable()
    {
        if (eventSo != null)
            eventSo.OnEventRaised -= OnEventRaised;
    }
    private void OnEventRaised(T value)
    {
        response.Invoke(value);
    }
}
