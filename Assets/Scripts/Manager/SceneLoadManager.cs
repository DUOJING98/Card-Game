using System.ComponentModel;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
    private AssetReference currenScene;
    public AssetReference map;
    private Room currentRoom;
    private Vector2Int currenRoomVector;
    [Header("Event")]
    public ObjectEventSo afterLoadRoomEvent;
    public ObjectEventSo updateRoomEvent;


    private void Start()
    {
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
            SceneManager.SetActiveScene(s.Result.Scene);
        }
    }

    private async Awaitable UnLoadSceneTask()
    {
        await SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
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

}
