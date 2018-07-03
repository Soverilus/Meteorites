using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMeteorite : MonoBehaviour {
    public GameObject[] Meteorites;

    public void SpawnOne(int meteorType, Vector3 pos) {
        GameObject newMeteor = Instantiate(Meteorites[meteorType], pos, Quaternion.identity);
        MeteorBehaviour mScript = newMeteor.GetComponent<MeteorBehaviour>();
        mScript.StartingVelocity(-transform.position);
    }
}
