using System;
using UnityEngine;
using UnityEngine.UIElements;

public class GameWinPanel : MonoBehaviour
{
    private VisualElement element;
    private Button pickCardButton;
    private Button backToMapButton;

    [Header("Event")]

    public ObjectEventSo loadMapEvent;
    public ObjectEventSo pickCardEvent;

    private void Awake()
    {
        element = GetComponent<UIDocument>().rootVisualElement;
        pickCardButton = element.Q<Button>("PickCardButton");
        backToMapButton = element.Q<Button>("BackToMapButton");

        backToMapButton.clicked += OnBackToMapButtonClicked;
        pickCardButton.clicked += OnPickCardButtonClicked;
    }

    private void OnPickCardButtonClicked()
    {
        pickCardEvent.RaisedEvent(null,this);
    }

    private void OnBackToMapButtonClicked()
    {
        loadMapEvent.RaisedEvent(null,this);
    }
}
