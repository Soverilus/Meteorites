﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void SceneChange(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }
    public void AppQuit() {
        Application.Quit();
    }
}
