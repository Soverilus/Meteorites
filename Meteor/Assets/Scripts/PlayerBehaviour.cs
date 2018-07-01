using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {
    public int player;
    [SerializeField]
    int maxLives;
    int curLives;
    Vector3 newDir;
    public float speed;
    float rotateSpeed;
    Rigidbody myRB;

    void Start() {
        curLives = maxLives;
        myRB = GetComponent<Rigidbody>();
        rotateSpeed = speed * Time.deltaTime;
    }

    void Update() {
        if (myRB.velocity.sqrMagnitude >= 0.1f) {
            RotateForwards();
        }
    }

    void RotateForwards() {
        newDir = Vector3.RotateTowards(myRB.transform.forward, myRB.velocity.normalized, rotateSpeed, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDir);
        newDir = Vector3.RotateTowards(myRB.transform.up, myRB.velocity.normalized, rotateSpeed, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDir);
    }

    public void DamageMe(Transform other, bool playerShot) {
        curLives -= 1;
        if (curLives <= 0) {
            Destroy(gameObject);
        }
    }
}