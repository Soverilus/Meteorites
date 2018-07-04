using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrapChecker : MonoBehaviour {
    public bool hasWrapped = false;
    Renderer checkRender;
    Collider myCol;
    public float collisionTimer;
    public float collisionTimerMax;

    float maxX;
    float maxZ;
    float minX;
    float minZ;

    public int negValx;
    public int negValz;

    private void Start() {
        checkRender = GetComponent<Renderer>();
        if (GetComponent<MeshCollider>() != null) {
            myCol = GetComponent<MeshCollider>();
        }
        else if (GetComponent<SphereCollider>() != null) {
            myCol = GetComponent<SphereCollider>();
        }
        else {
            myCol = GetComponent<Collider>();
        }
        myCol.isTrigger = true;
    }

    public void GetRangeExtent(float upX, float dowX, float upZ, float dowZ) {
        maxX = upX;
        maxZ = upZ;
        minX = dowX;
        minZ = dowZ;
    }
    private void Update() {
        //Debug.Log(Time.deltaTime);
        if (collisionTimer < collisionTimerMax) {
            collisionTimer += Time.deltaTime;
        }
        if (checkRender.isVisible) {
            hasWrapped = false;
            myCol.isTrigger = true;
            if (transform.position.x >= maxX || transform.position.x <= minX) {
                negValx = -1;
            }
            else {
                negValx = 1;
            }

            if (transform.position.z >= maxZ || transform.position.z <= minZ) {
                negValz = -1;
            }
            else {
                if (collisionTimer >= collisionTimerMax) {
                    myCol.isTrigger = false;
                }
                negValz = 1;
            }
        }
    }
    public void WrapAround(Vector3 pos) {
        //Debug.Log("reached here2");
        if (!checkRender.isVisible && hasWrapped == false) {
            hasWrapped = true;
            transform.position = new Vector3(pos.x * negValx, 0f, pos.z * negValz);
            //Debug.Log("reached here final");
        }
    }
}