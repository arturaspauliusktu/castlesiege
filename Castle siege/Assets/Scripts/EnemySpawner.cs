using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {


    public float radius = 10;
    public Transform spawnpoint;
    private System.Random random;

    #region singleton

    public static EnemySpawner instance;

    private void Awake()
    {
        instance = this;
        random = new System.Random();
        
    }

    #endregion

    public bool SpawnUnit(GameObject unit)
    {

        Instantiate(unit, spawnpoint.position + RandomPoint(), spawnpoint.rotation);

        return true;
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
