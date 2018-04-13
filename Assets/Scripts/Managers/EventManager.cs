using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public enum EventType {
    USER_CONFIRMED,
    CARD_READ,
    TRANSACTION_REQUEST_SUCCESS
}

[System.Serializable]
public class ProgramEvent : UnityEvent<object> {
}

public class EventManager : MonoBehaviour {

    private Dictionary<EventType, ProgramEvent> eventDictionary;

    private static EventManager eventManager;

    public static EventManager instance {
        get {
            if (!eventManager) {
                eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

                if (!eventManager) {
                    Debug.LogError("There needs to be one active EventManger script, on a GameObject in the scene.");
                }
                else {
                    eventManager.Init();
                }
            }

            return eventManager;
        }
    }

    void Init() {
        if (eventDictionary == null) {
            eventDictionary = new Dictionary<EventType, ProgramEvent>();
        }
    }

    public static void StartListening(EventType eventName, UnityAction<object> listener) {
        ProgramEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent)) {
            thisEvent.AddListener(listener);
        }
        else {
            thisEvent = new ProgramEvent();
            thisEvent.AddListener(listener);
            instance.eventDictionary.Add(eventName, thisEvent);
        }
    }

    public static void StopListening(EventType eventName, UnityAction<object> listener) {
        if (instance == null) return;
        ProgramEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent)) {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void TriggerEvent(EventType eventName, object param) {
        ProgramEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent)) {
            thisEvent.Invoke(param);
        }
    }
}