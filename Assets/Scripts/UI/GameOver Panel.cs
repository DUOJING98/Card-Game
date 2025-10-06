using System;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UIElements.Button;

public class GameOverPanel : MonoBehaviour
{
    private Button button;
    public ObjectEventSo loadMenuEvent;

    private void OnEnable()
    {
        GetComponent<UIDocument>().rootVisualElement.Q<Button>("BackToTitle").clicked += BackToTitle;
    }

    private void BackToTitle()
    {
        loadMenuEvent.RaisedEvent(null, this);
    }
}
