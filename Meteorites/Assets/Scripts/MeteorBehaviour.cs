using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorBehaviour : MonoBehaviour {
    [SerializeField]
    float maxSpeed;

    Rigidbody myRB;

	void Start () {
        myRB = GetComponent<Rigidbody>();
        myRB.velocity = new Vector3(10f, 0f, 5f);
	}

	void Update () {
		
	}
}
