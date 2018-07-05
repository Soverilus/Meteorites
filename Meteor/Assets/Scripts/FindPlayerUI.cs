using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FindPlayerUI : MonoBehaviour {
    bool foundPlayer = false;
    GameObject player;
    public int num;
    public Text myText;
    public Text livesText;
    public Text ammoText;
    PlayerBehaviour pMisc;

    private void Start() {
    }

    void Update() {
        if (!foundPlayer) {
            if (GameObject.Find("Player" + num) != null) {
                player = GameObject.Find("Player" + num);
                pMisc = player.GetComponent<PlayerBehaviour>();
                GetText();
                foundPlayer = true;
            }
        }
        if (foundPlayer) {
            livesText.text = pMisc.curLives.ToString();
            ammoText.text = pMisc.ammo.ToString();
        }
    }
    void GetText() {
        myText.text = player.name;
    }
}
