using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class RupiahView : MonoBehaviour {

    private Text rupiahText;

    void Start() {
        rupiahText = GetComponent<Text>();
    }


	void Update () {
        int rupiah = VMachineApplication.instance.GetCurrentRealRupiahForView();

        FormatValue(rupiah.ToString());
	}

    public void FormatValue(string value) {
        value = Regex.Replace(value, "[^0-9]", "");
        if (value == "") {
            rupiahText.text = "";
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
        rupiahText.text = formattedValue;
    }
}
