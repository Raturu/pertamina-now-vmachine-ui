using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VMachineApplication : Singleton<VMachineApplication> {

    private string currentUID;
    private int gasolineValue;
    //public int gasolineValueInLiter { private set; get; }
    public int maxUsage { private set; get; }

    void UserConfirmed(object val) {
        int intValue = (int)val;
        if (intValue == 0) return;

        gasolineValue = intValue;

        GoToCardRead();
    }

    void ResetTransactionData() {
        currentUID = "";
        gasolineValue = 0;
        //gasolineValueInLiter = 0;
    }

    void InitiateRequest(object uid) {
        string uidString = (string)uid;
        currentUID = uidString;

        RequestData requestData = new RequestData(currentUID, gasolineValue);

        EventManager.TriggerEvent(EventType.TRANSACTION_REQUEST_INITIATION, requestData);
    }

    void SetMaxUsage(object usageObject) {
        int usageInt = (int)usageObject;

        maxUsage = usageInt;
    }

    void GoToCardRead() {
        SceneManager.LoadScene("CardRead");
    }

    void OnEnable() {
        EventManager.StartListening(EventType.USER_CONFIRMED, UserConfirmed);
        EventManager.StartListening(EventType.DATA_COMPLETE, InitiateRequest);
        EventManager.StartListening(EventType.MAX_USAGE_CONTROL, SetMaxUsage);
    }

    void OnDisable() {
        EventManager.StopListening(EventType.USER_CONFIRMED, UserConfirmed);
        EventManager.StopListening(EventType.DATA_COMPLETE, InitiateRequest);
        EventManager.StopListening(EventType.MAX_USAGE_CONTROL, SetMaxUsage);
    }
}
