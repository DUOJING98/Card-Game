using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PickCardPanel : MonoBehaviour
{
    public CardManager cardManager;
    private VisualElement visualElement;
    public VisualTreeAsset cardTemplate;
    private VisualElement cardContainer;
    private CardDataSo currentCardData;
    private Button confirmButton;

    [Header("Event")]
    public ObjectEventSo finishPickCardEvent;

    private List<Button> cardButtons = new();

    private void OnEnable()
    {
        visualElement = GetComponent<UIDocument>().rootVisualElement;
        cardContainer = visualElement.Q<VisualElement>("Container");
        confirmButton = visualElement.Q<Button>("ConfirmButton");

        confirmButton.clicked += OnConfirmButtonClicked;


        for (int i = 0; i < 3; i++)
        {
            var card = cardTemplate.Instantiate();
            var data = cardManager.GetNewCardData();
            //初始化
            InitCard(card, data);
            //card.style.height = 320;
            var cardButton = card.Q<Button>("Card");

            cardContainer.Add(card);
            cardButtons.Add(cardButton);

            cardButton.clicked += () => OnCardClicked(cardButton, data);
        }
    }

    private void OnConfirmButtonClicked()
    {
        cardManager.AddNewCard(currentCardData);
        finishPickCardEvent.RaisedEvent(null, this);
    }

    private void OnCardClicked(Button cardButton, CardDataSo data)
    {
        currentCardData = data;
        for (int i = 0; i < cardButtons.Count; i++)
        {
            if (cardButtons[i] == cardButton)
                cardButtons[i].SetEnabled(false);
            else
                cardButtons[i].SetEnabled(true);
        }
        //Debug.Log("Card Clicked:" + currentCardData.name);
    }

    public void InitCard(VisualElement card, CardDataSo cardData)
    {
        card.dataSource = cardData;

        var cardSpriteElement = card.Q<VisualElement>("CardSprite");
        var cardCost = card.Q<Label>("EnergyCost");
        var cardDescription = card.Q<Label>("CardDescription");
        var cardType = card.Q<Label>("CardType");
        var cardName = card.Q<Label>("CardName");

        cardSpriteElement.style.backgroundImage = new StyleBackground(cardData.cardImage);
        cardName.text = cardData.cardName;
        cardCost.text = cardData.cost.ToString();
        cardDescription.text = cardData.description.ToString();



        cardType.text = cardData.cardType switch
        {
            CardType.Attack => "攻撃",
            CardType.Defense => "スキル",
            CardType.Abilities => "能力",
            _ => throw new System.NotImplementedException()
        };
    }
}
