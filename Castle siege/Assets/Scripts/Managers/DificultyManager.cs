using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DificultyManager : MonoBehaviour {

    public int Dificulty;
    public int DificultyIncrease;
    public int WaveCount;
    public Text WaveText;
    int enemeysInWave;
    int enemeysUntilDificultyIncrease;
    int currentWave;
    CurrencyManager CM;
    EnemySpawner ES;
    KillCounter KC;

	// Use this for initialization
	void Start ()
    {
        CM = GameObject.FindObjectOfType<CurrencyManager>();
        ES = GameObject.FindObjectOfType<EnemySpawner>();
        KC = GameObject.FindObjectOfType<KillCounter>();
        currentWave = 0;
        changeDificulty();
        enemeysUntilDificultyIncrease = enemeysInWave;
        WaveText.text = "Wave: " + currentWave + "/" + WaveCount + "    Enemies: " + (KC.getAttackers() + enemeysInWave - enemeysUntilDificultyIncrease) + "/" + enemeysInWave;
    }
	
	// Update is called once per frame
	void Update ()
    {
        WaveText.text = "Wave: " + currentWave + "/" + WaveCount + "    Enemies: " + (KC.getAttackers() + enemeysInWave - enemeysUntilDificultyIncrease) + "/" + enemeysInWave;
        if (KC.getAttackers() > enemeysUntilDificultyIncrease)
        {
            Dificulty += DificultyIncrease;
            changeDificulty();
        }
    }

    void changeDificulty()
    {
        ES.ResetDificulty(60 / Dificulty + 1, 40 / Dificulty + 1, 30 / Dificulty + 1);
        enemeysInWave = Dificulty * 2;
        enemeysUntilDificultyIncrease += enemeysInWave;
        currentWave++;
    }
}
