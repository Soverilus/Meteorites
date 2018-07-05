using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEffect : MonoBehaviour {

    ParticleSystem myPS;
	void Start () {
        myPS = GetComponent<ParticleSystem>();
		
	}
	
	// Update is called once per frame
	void Update () {
		if (myPS.time >= myPS.main.duration) {
            Destroy(gameObject);
        }
	}
}
