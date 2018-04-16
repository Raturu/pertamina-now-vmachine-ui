using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModalDesriptionCenterer : MonoBehaviour {

    public float normalBottom = 160;
    public float extendedBottom = 64;

    public Button successButton;
    public Button alternativeButton;

    private RectTransform rectTransform;

    void Start() {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update() {
        SetDescriptionHeight();
    }

    void SetDescriptionHeight() {
        if (!successButton.isActiveAndEnabled && !alternativeButton.isActiveAndEnabled) {
            rectTransform.offsetMin = new Vector2(rectTransform.offsetMin.x, extendedBottom);
        }
        else {
            rectTransform.offsetMin = new Vector2(rectTransform.offsetMin.x, normalBottom);
        }
    }
}
