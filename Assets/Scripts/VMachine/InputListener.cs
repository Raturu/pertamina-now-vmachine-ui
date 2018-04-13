using System.Collections;
using System.Globalization;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InputListener : MonoBehaviour {

    public UnityEvent events;

    void Update() {
        ListenConfirmation();
    }

    private void ListenConfirmation() {
        if (Input.GetButtonDown("Submit")) {
            events.Invoke();
        }
    }
}
