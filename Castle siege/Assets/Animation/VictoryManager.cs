using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryManager : MonoBehaviour {

    public float restartDelay = 5;
    Animator anim;
    float restarTimer;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("v"))
        {
            anim.SetTrigger("Victory");

            restarTimer += Time.deltaTime;

            if(restarTimer >= restartDelay)
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }	
    }
}
