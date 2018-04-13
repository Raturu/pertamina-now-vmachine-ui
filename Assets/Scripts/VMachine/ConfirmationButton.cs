using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmationButton : MonoBehaviour {

    public void ConfirmPressed() {
        EventManager.TriggerEvent(EventType.USER_CONFIRMED, null);
    }
}
