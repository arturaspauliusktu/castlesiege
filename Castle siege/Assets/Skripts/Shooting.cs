using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour {

    public bool isFiring;

    //public Projectile projectile;
    public float projectileSpeed;

    public float timeBetweenShots;
    private float shotCounter;

    public Transform spawnPoint;

    public Rigidbody project;
    public float force;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (isFiring)
        {
            shotCounter -= Time.deltaTime;
            if (shotCounter <= 0)
            {
                shotCounter = timeBetweenShots;
                Rigidbody newProject = (Rigidbody)Instantiate(project, spawnPoint.position, spawnPoint.rotation);
                GameObject gun = GameObject.FindGameObjectsWithTag("gun")[0];
                //newProject.speed = projectileSpeed;
                newProject.AddForce(gun.transform.forward * force);
            }
        }
        else
        {
            shotCounter = 0;
        }
	}
}
