using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public float radius = 10;
    public Transform spawnpoint;
    private System.Random random;

    public GameObject archer;
    public GameObject knight;
    public GameObject fighter;
    public GameObject catapult;

    #region singleton

    public static EnemySpawner instance;

    private void Awake()
    {
        instance = this;
        random = new System.Random();
        
    }

    #endregion

    private void Start()
    {
        random = new System.Random();
        //InvokeRepeating("autoSpawnArchers", ArcherSpawnSpeed, ArcherSpawnSpeed);
        //InvokeRepeating("autoSpawnFighter", FighterSpawnSpeed, FighterSpawnSpeed);
        //InvokeRepeating("autoSpawnKnight", KnightSpawnSpeed, KnightSpawnSpeed);
    }

    public void ResetDificulty(int archerSpawnSpeed, int fighterSpawnSpeed, int knightSpawnSpeed)
    {
        CancelInvoke();
        InvokeRepeating("autoSpawnArchers", archerSpawnSpeed, archerSpawnSpeed);
        InvokeRepeating("autoSpawnFighter", fighterSpawnSpeed, fighterSpawnSpeed);
        InvokeRepeating("autoSpawnKnight", knightSpawnSpeed, knightSpawnSpeed);
    }

    public bool SpawnUnit(GameObject unit)
    {

        GameObject obj = Instantiate(unit, spawnpoint.position + RandomPoint(), spawnpoint.rotation);
        //2-handed warior unit spalvinimas raudonai
        if(obj.transform.GetChild(1).GetComponent<Renderer>())
            obj.transform.GetChild(1).GetComponent<Renderer>().material.color = Color.red;
        obj.SetActive(true);
        return true;
    }

    private void autoSpawnArchers()
    {
        SpawnUnit(archer);
    }

    private void autoSpawnKnight()
    {
        SpawnUnit(knight);
    }

    private void autoSpawnFighter()
    {
        SpawnUnit(fighter);
    }
    private void autoSpawnCatapult()
    {
        SpawnUnit(catapult);
    }

    private Vector3 RandomPoint()
    {
        float r = (float)random.NextDouble()*radius - radius;
        float x = (float)random.NextDouble() * r - r;
        float t = (float)Math.Acos(x / r);
        float y = r * (float)Math.Sin(t);

        return new Vector3(x, 0, y);

    }
}
