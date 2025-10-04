using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Panel")]
    public GameObject gameplayPanel;
    public GameObject gameWInPanel;
    public GameObject gameOverPanel;


    public void OnLoadRoomEvent(object data)
    {
        Room currentRoom = (Room)data;
        switch (currentRoom.roomData.roomType)
        {
            case RoomType.MinorEnemy:
            case RoomType.EliteEnemy:
            case RoomType.Boss:
                gameplayPanel.SetActive(true);
                break;
            case RoomType.Shop:
                break;
            case RoomType.Treasure:
                break;
            case RoomType.RestRoom:
                break;
        }
    }

    /// <summary>
    /// Load map/load menu
    /// </summary>

    public void HideAllPanels()
    {
        gameplayPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        gameWInPanel.SetActive(false);
    }

    public void OnGameWinEvent()
    {
        gameWInPanel.SetActive(true);
        gameplayPanel.SetActive(false);
    }

    public void OnGameOverEvent()
    {
        gameOverPanel.SetActive(true);
        gameplayPanel.SetActive(false);
    }
}
