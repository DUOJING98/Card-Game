using UnityEngine;

public class SceneLoadManager : MonoBehaviour
{
  public void OnLoadRoomEvent(object data)
    {
        if(data is RoomDataSo)
        {
            var currentData = (RoomDataSo)data;

            Debug.Log(currentData.roomType);
        }
    }
}
