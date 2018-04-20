using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RequestData {
    public string uid;
    public int requestValue;
    public bool freeMode;

    public RequestData(string uid, int requestValue, bool freeMode) {
        this.uid = uid;
        this.requestValue = requestValue;
        this.freeMode = freeMode;
    }

    //public RequestData(string uid, int requestValue) {
    //    this.uid = uid;
    //    this.requestValue = requestValue;
    //    this.freeMode = false;
    //}

    //public RequestData(string uid) {
    //    this.uid = uid;
    //    this.requestValue = 0;
    //    this.freeMode = true;
    //}

    public string freeModeString() {
        return (freeMode ? "TRUE" : "FALSE");
    }
}

public class RequestCreator : MonoBehaviour {

    public string apiKey = "CODEX@123";

    //void Start() {
    //    RequestData rdata = new RequestData("04:22:2A:E2:7C:28:80", 20000);
    //    StartCoroutine(Upload(rdata));
    //}

    void CreateRequest(object request) {

        RequestData requestData = (RequestData)request;
        StartCoroutine(Upload(requestData));
    }

    void OnEnable() {
        EventManager.StartListening(EventType.TRANSACTION_REQUEST_INITIATION, CreateRequest);
    }

    public void OnDisable() {
        EventManager.StopListening(EventType.TRANSACTION_REQUEST_INITIATION, CreateRequest);
    }

    IEnumerator Upload(RequestData requestData) {
        Dictionary<string, string> formFields = new Dictionary<string, string>();
        formFields.Add("uid", requestData.uid);
        formFields.Add("request_value", requestData.requestValue.ToString());
        formFields.Add("free_mode", requestData.freeModeString());

        //UnityWebRequest www = UnityWebRequest.Post("http://127.0.0.1:8000/vrest/validate/", formFields); // Local server test
        UnityWebRequest www = UnityWebRequest.Post("http://159.65.139.83/pertamina-now/api/Collection/requestBuy/", formFields); // Remote server
        www.SetRequestHeader("x-api-key", apiKey);

        www.timeout = 5; // 5 seconds timeout

        yield return new WaitForSeconds(2); // Simulate network delay :v
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError) {
            Debug.Log(www.error);
            Debug.Log(www.downloadHandler.text);
            EventManager.TriggerEvent(EventType.TRANSACTION_REQUEST_ERROR, null);
        }
        else {
            Debug.Log("Transaction Request Complete!");
            Debug.Log(www.downloadHandler.text);
            Debug.Log(www.error);
            EventManager.TriggerEvent(EventType.TRANSCATION_REQUEST_RESPONSE_RECEIVED, www.downloadHandler.text);
        }
    }
	
}
