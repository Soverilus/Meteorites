using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrap : MonoBehaviour {

    Collider myCol;
    float maxX;
    float maxZ;
    float minX;
    float minZ;

    public List<GameObject> myObjects;

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

    private void OnTriggerStay(Collider other) {
        if (!myObjects.Contains(other.gameObject)) {
            myObjects.Add(other.gameObject);
            if (other.gameObject.GetComponent<WrapChecker>() != null) {
                WrapChecker checkWrap = other.gameObject.GetComponent<WrapChecker>();
                checkWrap.GetRangeExtent(maxX, minX, maxZ, minZ);
            }
        }
    }

    private void Update() {
        foreach (GameObject obj in myObjects) {
            Vector3 pos = obj.transform.position;
            if (pos.x > maxX || pos.x < minX || pos.z > maxZ || pos.z < minZ) {
                if (obj.GetComponent<WrapChecker>() != null) {
                    Renderer objRen = obj.GetComponent<Renderer>();
                    WrapChecker checkWrap = obj.GetComponent<WrapChecker>();
                    if (objRen.isVisible) {
                        checkWrap.isInvisible = false;
                    }
                    if (!objRen.isVisible && checkWrap.isInvisible == false) {
                        checkWrap.isInvisible = true;
                        obj.transform.position = new Vector3(pos.x * checkWrap.negValx, 0f, pos.z * checkWrap.negValz);
                    }
                }
            }
        }
    }
}