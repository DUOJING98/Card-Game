using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="MapConfigSo",menuName ="Map/MapConfigSo")]

public class MapConfigSo:ScriptableObject
{
    public List<RoomBlueprint> roomBlueprints;
}

[System.Serializable]
public class RoomBlueprint
{
    public int min, max;
    public RoomType roomType;
}
