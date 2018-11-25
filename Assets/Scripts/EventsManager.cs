using System;
using System.Collections.Generic;
using UnityEngine;

public class EventsManager : MonoBehaviour {

    public enum EventType {
        PlayerSpawn,
        PlayerDeath
    }

    private static Dictionary<EventType, Action<object>> handlers = new Dictionary<EventType, Action<object>>();

    public static void Subscribe(EventType eventType, Action<object> handler) {
        if (handlers.ContainsKey(eventType)) {
            handlers[eventType] += handler;
        } else {
            handlers[eventType] = handler;
        }
    }

    public static void Unsubscribe(EventType eventType, Action<object> handler) {
        if (handlers.ContainsKey(eventType)) {
            handlers[eventType] -= handler;
        }
    }

    public static void SendEvent(EventType eventType, object arg) {
        if (handlers.ContainsKey(eventType)) {
            handlers[eventType](arg);
        }
    }
}
