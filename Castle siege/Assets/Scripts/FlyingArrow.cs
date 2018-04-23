using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingArrow : MonoBehaviour {

    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent <Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        transform.rotation = Quaternion.LookRotation(rb.velocity);
	}
}
