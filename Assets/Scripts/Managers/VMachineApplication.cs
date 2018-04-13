using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class VMachineApplication : Singleton<VMachineApplication> {

    public int gasolineValue { private set; get; }
    public int gasolineValueInLiter { private set; get; }
    public string currentUID { private set; get; }

	void OnEnable () {
        EventManager.StartListening(EventType.USER_CONFIRMED, delegate { UserConfirmed(); });
	}

    void OnDisable() {
        EventManager.StopListening(EventType.USER_CONFIRMED, delegate { UserConfirmed(); });
    }

    public void SetGasolineValue(string val) {
        gasolineValue = int.Parse(val);
    }

    void UserConfirmed() {
        if (gasolineValue == 0) return;

        GoToCardRead();
    }

    void GoToCardRead() {
        SceneManager.LoadScene("CardRead");
    }
}
