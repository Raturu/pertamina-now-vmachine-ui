using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardListener : MonoBehaviour {

    void CardPrint(object uid) {
        Debug.Log((string)uid);
    }

	void OnEnable () {
        EventManager.StartListening(EventType.CARD_READ, CardPrint);
	}

    void OnDisable() {
        EventManager.StopListening(EventType.CARD_READ, CardPrint);
    }
	
}
