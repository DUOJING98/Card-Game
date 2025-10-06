using UnityEngine;
using UnityEngine.EventSystems;

public class TreasureButton : MonoBehaviour, IPointerDownHandler
{
    public ObjectEventSo gameWinEvent;

    public void OnPointerDown(PointerEventData eventData)
    {
        gameWinEvent.RaisedEvent(null, this);
    }
}
