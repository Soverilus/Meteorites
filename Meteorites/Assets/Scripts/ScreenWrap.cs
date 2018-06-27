using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrap : MonoBehaviour {
    Collider myCol;
    float maxX;
    float maxZ;
    float minX;
    float minZ;

    private void Start() {
        myCol = GetComponent<Collider>();
        GetExtents();
    }

    void GetExtents() {
        maxX = myCol.bounds.extents.x;
        maxZ = myCol.bounds.extents.z;
        minX = -myCol.bounds.extents.x;
        minZ = -myCol.bounds.extents.z;
    }

    private void OnTriggerExit(Collider other) {
        float negX = 1f;
        float negZ = 1f;
        Vector3 p = other.transform.position;
        if (p.x < maxX && p.x > minX) {
            negZ = -1f;
        }
        if (p.y < maxZ && p.y > minZ) {
            negX = -1f;
        }
        else {
            Debug.Log("HOW THE FUCK DID YOU EVEN DO THAT YOU FUCKING TWIT");
        }
        other.transform.position = new Vector3(p.x * negX, 0f, p.z * negZ);
    }
}