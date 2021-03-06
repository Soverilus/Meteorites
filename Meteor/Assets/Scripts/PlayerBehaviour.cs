﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {
    [SerializeField]
    GameObject playerDeath;
    bool isQuitting = false;
    float timer;
    ControllerBehaviour listReference;

    MeshRenderer myRend;
    Color myRendOriginal;

    [HideInInspector]
    public bool gameStart = false;
    public int player;

    [SerializeField]
    int maxLives;
    public int curLives;

    Quaternion rotationDir;
    Vector3 targetDir;
    public float rotateSpeed;
    Rigidbody myRB;
    Vector3 leftStickInput;

    public int maxAmmo;
    public int ammo;
    bool shot = false;
    public float shotSpeed;
    [SerializeField]
    GameObject[] cannon;
    public GameObject bullet;
    public float reloadTime;
    float reloadTimer;
    float colorTimer;

    public float maxSpeed;
    public float speed;

    void Start() {
        listReference = GameObject.FindGameObjectWithTag("Spawner").GetComponent<ControllerBehaviour>();
        myRend = GetComponent<MeshRenderer>();
        SetPlayerColor();
        curLives = maxLives;
        myRB = GetComponent<Rigidbody>();
    }

    void SetPlayerColor() {
        float r = 0.5f;
        float g = 0.5f;
        float b = 0.5f;
        if (player == 1 || player == 3) {
            r = 1f;
            b = 0.5f;
            if (player == 1) {
                g = 0.5f;
            }
            else {
                g = 1f;
            }
        }
        else if (player == 2 || player == 4) {
            b = 1f;
            r = 0.5f;
            if (player == 2) {
                g = 0.5f;
            }
            else {
                g = 1f;
            }
        }
        myRend.material.color = new Color(r, g, b);
        myRendOriginal = myRend.material.color;
        name = "Player" + player;
    }

    private void Update() {
        transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
        timer += Time.deltaTime;
        if (myRend.material.color == Color.red) {
            colorTimer += Time.deltaTime;
            if (colorTimer >= 0.5f) {
                myRend.material.color = myRendOriginal;
                colorTimer = 0f;
            }
        }
        if (ammo == 0) {
            reloadTimer += Time.deltaTime;
            if (reloadTimer >= reloadTime) {
                reloadTimer = 0f;
                ammo = maxAmmo;
            }
        }

        if (gameStart) {
            InGameUpdate();
        }
    }

    private void FixedUpdate() {
        if (gameStart) {
            Flight();
        }
    }

    void InGameUpdate() {
        leftStickInput = new Vector3(Input.GetAxis("Joystick" + player + "x"), 0f, Input.GetAxis("Joystick" + player + "y")).normalized;
        if (leftStickInput.sqrMagnitude > 0.1f) {
            RotateForwards();
        }
        if (Input.GetAxisRaw("Fire" + player) > 0 && shot == false) {
            shot = true;
            Shoot();
        }
        if (Input.GetAxisRaw("Fire" + player) < 1) {
            shot = false;
        }
    }

    void Flight() {
        myRB.AddForce(transform.forward * Input.GetAxis("Thrust" + player) * speed * myRB.mass * Time.fixedDeltaTime);
        if (myRB.velocity.sqrMagnitude > maxSpeed) {
            myRB.velocity = Mathf.Clamp(myRB.velocity.magnitude, 0f, maxSpeed) * myRB.velocity.normalized;
        }
    }

    void RotateForwards() {
        targetDir = (leftStickInput).normalized;
        rotationDir = Quaternion.LookRotation(targetDir);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotationDir, Time.deltaTime * rotateSpeed);
    }

    void Shoot() {
        if (ammo > 0) {
            int whichCannon = Random.Range(0, 2);
            GameObject projectile = Instantiate(bullet, cannon[whichCannon].transform.position, transform.rotation);
            Rigidbody projRB = projectile.GetComponent<Rigidbody>();
            projRB.velocity = -projectile.transform.forward * Time.deltaTime * shotSpeed;
            ammo -= 1;
        }
    }

    public void DamageMe(Transform other, bool playerShot) {
        if (gameStart) {
            if (myRend.material.color == myRendOriginal) {
                curLives -= 1;
            }
            if (curLives <= 0) {
                GameObject[] wings = new GameObject[transform.childCount];
                for (int i = 0; i < wings.Length; i++) {
                    wings[i] = transform.GetChild(i).gameObject;
                    listReference.myShips.Remove(wings[i]);
                }
                listReference.myShips.Remove(gameObject);
                Destroy(gameObject);
            }
            myRend.material.color = Color.red;
        }
    }
    private void OnApplicationQuit() {
        isQuitting = true;
    }
    private void OnDestroy() {
        if (!isQuitting) {
            Instantiate(playerDeath, transform.position, Quaternion.identity);
        }
    }
}