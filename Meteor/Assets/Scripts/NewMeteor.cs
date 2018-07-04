using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMeteor : MonoBehaviour {
    float frames;
    bool isQuitting;
    ScreenWrap spawnScript;
    private void Start() {
        spawnScript = GameObject.FindGameObjectWithTag("Spawner").GetComponent<ScreenWrap>();
    }
    private void Update() {
        frames = Time.deltaTime;
    }
    private void OnApplicationQuit() {
        isQuitting = true;
    }
    private void OnDestroy() {
        if (!isQuitting && frames < 0.02f) {
            spawnScript.MakeMoreMeteor();
        }
    }
}
