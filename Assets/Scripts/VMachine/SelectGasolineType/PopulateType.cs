using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulateType : MonoBehaviour {

    public GameObject buttonHolder;
    public GameObject typeButton;

    void GenerateButton(GasolineData data) {
        GameObject newButton = Instantiate(typeButton, buttonHolder.transform);
        TypeButtonModifier buttonModifier = newButton.GetComponent<TypeButtonModifier>();

        buttonModifier.gasolineData = data;
    }

    public void PopulateButton(object sold) {
        GasolineData[] gasData = (GasolineData[])sold;

        for (int i = 0; i < gasData.Length; i++) {
            GenerateButton(gasData[i]);
        }
    }

    void OnEnable() {
        EventManager.StartListening(EventType.GASOLINE_LOADED, PopulateButton);
    }

    void OnDisable() {
        EventManager.StopListening(EventType.GASOLINE_LOADED, PopulateButton);
    }
}
