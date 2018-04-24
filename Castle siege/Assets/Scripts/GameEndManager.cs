using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEndManager : MonoBehaviour {

    public float restarDelay = 5;
    Animator anim;
    float timer;
    GameObject king;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        king = GameObject.FindGameObjectWithTag("King");
	}
	
	// Update is called once per frame
	void Update () {
        if (king.GetComponent<Unit>().stats.health <= 0)
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
