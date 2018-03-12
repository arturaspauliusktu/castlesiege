using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

    public Shooting shot;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            shot.isFiring = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            shot.isFiring = false;
        }
	}
}
