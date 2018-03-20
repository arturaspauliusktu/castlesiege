using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

    public int MaxUnitHealth = 100;
    public int UnitHealth = 100;
    Unit_Health health;
    Rigidbody rb;

    // Use this for initialization
    void Start () {
        health = new Unit_Health(MaxUnitHealth, UnitHealth);
    }

    private void OnCollisionEnter(Collision collision)
    {

        if(collision.impactForceSum.magnitude > 10.0f)
        {
            Debug.Log(health.getHealth());

            if (health.giveDamage(10))
            {
                // Dabar tai tik pritaikyta katapultai, bet kiekvienas unitas turetu tureti savo mirimo mechaminzma.
                Debug.Log(health.getHealth());
                foreach (Transform child in transform)
                {
                    child.gameObject.AddComponent<Rigidbody>();
                    Debug.Log(child);
                }
                Destroy(transform.gameObject, 5);
            }
        }

    }

    // Update is called once per frame
    void Update () {
        UnitHealth = health.getHealth();
	}
}
