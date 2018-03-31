using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

    public int MaxUnitHealth = 100;
    public int UnitHealth = 100;
    bool isAlive = true;
    Unit_Health health;
    Break br;

    // Use this for initialization
    void Start () {
        health = new Unit_Health(MaxUnitHealth, UnitHealth);
        br = gameObject.GetComponent<Break>();
    }

    private void OnCollisionEnter(Collision collision)
    {

        if(collision.relativeVelocity.magnitude > 10.0f)
        {
            Debug.Log(health.getHealth());

            if (health.giveDamage(10) && isAlive)
            {
                isAlive = false;
                br.StartCoroutine("BreakObject");
                Debug.Log(gameObject.name + " - Dead");
            }
        }

    }

    // Update is called once per frame
    void Update () {
        UnitHealth = health.getHealth();
	}
}
