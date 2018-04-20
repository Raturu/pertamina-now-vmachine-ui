using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class TypeLoader : MonoBehaviour {

    private string soldGasolineDataFilePath = "/StreamingAssets/gasoline.json";

	void Start () {
        LoadGasolineData();
	}

    private void LoadGasolineData() {
        GasolineData[] gasolineSold;
        string filePath = Application.dataPath + soldGasolineDataFilePath;

        if (File.Exists(filePath)) {
            string dataAsJson = File.ReadAllText(filePath);
            gasolineSold = JsonHelper.GetJsonArray<GasolineData>(dataAsJson);
        }
        else {
            gasolineSold = new GasolineData[0];
        }

        EventManager.TriggerEvent(EventType.GASOLINE_LOADED, gasolineSold);
    }
}
