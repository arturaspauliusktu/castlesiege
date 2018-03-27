using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEndManager : MonoBehaviour {

    public float restarDelay = 5;
    Animator anim;
    float timer;
    GameObject door;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        door = GameObject.FindGameObjectsWithTag("Door")[0];
	}
	
	// Update is called once per frame
	void Update () {
        if (door.GetComponent<Unit>().UnitHealth < 100)
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
