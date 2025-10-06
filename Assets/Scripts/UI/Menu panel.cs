
using UnityEngine;
using UnityEngine.UIElements;

public class Menupanel : MonoBehaviour
{
    private VisualElement visualElement;

    private Button newGameButton;
    private Button quitGameButton;

    public ObjectEventSo newGameEvent;

    private void OnEnable()
    {
        visualElement = GetComponent<UIDocument>().rootVisualElement;
        newGameButton = visualElement.Q<Button>("Start");
        quitGameButton = visualElement.Q<Button>("Quit");

        newGameButton.clicked += OnNewGameButtonClicked;
        quitGameButton.clicked += OnQuitButtonClicked;
    }

    private void OnQuitButtonClicked()
    {
        Application.Quit();
    }

    private void OnNewGameButtonClicked()
    {
        newGameEvent.RaisedEvent(null, this);
    }

}
