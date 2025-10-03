using UnityEngine;
using UnityEngine.UIElements;

public class GameplayPanel : MonoBehaviour
{
    private VisualElement rootElement;
    private Label energyAmount, drawAmount, discardAmount, turnLabel;
    private Button endTurnButton;
    private void OnEnable()
    {
        rootElement = GetComponent<UIDocument>().rootVisualElement;

        energyAmount = rootElement.Q<Label>("EnergyAmount");
        drawAmount = rootElement.Q<Label>("DrawAmount");
        discardAmount = rootElement.Q<Label>("DiscardAmount");
        turnLabel = rootElement.Q<Label>("TurnLabel");
        endTurnButton = rootElement.Q<Button>("EndTurn");
        energyAmount.text = "0";
        drawAmount.text = "0";
        discardAmount.text = "0";
        turnLabel.text = "•≤©`•‡È_ º";

    }

    public void UpdateDrawDeckAmount(int amount)
    {
        drawAmount.text = amount.ToString();
    }
    public void UpdateDiscardDeckAmount(int amount)
    {
        discardAmount.text = amount.ToString();
    }
}
