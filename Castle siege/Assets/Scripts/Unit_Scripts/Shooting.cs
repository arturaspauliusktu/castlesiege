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

    private bool isReady;

	// Use this for initialization
	void Start () {
        animation = GetComponent<Animator>();
        isReady = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.DownArrow))
            force -= 1000;
        if (Input.GetKey(KeyCode.UpArrow))
            force += 1000;
        if (Input.GetKeyDown(KeyCode.Space) && isReady == true)
        {
            Shoot();
        }
	}

    public void Shoot()
    {
        isReady = false;
        animation.SetTrigger("Shoot");
        animation.ResetTrigger("Shoot");
        animation.Play("catapult_lose");
        Rigidbody newProject = (Rigidbody)Instantiate(project, spawnPoint.position, spawnPoint.rotation);
        GameObject gun = GameObject.FindGameObjectsWithTag("gun")[0];
        newProject.AddForce(new Vector3(transform.parent.gameObject.transform.rotation.x, transform.parent.gameObject.transform.rotation.y, transform.parent.gameObject.transform.rotation.z) * force, ForceMode.VelocityChange);
    }

    public void setReady()
    {
        isReady = true;
    }
}
