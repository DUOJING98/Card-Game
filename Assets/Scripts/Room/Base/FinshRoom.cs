using UnityEngine;

public class FinshRoom : MonoBehaviour
{
    public ObjectEventSo loadMapEvent;
    private void OnMouseDown()
    {
        loadMapEvent.RaisedEvent(null, this);
    }
}
