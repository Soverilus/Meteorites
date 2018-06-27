using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMeteorite : MonoBehaviour {
    public GameObject[] Meteorites;

    public void SpawnOne(int meteorType, Vector3 pos) {
        Instantiate(Meteorites[meteorType - 1], pos, Quaternion.identity);
    }
}
