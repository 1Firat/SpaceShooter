using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public static class GameEvent
{
    public delegate void EventDelegate(EventGame value);

    private static List<EventDelegate> listeners = new List<EventDelegate>();

    public static void Raise(EventGame eventData)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i](eventData);
        }
    }

    public static void RegisterListener(EventDelegate listener)
    {
        listeners.Add(listener);
    }

    public static void UnregisterListener(EventDelegate listener)
    {
        listeners.Remove(listener);
    }
}
