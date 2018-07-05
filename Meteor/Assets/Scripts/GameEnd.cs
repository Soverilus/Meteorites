using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameEnd : MonoBehaviour {
    ControllerBehaviour myTimer;
    [SerializeField]
    GameObject gameEndPanel;
    int listCount = 4;
    bool hasList = false;
    [SerializeField]
    Text whoWin;
    int playerNum;

    private void Start() {
        gameEndPanel.SetActive(false);
        myTimer = GetComponent<ControllerBehaviour>();

    }

    void Update () {
        if (myTimer.timerTime <= 0f) {
            if (!hasList) {
                hasList = true;
            }
        }
		if (hasList) {
            listCount = myTimer.myShips.Count;
            if (listCount == 1) {
                Time.timeScale = 0;
                gameEndPanel.SetActive(true);
                foreach (GameObject playerObj in myTimer.myShips) {
                    if (playerObj.GetComponent<PlayerBehaviour>() != null) {
                        playerNum = playerObj.GetComponent<PlayerBehaviour>().player;
                        playerObj.GetComponent<PlayerBehaviour>().enabled = false;
                    }
                }
                whoWin.text = "Player" + playerNum + " has Won!";
            }
        }
	}
}
