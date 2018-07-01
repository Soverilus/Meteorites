using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorBehaviour : MonoBehaviour {
    [SerializeField]
    float speed;

    public float dist;

    [SerializeField]
    int meteorLevel;
    [SerializeField]
    GameObject meteorSubtype;

    [SerializeField]
    int maxHealth;
    [SerializeField]
    int curHealth;

    [HideInInspector]
    public Rigidbody myRB;

    void Awake() {
        myRB = GetComponent<Rigidbody>();
        curHealth = maxHealth;
    }

    void Update() {
        if (myRB.velocity.magnitude <= 0.0001f) {
            StartingVelocity(new Vector3(Random.Range(-100f, 100f), 0f, Random.Range(-100f, 100f)));
        }
    }

    public void StartingVelocity(Vector3 i) {
        i = new Vector3(i.x, 0f, i.z);
        Vector3 dir = i.normalized;
        myRB.velocity = dir * speed;
        myRB.AddTorque(new Vector3(Random.Range(-150f * myRB.mass, 150f * myRB.mass), Random.Range(-150f * myRB.mass, 150f * myRB.mass), Random.Range(-150f * myRB.mass, 150f * myRB.mass)));
    }
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.GetComponent<MeteorBehaviour>() != null) {
            MeteorBehaviour otherScript = other.gameObject.GetComponent<MeteorBehaviour>();
            otherScript.DamageMe(maxHealth / 2, transform, false);
        }
        if (other.gameObject.GetComponent<PlayerBehaviour>() != null) {
            PlayerBehaviour playerScript = other.gameObject.GetComponent<PlayerBehaviour>();
            playerScript.DamageMe(transform, false);
        }
    }

    public void DamageMe(int dmg, Transform other, bool playerShot) {
        curHealth -= dmg;
        if (curHealth <= 0) {
            if (playerShot) {
                SplitAttack(other);
            }
            else {
                SplitMore(other);
            }
            Destroy(gameObject);
        }
    }

    void SplitMore(Transform otherPos) {
        if (meteorLevel != 0) {
            GameObject newMeteor0 = Instantiate(meteorSubtype, transform.position + otherPos.transform.right * dist, Quaternion.identity);
            GameObject newMeteor1 = Instantiate(meteorSubtype, transform.position + otherPos.transform.right * -dist, Quaternion.identity);
            newMeteor0.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            newMeteor1.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            MeteorBehaviour nM0 = newMeteor0.GetComponent<MeteorBehaviour>();
            MeteorBehaviour nM1 = newMeteor1.GetComponent<MeteorBehaviour>();
            Rigidbody otherRB = otherPos.gameObject.GetComponent<Rigidbody>();
            otherPos.gameObject.GetComponent<MeteorBehaviour>().myRB = otherRB;
            Vector3 combDir = new Vector3(otherRB.velocity.x + myRB.velocity.x, 0, otherRB.velocity.z + myRB.velocity.z).normalized;
            nM0.StartingVelocity(myRB.velocity);
            nM1.StartingVelocity(combDir);
        }
    }
    void SplitAttack(Transform otherPos) {
        if (meteorLevel != 0) {
            GameObject newMeteor0 = Instantiate(meteorSubtype, transform.position + otherPos.transform.right * dist, Quaternion.identity);
            GameObject newMeteor1 = Instantiate(meteorSubtype, transform.position + otherPos.transform.right * -dist, Quaternion.identity);
            newMeteor0.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            newMeteor1.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            MeteorBehaviour nM0 = newMeteor0.GetComponent<MeteorBehaviour>();
            MeteorBehaviour nM1 = newMeteor1.GetComponent<MeteorBehaviour>();
            Vector3 combDir = otherPos.gameObject.GetComponent<Rigidbody>().velocity;
            nM0.StartingVelocity(myRB.velocity);
            nM1.StartingVelocity(combDir);
        }
    }
}
