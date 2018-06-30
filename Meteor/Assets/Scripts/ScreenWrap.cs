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
        for (int i = 0; i < myObjects.Count; i++) {
            if (myObjects[i] == null) {
                myObjects.Remove(myObjects[i]);
            }
            else {
                //Debug.Log("reached here5 + " + i);
                GameObject obj = myObjects[i];
                Vector3 pos = obj.transform.position;
                if (pos.x >= maxX || pos.x <= minX || pos.z >= maxZ || pos.z <= minZ) {
                    //Debug.Log("reached here4");
                    if (obj.GetComponent<WrapChecker>() != null) {
                        WrapChecker checkWrap = obj.GetComponent<WrapChecker>();
                        checkWrap.WrapAround(pos);
                        //Debug.Log("reached here3");
                    }
                }
            }
        }
    }
}