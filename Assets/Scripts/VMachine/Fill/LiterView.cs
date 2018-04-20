using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class LiterView : MonoBehaviour {

    private Text literText;

    void Start() {
        literText = GetComponent<Text>();
    }


    void Update() {
        double liter = VMachineApplication.instance.GetCurrentRealLiter();

        literText.text = liter.ToString("0.##");
        //FormatValue(liter.ToString());
    }

    public void FormatValue(string value) {
        value = Regex.Replace(value, "[^0-9.]", "");
        if (value == "") {
            literText.text = "";
            return;
        }

        double doubleValue = 0;
        try {
            doubleValue = double.Parse(value);
        }
        catch (FormatException err) {
            Debug.LogError(err);
            Debug.LogError("Value: " + value);
        }

        string formattedValue = string.Format(new CultureInfo("id-ID"), "{0:n0}", doubleValue);
        literText.text = formattedValue;
    }
}
