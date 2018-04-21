using System.Collections;
using System.Globalization;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InputListener : MonoBehaviour {

    public InputField inputField;
    public UnityEvent events;

    void Update() {
        ListenConfirmation();
    }

    private void ListenConfirmation() {
        if (Input.GetButtonDown("Submit")) {
            events.Invoke();
        }
    }

    public void CancelTransaction () {
        EventManager.TriggerEvent(EventType.TRANSACTION_CANCELLED, null);
        SceneManager.LoadScene("SelectGasolineType");
    }

    public void ToggleMode() {

    }

    public void NumberButtonPressed(string value) {
        inputField.text += value;
    }
}
