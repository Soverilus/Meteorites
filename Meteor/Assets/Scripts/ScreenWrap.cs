using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrap : MonoBehaviour {

    SpawnMeteorite spawnOne;
    Collider myCol;
    float maxX;
    float maxZ;

    Vector3[] spawnExtents = new Vector3[4];

    public List<GameObject> myMeteors;

    private void Start() {
        myCol = GetComponent<Collider>();
        GetExtents();
        GetSpawnExtents();
        spawnOne = GetComponent<SpawnMeteorite>();
        MakeMoreMeteor();
    }

    void GetExtents() {
        maxX = myCol.bounds.extents.x;
        maxZ = myCol.bounds.extents.z;
    }
    void GetSpawnExtents() {
        spawnExtents[0] = new Vector3(-maxX, 0, maxZ);
        spawnExtents[1] = new Vector3(maxX, 0, maxZ);
        spawnExtents[2] = new Vector3(maxX, 0, -maxZ);
        spawnExtents[3] = new Vector3(-maxX, 0, -maxZ);
    }

    private void OnTriggerStay(Collider other) {
        if (!myMeteors.Contains(other.gameObject)) {
            myMeteors.Add(other.gameObject);
            if (other.gameObject.GetComponent<WrapChecker>() != null) {
                WrapChecker checkWrap = other.gameObject.GetComponent<WrapChecker>();
                checkWrap.GetRangeExtent(maxX, -maxX, maxZ, -maxZ);
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
                if (pos.x >= maxX || pos.x <= -maxX || pos.z >= maxZ || pos.z <= -maxZ) {
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
    public void MakeMoreMeteor() {
        Vector3 pos;
        pos = new Vector3(Random.Range(spawnExtents[0].x, spawnExtents[1].x), 0f, spawnExtents[0].z); ;
        int upRightDownLeft = Random.Range(0, 4);
        int meteorType = Random.Range(1, 3);
        if (upRightDownLeft == 0) {
            pos = new Vector3(Random.Range(spawnExtents[0].x, spawnExtents[1].x), 0f, spawnExtents[0].z);
        }
        else if (upRightDownLeft == 1) {
            pos = new Vector3(spawnExtents[1].x, 0f, Random.Range(spawnExtents[2].z, spawnExtents[1].z));
        }
        else if (upRightDownLeft == 2) {
            pos = new Vector3(Random.Range(spawnExtents[2].x, spawnExtents[3].x), 0f, spawnExtents[2].z);
        }
        else if (upRightDownLeft == 3) {
            pos = new Vector3(spawnExtents[3].x, 0f, Random.Range(spawnExtents[0].z, spawnExtents[3].z));
        }
        else {
            Debug.Log("Error: ScreenWrap spawn extents reporting value above maximum");
        }
        spawnOne.SpawnOne(meteorType, pos);
        Debug.Log(pos);
    }
}