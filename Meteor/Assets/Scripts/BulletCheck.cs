using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCheck : MonoBehaviour {
    public int damage = 50;
    public float lifeTime;
    float timer;

    private void Update() {
        timer += Time.deltaTime;
        if (lifeTime <= timer) {
            Destroy(gameObject);
        }
    }

    void FadeDestroy() {
        //TODO
    }
}
