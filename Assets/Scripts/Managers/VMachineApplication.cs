using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class GasolineData {
    public int id_spbu;
    public int id_bbm;
    public int id_spbu_bbm;
    public int harga;
    public string nama_bbm;
    public Sprite button_image;
}

public class TransactionData {
    public GasolineData currentGasoline;
    public string currentUID;
    public int gasolineValue;
    public int maxUsage;
    public int gasolineValueInLiter;
}

public class VMachineApplication : Singleton<VMachineApplication> {

    [SerializeField]
    private int id_spbu;

    //private string currentUID;
    //private int gasolineValue;
    ////public int gasolineValueInLiter { private set; get; }
    //public int maxUsage { private set; get; }

    List<TransactionData> pendingTransactionUsage;
    TransactionData currentTransaction;

    private GasolineData[] gasolineSold;

    void InitTransaction(object gasData) {
        GasolineData currentGasoline = (GasolineData)gasData;
        currentTransaction = new TransactionData();
        currentTransaction.currentGasoline = currentGasoline;

        GoToSelectValue();
    }

    void UserConfirmed(object val) {
        int intValue = (int)val;
        if (intValue == 0) return;

        currentTransaction.gasolineValue = intValue;

        GoToCardRead();
    }

    void ResetTransactionData() {
        //currentTransaction.currentUID = "";
        //currentTransaction.gasolineValue = 0;
        //currentTransaction.gasolineValueInLiter = 0;
        currentTransaction = null;
    }

    void InitiateRequest(object uid) {
        string uidString = (string)uid;
        currentTransaction.currentUID = uidString;

        RequestData requestData = new RequestData(currentTransaction.currentUID, currentTransaction.gasolineValue);

        EventManager.TriggerEvent(EventType.TRANSACTION_REQUEST_INITIATION, requestData);
    }

    void SetMaxUsage(object usageObject) {
        int usageInt = (int)usageObject;

        currentTransaction.maxUsage = usageInt;
    }

    public int GetSPBUID() {
        return id_spbu;
    }

    void SetGasolineSold(object sold) {
        GasolineData[] soldData = (GasolineData[])sold;
        gasolineSold = soldData;
    }

    void GoToCardRead() {
        SceneManager.LoadScene("CardRead");
    }

    void GoToSelectValue() {
        SceneManager.LoadScene("InputGasolineValue");
    }

    void OnEnable() {
        EventManager.StartListening(EventType.USER_CONFIRMED, UserConfirmed);
        EventManager.StartListening(EventType.DATA_COMPLETE, InitiateRequest);
        EventManager.StartListening(EventType.MAX_USAGE_CONTROL, SetMaxUsage);
        EventManager.StartListening(EventType.GASOLINE_LOADED, SetGasolineSold);
        EventManager.StartListening(EventType.GASOLINE_SELECTED, InitTransaction);
    }

    void OnDisable() {
        EventManager.StopListening(EventType.USER_CONFIRMED, UserConfirmed);
        EventManager.StopListening(EventType.DATA_COMPLETE, InitiateRequest);
        EventManager.StopListening(EventType.MAX_USAGE_CONTROL, SetMaxUsage);
        EventManager.StopListening(EventType.GASOLINE_LOADED, SetGasolineSold);
        EventManager.StopListening(EventType.GASOLINE_SELECTED, InitTransaction);
    }
}
