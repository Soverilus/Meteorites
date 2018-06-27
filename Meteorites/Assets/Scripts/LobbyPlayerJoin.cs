using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyPlayerJoin : MonoBehaviour {
    bool[] playerConnect;


	void Start () {
        playerConnect = new bool[4];
        for(int i = 0; i < playerConnect.Length; i++) {
            playerConnect[i] = false;
        }
	}

	void Update () {
        if (Input.GetAxis("AnyFire") > 0) {
            for (int j = 1; j <= 4 /*|| playerConnect[j - 1] == true*/; j++) {
                if (Input.GetAxis("Fire" + j.ToString()) > 0) {
                    playerConnect[j - 1] = true;
                }
            }
        }
	}
}
