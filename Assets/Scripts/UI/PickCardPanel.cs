using UnityEngine;
using UnityEngine.UIElements;

public class PickCardPanel : MonoBehaviour
{
    private VisualElement visualElement;
    public VisualTreeAsset cardTemplate;
    private VisualElement cardContainer;
    private CardDataSo currentCardData;


    private void OnEnable()
    {
        visualElement = GetComponent<UIDocument>().rootVisualElement;
        cardContainer = visualElement.Q<VisualElement>("Container");

        for(int i = 0; i < 3; i++)
        {
            var card = cardTemplate.Instantiate();
            card.style.height = 320;
            cardContainer.Add(card);
        }
    }
}
