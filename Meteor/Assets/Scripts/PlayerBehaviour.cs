using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {
    [HideInInspector]
    public bool gameStart = false;
    public int player;
    [SerializeField]
    int maxLives;
    int curLives;
    Vector3 rotationDir;
    public float speed;
    float rotateSpeed;
    Rigidbody myRB;
    Vector3 joystickInput;

    void Start() {
        curLives = maxLives;
        myRB = GetComponent<Rigidbody>();
        rotateSpeed = speed * Time.deltaTime;
    }

    private void FixedUpdate() {
        if (gameStart) {
            Flight();
        }
    }

    void Update() {
        if (gameStart) {
            if (myRB.velocity.sqrMagnitude >= 0.1f) {
                RotateForwards();
            }
        }
    }

    void RotateForwards() {
        rotationDir = Vector3.RotateTowards(myRB.transform.forward, myRB.velocity.normalized, rotateSpeed, 0.0f);
        transform.rotation = Quaternion.LookRotation(rotationDir);
        rotationDir = Vector3.RotateTowards(myRB.transform.up, myRB.velocity.normalized, rotateSpeed, 0.0f);
        transform.rotation = Quaternion.LookRotation(rotationDir);
    }

    void Flight() {
        joystickInput = new Vector3(Input.GetAxis("Joystick" + player + "x"), 0f, Input.GetAxis("Joystick" + player + "y")).normalized;

    }

    public void DamageMe(Transform other, bool playerShot) {
        if (gameStart) {
            curLives -= 1;
            if (curLives <= 0) {
                Destroy(gameObject);
            }
        }
    }
}