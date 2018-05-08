using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEndManager : MonoBehaviour {

    public float restarDelay = 5;
    Animator anim;
    float timer;
    public Unit king;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (king.stats.health <= 0)
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
