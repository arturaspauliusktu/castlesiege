using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamWievSideways : MonoBehaviour {

    public float speed;

    int castleSideX;
    int barackSideX;
    bool moveToBaracks;
    bool moveToCastle;
    bool rotateLeft;
    bool rotateRight;
    float currentSightX;

	// Use this for initialization
	void Start () {
        castleSideX = -215;
        barackSideX = 1150;
        moveToBaracks = false;
        moveToCastle = false;
        rotateLeft = false;
        rotateRight = false;
        currentSightX = 0;
    }
	
	// Update is called once per frame
	void Update () {

        transform.LookAt(new Vector3(currentSightX, 0, 0));

        if (Input.GetKeyDown("w"))
        {
            moveToBaracks = true;
        }

        if (Input.GetKeyUp("w"))
        {
            moveToBaracks = false;
        }
        
        if (Input.GetKeyDown("s"))
        {
            moveToCastle = true;
        }

        if (Input.GetKeyUp("s"))
        {
            moveToCastle = false;
        }

        if (Input.GetKeyDown("q"))
        {
            rotateLeft = true;
        }

        if (Input.GetKeyUp("q"))
        {
            rotateLeft = false;
        }

        if (Input.GetKeyDown("e"))
        {
            rotateRight = true;
        }

        if (Input.GetKeyUp("e"))
        {
            rotateRight = false;
        }

        if (moveToBaracks)
        {
            float xOld = transform.position.x;
            transform.position = Vector3.Lerp(transform.position, new Vector3(barackSideX, transform.position.y, transform.position.z), Time.deltaTime * speed);
            currentSightX += transform.position.x - xOld;
        }

        if (moveToCastle)
        {
            float xOld = transform.position.x;
            transform.position = Vector3.Lerp(transform.position, new Vector3(castleSideX, transform.position.y, transform.position.z), Time.deltaTime * speed);
            currentSightX += transform.position.x - xOld;
        }

        if (rotateLeft)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed * 400);
        }

        if (rotateRight)
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed * 400);
        }
    }

    private void FixedUpdate()
    {
    }
}
