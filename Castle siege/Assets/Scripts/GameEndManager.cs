using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEndManager : MonoBehaviour {

    public float restarDelay = 5;
    Animator anim;
    float timer;
    public Unit king;
    KillCounter KC;
    DificultyManager DM;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        KC = FindObjectOfType<KillCounter>();
        DM = FindObjectOfType<DificultyManager>();
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

        int i = KC.getAliveAttackers();
        if( KC.getAliveAttackers() <= 0 && DM.currentWave == DM.GetWaveCount())
        {
            anim.SetTrigger("Victory");

            timer += Time.deltaTime;
            if (timer >= restarDelay)
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }
	}
}
