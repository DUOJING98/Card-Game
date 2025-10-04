using UnityEngine;

[CreateAssetMenu(fileName ="IntVariable",menuName = "Variable/IntVariable")]
public class IntVariable : ScriptableObject
{
    public int maxValue;
    public int currentValue;

    public IntEventSo valueChangeEvent;

    [TextArea]
    [SerializeField] string description;
    public void SetValue(int value)
    {
        currentValue = value;
        valueChangeEvent?.RaisedEvent(value,this);
    }
}
