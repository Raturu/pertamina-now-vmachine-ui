using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeSelector : MonoBehaviour {

    private GasolineTypeData gasTypeData;

    void Start() {
        gasTypeData = GetComponent<GasolineTypeData>();
    }

    public void Clicked() {
        GasolineData gasData = gasTypeData.gasolineData;

        EventManager.TriggerEvent(EventType.GASOLINE_SELECTED, gasData);
    }
}
