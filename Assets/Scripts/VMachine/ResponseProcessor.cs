using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Response {
    public string status;
    public int max_buy;
}

public enum REQUEST_STATE {
    SUCCESS,
    NOT_ENOUGH_BALANCE,
    ERROR
}

public class ResponseProcessor : MonoBehaviour {

    REQUEST_STATE GetResponseState(string state) {
        if (state == "OK")
            return REQUEST_STATE.SUCCESS;
        else if (state == "NO_BALANCE")
            return REQUEST_STATE.NOT_ENOUGH_BALANCE;
        else
            return REQUEST_STATE.ERROR;
    }

    void CheckResponse(string status) {
        REQUEST_STATE state = GetResponseState(status);

        switch (state) {
            case REQUEST_STATE.SUCCESS:
                EventManager.TriggerEvent(EventType.TRANSACTION_REQUEST_SUCCESS, null);
                break;
            case REQUEST_STATE.NOT_ENOUGH_BALANCE:
                EventManager.TriggerEvent(EventType.TRANSACTION_REQUEST_NOT_ENOUGH_BALANCE, null);
                break;
            case REQUEST_STATE.ERROR:
                EventManager.TriggerEvent(EventType.TRANSACTION_REQUEST_ERROR, null);
                break;
            default:
                break;
        }
    }

    void ProcessResponse(Response response) {
        EventManager.TriggerEvent(EventType.MAX_USAGE_CONTROL, response.max_buy);

        CheckResponse(response.status);
    }

    void ParseRespose(object responseObject) {
        string responseString = (string)responseObject;

        Response response = JsonUtility.FromJson<Response>(responseString);

        ProcessResponse(response);
    }

    void OnEnable() {
        EventManager.StartListening(EventType.TRANSCATION_REQUEST_RESPONSE_RECEIVED, ParseRespose);
    }

    void OnDisable() {
        EventManager.StopListening(EventType.TRANSCATION_REQUEST_RESPONSE_RECEIVED, ParseRespose);
    }
}
