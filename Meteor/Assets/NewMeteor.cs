using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMeteor : MonoBehaviour {
    bool isQuitting;
    ScreenWrap spawnScript;
    private void Start() {
        spawnScript = GameObject.FindGameObjectWithTag("Spawner").GetComponent<ScreenWrap>();
    }
    private void OnApplicationQuit() {
        isQuitting = true;
    }
    private void OnDestroy() {
        if (!isQuitting) {
            spawnScript.MakeMoreMeteor();
        }
    }
}
