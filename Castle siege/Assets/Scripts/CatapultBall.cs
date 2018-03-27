using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatapultBall : MonoBehaviour {

    public Rigidbody cannonballPrefab;
    public Transform spawnPoint;
    public float force;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("e"))
        {
            FireCannonball();
        }
	}

    void FireCannonball()
    {
        Rigidbody cannonball = (Rigidbody)Instantiate(cannonballPrefab, spawnPoint.position, spawnPoint.rotation);
        cannonball.AddForce(spawnPoint.forward * force);
    }
}
