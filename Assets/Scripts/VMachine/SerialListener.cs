using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;

public class SerialListener : MonoBehaviour {

    bool ValidateFormat(string uid) {
        string regex = @"^([0-9A-Fa-f]{2}[:])*([0-9A-Fa-f]{2})$";
        bool isValid = Regex.IsMatch(uid, regex);

        if (isValid)
            EventManager.TriggerEvent(EventType.VALID_CARD_READED, null);

        return isValid;
    }

    void SerialProcess(object message) {
        string uidString = (string)message;
        if (!ValidateFormat(uidString)) {
            return;
        }

        EventManager.TriggerEvent(EventType.DATA_COMPLETE, uidString);
    }

	void OnEnable () {
        EventManager.StartListening(EventType.SERIAL_RECEIVED, SerialProcess);
	}

    void OnDisable() {
        EventManager.StopListening(EventType.SERIAL_RECEIVED, SerialProcess);
    }
	
}
