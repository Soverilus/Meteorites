using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyPlayerJoin : MonoBehaviour {
    bool doOnce = true;
    [HideInInspector]
    public bool gameStart = false;
    [HideInInspector]
    public GameObject[] playerObjects = new GameObject[4];
    bool[] playerConnect;
    ControllerBehaviour shipListController;
    [SerializeField]
    GameObject playerObject;
    [SerializeField]
    Transform[] shipSpawns;

    void Start() {
        shipListController = GetComponent<ControllerBehaviour>();
        playerConnect = new bool[4];
        for (int i = 0; i < playerConnect.Length; i++) {
            playerConnect[i] = false;
        }
    }

    void Update() {
        if (!gameStart) {
            if (Input.GetAxis("AnyFire") > 0 || Input.GetAxis("AnySpecial") > 0) {
                for (int j = 1; j <= 4; j++) {
                    if (Input.GetAxis("Fire" + j.ToString()) > 0 && playerConnect[j - 1] == false) {
                        playerConnect[j - 1] = true;
                        Debug.Log("Player " + j + " has connected");
                        CreatePlayer(j);
                    }
                    if (Input.GetAxis("Special" + j.ToString()) > 0 && playerConnect[j - 1] == true) {
                        playerConnect[j - 1] = false;
                        Debug.Log("Player " + j + " has disconnected");
                        DeletePlayer(j);
                    }
                }
            }
        }
        else if (gameStart && doOnce) {
            for (int i = 0; i < playerObjects.Length; i++) {
                PlayerBehaviour playerActivate = playerObjects[i].GetComponent<PlayerBehaviour>();
                playerActivate.gameStart = true;
            }
            doOnce = false;
        }
    }

    void CreatePlayer(int playerNum) {
        GameObject playerShip = Instantiate(playerObject, shipSpawns[playerNum - 1].position, shipSpawns[playerNum - 1].rotation);
        playerObjects[playerNum - 1] = playerShip;
        PlayerBehaviour playerIdentity = playerShip.GetComponent<PlayerBehaviour>();
        playerIdentity.player = playerNum;
    }

    void DeletePlayer(int playerNum) {
        shipListController.myShips.Remove(playerObjects[playerNum - 1]);
        Destroy(playerObjects[playerNum - 1]);
        playerObjects[playerNum - 1] = null;
    }
}