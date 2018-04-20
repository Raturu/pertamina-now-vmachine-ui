using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeButtonModifier : MonoBehaviour {

    public Sprite premiumSprite;
    public Sprite pertaliteSprite;
    public Sprite pertamaxSprite;
    public Sprite otherSprite;

    public Text buttonText;
    public Image buttonImage;
    public GasolineData gasolineData;

    // Quick
    Sprite GetImage(string nama_bbm) {

        if (nama_bbm == "Premium") {
            return premiumSprite;
        }
        else if (nama_bbm == "Pertalite") {
            return pertaliteSprite;
        }
        else if (nama_bbm == "Pertamax") {
            return pertamaxSprite;
        }
        else {
            return otherSprite;
        }
    }

    void Start() {
        buttonText.text = gasolineData.nama_bbm;
        buttonImage.sprite = GetImage(gasolineData.nama_bbm);
    }
}
