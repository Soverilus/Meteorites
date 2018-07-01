using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrap : MonoBehaviour {

    Collider myCol;
    float maxX;
    float maxZ;
    float minX;
    float minZ;

    public List<GameObject> myMeteors;

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
        if (!myMeteors.Contains(other.gameObject)) {
            myMeteors.Add(other.gameObject);
            if (other.gameObject.GetComponent<WrapChecker>() != null) {
                WrapChecker checkWrap = other.gameObject.GetComponent<WrapChecker>();
                checkWrap.GetRangeExtent(maxX, minX, maxZ, minZ);
            }
        }
    }

    private void Update() {
        for (int i = 0; i < myMeteors.Count; i++) {
            if (myMeteors[i] == null) {
                myMeteors.Remove(myMeteors[i]);
            }
            else {
                //Debug.Log("reached here5 + " + i);
                GameObject obj = myMeteors[i];
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