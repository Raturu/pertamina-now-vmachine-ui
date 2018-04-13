using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmationButton : MonoBehaviour {

    public InputField input;

    public void ConfirmPressed() {
        string value = input.text;
        value = Regex.Replace(value, "[^0-9]", "");

        if (value == "") {
            return;
        }

        int gasolineValue = int.Parse(value);

        EventManager.TriggerEvent(EventType.USER_CONFIRMED, gasolineValue);
    }
}
