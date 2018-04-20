using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Filler : MonoBehaviour {

    public double slowFillSpeed = 0.1f;

    public void SlowFill() {
        double currentLiter = VMachineApplication.instance.GetCurrentRealLiter();
        VMachineApplication.instance.SetUsage(currentLiter + slowFillSpeed);
    }

    public void InstantFill() {
        // BOOOOOOOOOOOOOO YAAA
        VMachineApplication.instance.SetUsage(1000000.0f);
    }

    public void FinishFill() {
        EventManager.TriggerEvent(EventType.FINISH_FILL, null);
    }
}
