using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainbowEffect : MonoBehaviour {

    Color colorA;
    Color colorB;
    Light meRainboi;
    void Start() {
        meRainboi = GetComponent<Light>();
    }

    void Update() {
        ChangeColor();

    }
    //TODO
    void ChangeColor() {

    }
}
