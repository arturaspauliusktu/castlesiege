using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour {

    public bool isFiring;

    public Projectile projectile;
    public float projectileSpeed;

    public float timeBetweenShots;
    private float shotCounter;

    public Transform spawnPoint;

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
                Projectile newProject = (Projectile)Instantiate(projectile, spawnPoint.position, spawnPoint.rotation);
                newProject.speed = projectileSpeed;
            }
        }
        else
        {
            shotCounter = 0;
        }
	}
}
