using System;
using UnityEngine;
using UnityEngine.UIElements;

public class RoomRestPanel : MonoBehaviour
{
    private VisualElement visualElement;

    private Button restButton, backToMapButton;

    public EffectBase resEffect;
    public ObjectEventSo loadMapEvent;

    private CharacterBse player;

    private void OnEnable()
    {
        visualElement = GetComponent<UIDocument>().rootVisualElement;
        restButton = visualElement.Q<Button>("Rest");
        backToMapButton = visualElement.Q<Button>("Back");

        player = FindAnyObjectByType<Player>(FindObjectsInactive.Include);
        restButton.clicked += OnRestButtonClicked;
        backToMapButton.clicked += OnBackToMapButtonClicked;
    }

    private void OnBackToMapButtonClicked()
    {
        loadMapEvent.RaisedEvent(null, this);
    }

    private void OnRestButtonClicked()
    {
        resEffect.Execute(player, null);
        restButton.SetEnabled(false);
    }
}
