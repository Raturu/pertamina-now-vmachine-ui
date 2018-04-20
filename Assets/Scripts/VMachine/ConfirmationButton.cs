using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class UserInput {
    public bool inRupiahMode;
    public int gasolineValueInRupiah;
    public double gasolineValueInLiter;
    public bool freeMode = false;
}

public class ConfirmationButton : MonoBehaviour {

    public bool inRupiahMode;
    public InputField input;

    void Start() {
        inRupiahMode = true;
    }

    public void ConfirmPressed() {
        string value = input.text;
        value = Regex.Replace(value, "[^0-9]", "");

        if (value == "") {
            return;
        }

        UserInput userInput = new UserInput();

        userInput.inRupiahMode = inRupiahMode;

        if (inRupiahMode)
            userInput.gasolineValueInRupiah = int.Parse(value);
        else
            userInput.gasolineValueInLiter = double.Parse(value);

        EventManager.TriggerEvent(EventType.USER_CONFIRMED, userInput);
    }

    public void UserFreeMode() {
        UserInput userInput = new UserInput();
        userInput.freeMode = true;

        EventManager.TriggerEvent(EventType.USER_CONFIRMED, userInput);
    }
}
