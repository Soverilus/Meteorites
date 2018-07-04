﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {
    //sorry for the lack of commenting. I was trying to get this done as quickly as possible. If you're reading this then I didn't have time later in the week to add comments to everything.
    float timer;

    [HideInInspector]
    public bool gameStart = false;
    public int player;

    [SerializeField]
    int maxLives;
    int curLives;

    Quaternion rotationDir;
    Vector3 targetDir;
    public float rotateSpeed;
    Rigidbody myRB;
    Vector3 leftStickInput;

    public int maxAmmo;
    int ammo;
    bool shot = false;
    public float shotSpeed;
    [SerializeField]
    GameObject[] cannon;
    public GameObject bullet;
    public float reloadTime;
    float reloadTimer;

    public float maxSpeed;
    public float speed;

    void Start() {
        curLives = maxLives;
        myRB = GetComponent<Rigidbody>();
    }

    private void Update() {
        transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
        timer += Time.deltaTime;
        reloadTimer += Time.deltaTime;

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
        if (Input.GetAxisRaw("Fire" + player) > 0 && ammo > 0 && shot == false) {
            shot = true;
            Shoot();
            ammo -= 1;
        }
        if (Input.GetAxisRaw("Fire" + player) < 1) {
            shot = false;
        }
        if (reloadTimer >= reloadTime && ammo < maxAmmo) {
            ammo += 1;
            reloadTimer = 0f;
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
        Debug.Log("I've shot you");
        int whichCannon = Random.Range(0, 2);
        GameObject projectile = Instantiate(bullet, cannon[whichCannon].transform.position, transform.rotation);
        Rigidbody projRB = projectile.GetComponent<Rigidbody>();
        projRB.velocity = -projectile.transform.forward * Time.deltaTime * shotSpeed;
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