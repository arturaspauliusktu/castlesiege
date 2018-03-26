using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEndManager : MonoBehaviour {

    public float restarDelay = 5;
    Animator anim;
    float timer;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("p"))
        {
            anim.SetTrigger("GameEnd");

            timer += Time.deltaTime;
            if (timer >= restarDelay)
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }
	}
}
