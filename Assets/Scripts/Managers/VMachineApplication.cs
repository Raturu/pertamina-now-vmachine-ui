using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;


public class VMachineApplication : Singleton<VMachineApplication> {

    public int gasolineValue { private set; get; }
    public int gasolineValueInLiter { private set; get; }
    public string currentUID { private set; get; }

	void OnEnable () {
        EventManager.StartListening(EventType.USER_CONFIRMED, UserConfirmed);
	}

    void OnDisable() {
        EventManager.StopListening(EventType.USER_CONFIRMED, UserConfirmed);
    }

    void UserConfirmed(object val) {
        int intValue = (int)val;
        if (intValue == 0) return;

        gasolineValue = intValue;

        GoToCardRead();
    }

    void GoToCardRead() {
        SceneManager.LoadScene("CardRead");
    }
}
