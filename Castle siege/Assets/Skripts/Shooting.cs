using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour {

    public Animator animation;
    
    //public Projectile projectile;
    public float projectileSpeed;

    public float timeBetweenShots;

    public Transform spawnPoint;

    public Rigidbody project;
    public float force;

	// Use this for initialization
	void Start () {
        animation = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.DownArrow))
            force -= 1000;
        if (Input.GetKey(KeyCode.UpArrow))
            force += 1000;
        if (!animation.GetCurrentAnimatorStateInfo(0).IsName("fire") && Input.GetKey(KeyCode.Space))
        {
            animation.Play("fire");
            Rigidbody newProject = (Rigidbody)Instantiate(project, spawnPoint.position, spawnPoint.rotation);
            GameObject gun = GameObject.FindGameObjectsWithTag("gun")[0];
            newProject.AddForce(gun.transform.forward * force);
        }
	}
}
