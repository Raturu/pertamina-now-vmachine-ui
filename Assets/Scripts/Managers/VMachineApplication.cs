using System;
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
    public int gasolineValueInRupiah;
    public int maxUsageInRupiah;
    public double maxUsageInLiter;
    public bool freeMode = false;

    public int realUsage;
    public double realLiter;
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

    // Dummy transaction
    //void Start() {
    //    currentTransaction = new TransactionData();
    //    currentTransaction.currentGasoline = new GasolineData();
    //    currentTransaction.currentGasoline.harga = 6000;
    //    currentTransaction.currentGasoline.nama_bbm = "Premium";
    //    currentTransaction.currentGasoline.id_spbu = 1;
    //    currentTransaction.currentGasoline.id_bbm = 1;
    //    currentTransaction.currentGasoline.id_spbu_bbm = 1;

    //    currentTransaction.currentUID = "04222AE27C2880";
    //    currentTransaction.gasolineValueInRupiah = 20000;
    //    currentTransaction.maxUsageInRupiah = 20000;
    //    currentTransaction.maxUsageInLiter = 3.333333;
    //}

    void InitTransaction(object gasData) {
        GasolineData currentGasoline = (GasolineData)gasData;

        currentTransaction = new TransactionData();
        currentTransaction.currentGasoline = currentGasoline;

        GoToSelectValue();
    }

    void UserConfirmed(object param) {
        UserInput userInput = (UserInput)param;

        if (userInput.freeMode) {
            currentTransaction.freeMode = true;
        }
        else if (userInput.inRupiahMode) {
            if (userInput.gasolineValueInRupiah == 0) return;

            currentTransaction.gasolineValueInRupiah = userInput.gasolineValueInRupiah;
        }
        else {
            if (userInput.gasolineValueInLiter == 0) return;

            currentTransaction.gasolineValueInRupiah = CountRupiah(userInput.gasolineValueInLiter);
        }

        GoToCardRead();
    }

    void ResetTransactionData() {
        Debug.Log("Transaction data reset");
        //currentTransaction.currentUID = "";
        //currentTransaction.gasolineValue = 0;
        //currentTransaction.gasolineValueInLiter = 0;
        currentTransaction = null;
    }

    void InitiateRequest(object uid) {
        string uidString = (string)uid;
        currentTransaction.currentUID = uidString;

        RequestData requestData = new RequestData(currentTransaction.currentUID, currentTransaction.gasolineValueInRupiah, currentTransaction.freeMode);

        EventManager.TriggerEvent(EventType.TRANSACTION_REQUEST_INITIATION, requestData);
    }

    void SetMaxUsage(object usageObject) {
        int usageInt = (int)usageObject;

        currentTransaction.maxUsageInRupiah = usageInt;
        currentTransaction.maxUsageInLiter = CountLiter(usageInt);
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

    double CountLiter(int rupiah) {
        double liter = Convert.ToDouble(rupiah) / currentTransaction.currentGasoline.harga;

        return liter;
    }

    int CountRupiah(double liter) {
        double rupiahDouble = liter * currentTransaction.currentGasoline.harga;
        int rupiah = Convert.ToInt32(rupiahDouble);

        return rupiah;
    }

    public void SetUsage(double liter) {
        if (liter > currentTransaction.maxUsageInLiter)
            liter = currentTransaction.maxUsageInLiter;

        currentTransaction.realLiter = liter;

        currentTransaction.realUsage = CountRupiah(liter);
    }

    public double GetCurrentRealLiter() {
        return currentTransaction.realLiter;
    }

    public int GetCurrentRealRupiah() {
        return currentTransaction.realUsage;
    }

    void FillFinish() {
        Debug.Log("FIIIIIIIIINISH");
        Debug.Log(currentTransaction.realUsage);
        Debug.Log(currentTransaction.realLiter);
    }

    void OnEnable() {
        EventManager.StartListening(EventType.USER_CONFIRMED, UserConfirmed);
        EventManager.StartListening(EventType.DATA_COMPLETE, InitiateRequest);
        EventManager.StartListening(EventType.MAX_USAGE_CONTROL, SetMaxUsage);
        EventManager.StartListening(EventType.GASOLINE_LOADED, SetGasolineSold);
        EventManager.StartListening(EventType.GASOLINE_SELECTED, InitTransaction);
        EventManager.StartListening(EventType.TRANSACTION_CANCELLED, delegate { ResetTransactionData(); });
        EventManager.StartListening(EventType.FINISH_FILL, delegate { FillFinish(); });
    }

    void OnDisable() {
        EventManager.StopListening(EventType.USER_CONFIRMED, UserConfirmed);
        EventManager.StopListening(EventType.DATA_COMPLETE, InitiateRequest);
        EventManager.StopListening(EventType.MAX_USAGE_CONTROL, SetMaxUsage);
        EventManager.StopListening(EventType.GASOLINE_LOADED, SetGasolineSold);
        EventManager.StopListening(EventType.GASOLINE_SELECTED, InitTransaction);
        EventManager.StopListening(EventType.TRANSACTION_CANCELLED, delegate { ResetTransactionData(); });
        EventManager.StopListening(EventType.FINISH_FILL, delegate { FillFinish(); });
    }
}
