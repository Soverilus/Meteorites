using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrapChecker : MonoBehaviour {
    public bool isInvisible = true;

    public float topX;
    public float topZ;
    public float botX;
    public float botZ;

    public int negValx;
    public int negValz;

    public void GetRangeExtent(float maX, float miX, float maZ, float miZ) {
        topX = maX;
        topZ = maZ;
        botX = miX;
        botZ = miZ;
    }
    private void Update() {
        if (transform.position.x >= topX || transform.position.x <= botX) {
            negValx = -1;
        }
        else {
            negValx = 1;
        }

        if (transform.position.z >= topZ || transform.position.z <= botZ) {
            negValz = -1;
        }
        else {
            negValz = 1;
        }
    }
}
