using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class ValueFormatter : MonoBehaviour {

    private InputField valueText;

    void Awake() {
        valueText = GetComponent<InputField>();
    }

    public void FormatValue(string value) {
        value = Regex.Replace(value, "[^0-9]", "");
        if (value == "") {
            valueText.text = "";
            return; 
        }

        int integerValue = 0;
        try {
             integerValue = int.Parse(value);
        }
        catch (FormatException err) {
            Debug.LogError(err);
            Debug.LogError("Value: " + value);
        }

        string formattedValue = string.Format(new CultureInfo("id-ID"), "{0:n0}", integerValue);
        valueText.text = formattedValue;
    }
}
