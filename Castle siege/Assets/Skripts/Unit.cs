using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

    public int speed = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(transform.position.z <= 55f && transform.position.z >= -55f)
        {
            transform.Translate(0f, 0f, Input.GetAxis("Horizontal") * Time.deltaTime * speed);
        }
        else if(transform.position.z >= 55f)
        {
            transform.Translate(0f, 0f, Time.deltaTime * -speed);
        }

        else if(transform.position.z <= -55f)
        {
            transform.Translate(0f, 0f, Time.deltaTime * speed);
        }
	}
}
