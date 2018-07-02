using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerBehaviour : MonoBehaviour {
    LobbyPlayerJoin gameStart;
    [SerializeField]
    Text timer;
    public float timerTime = 10f;
    public List<GameObject> myShips;

    void Start() {
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
        if (timerTime > 0f) {
            FindShips();
        }
        if (timer != null) {
            TickTock();
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
