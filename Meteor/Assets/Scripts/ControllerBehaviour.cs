﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerBehaviour : MonoBehaviour {
    public bool _forceGameStart;
    LobbyPlayerJoin gameStart;
    [SerializeField]
    Text timer;
    public float timerTime = 10f;
    float originalTime;
    public List<GameObject> myShips;

    void Awake() {
        _forceGameStart = false;
        Time.timeScale = 1f;
        originalTime = timerTime;
        foreach (GameObject wierdProblematicAutomatedGameObject in GameObject.FindGameObjectsWithTag("Player")) {
            Destroy(wierdProblematicAutomatedGameObject);
        }
    }

    void FindShips() {
        gameStart = GetComponent<LobbyPlayerJoin>();
        foreach (GameObject myShip in GameObject.FindGameObjectsWithTag("Player")) {
            if (!myShips.Contains(myShip)) {
                myShips.Add(myShip);
            }
        }
    }

    void Update() {
        for (int i = myShips.Count - 1; i > -1; i--) {
            if (myShips[i] == null)
                myShips.RemoveAt(i);
        }
        if (timerTime > 0f) {
            FindShips();
            if (myShips.Count < 2 && !_forceGameStart) {
                timerTime = originalTime;
            }
        }
        if (timer != null) {
            TickTock();
        }
        if (_forceGameStart) {
            timerTime = -1f;
        }
    }

    void TickTock() {
        timerTime -= Time.deltaTime;
        timer.text = timerTime.ToString("F0");
        if (timerTime < 1 && timerTime > 0) {
            timer.text = 1.ToString();
        }
        if (timerTime <= 0f) {
            Destroy(timer);
            gameStart.gameStart = true;
        }
    }
}
