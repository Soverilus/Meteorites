using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestructImmediate : MonoBehaviour {
	void Awake () {
        Destroy(gameObject);
	}
}
