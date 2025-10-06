using System.ComponentModel;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
    public FadePanel fadePanel;
    private AssetReference currenScene;

    public AssetReference map;
    public AssetReference menu;

    private Room currentRoom;
    private Vector2Int currenRoomVector;
    [Header("Event")]
    public ObjectEventSo afterLoadRoomEvent;
    public ObjectEventSo updateRoomEvent;


    private void Awake()
    {
        LoadMenu();
        currenRoomVector = Vector2Int.one * -1;
    }
    public async void OnLoadRoomEvent(object data)
    {
        if (data is Room)
        {
            currentRoom = data as Room;

            var currentData = currentRoom.roomData;
            currenRoomVector = new(currentRoom.column, currentRoom.line);

            currenScene = currentData.sceneToLoad;

            //Debug.Log(currentData.roomType);
        }

        await UnLoadSceneTask();

        await LoadSceneTask();

        afterLoadRoomEvent.RaisedEvent(currentRoom, this);
    }

    private async Awaitable LoadSceneTask()
    {
        var s = currenScene.LoadSceneAsync(LoadSceneMode.Additive);
        await s.Task;

        if (s.Status == AsyncOperationStatus.Succeeded)
        {
            fadePanel.FadeOut(0.2f);
            SceneManager.SetActiveScene(s.Result.Scene);
        }
    }

    private async Awaitable UnLoadSceneTask()
    {
        fadePanel.FadeIn(0.4f);
        await Awaitable.WaitForSecondsAsync(0.45f);
        await Awaitable.FromAsyncOperation(SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene()));
    }

    public async void LoadMap()
    {
        await UnLoadSceneTask();
        if (currenRoomVector != Vector2.one * -1)
        {
            updateRoomEvent.RaisedEvent(currenRoomVector, this);
        }
        currenScene = map;
        await LoadSceneTask();
    }

    public async void LoadMenu()
    {
        if (currenScene != null)
            await UnLoadSceneTask();

        currenScene = menu;
        await LoadSceneTask();
    }
}
