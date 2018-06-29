using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorBehaviour : MonoBehaviour {
    [SerializeField]
    float speed;

    Rigidbody myRB;

	void Start () {
        myRB = GetComponent<Rigidbody>();
	}

	void Update () {
		if (myRB.velocity.magnitude <= 0.01f) {
            StartingVelocity(new Vector3(Random.Range(-100f, 100f), 0f, Random.Range(-100f, 100f)));
        }
	}

    void StartingVelocity(Vector3 i) {
        Vector3 dir = i.normalized;
        myRB.velocity = dir * speed;
    }
}
