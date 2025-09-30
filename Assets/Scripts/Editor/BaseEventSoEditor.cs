using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BaseEventSo<>))]
public class BaseEventSoEditor<T> : Editor
{
    private BaseEventSo<T> baseEventSo;

    private void OnEnable()
    {
        if (baseEventSo == null)
            baseEventSo = target as BaseEventSo<T>;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.LabelField("subÊý£º"+GetListener().Count);

        foreach(var listener in GetListener())
        {
            EditorGUILayout.LabelField(listener.ToString());
        }
    }

    private List<MonoBehaviour> GetListener()
    {
        List<MonoBehaviour> listeners = new();

        if(baseEventSo == null || baseEventSo.OnEventRaised == null)
        {
            return listeners;
        }

        var sub = baseEventSo.OnEventRaised.GetInvocationList();

        foreach ( var listener in sub)
        {
            var obj = listener.Target as MonoBehaviour;

            if (!listeners.Contains(obj))
            {
                listeners.Add(obj);
            }
        }

        return listeners;
    }


}
