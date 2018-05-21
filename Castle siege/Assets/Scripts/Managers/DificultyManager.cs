using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DificultyManager : MonoBehaviour {

    public static int Dificulty;
    public static int DificultyIncrease;
    public static int WaveCount;
    public Text WaveText;
    int enemeysInWave;
    int enemeysUntilDificultyIncrease;
    public int currentWave;
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
            if(currentWave == WaveCount)
            {
                endSpawning();
            }
            else
            {
                Dificulty += DificultyIncrease;
                changeDificulty();
            }
        }
    }

    void changeDificulty()
    {
        ES.ResetDificulty(60 / Dificulty + 1, 40 / Dificulty + 1, 30 / Dificulty + 1);
        enemeysInWave = Dificulty * 2;
        enemeysUntilDificultyIncrease += enemeysInWave;
        currentWave++;
    }

    void endSpawning()
    {
        ES.ResetDificulty(999, 999, 999);
    }

    public int GetWaveCount()
    {
        return WaveCount;
    }

    public void SetDificulty(int dificulty, int increase, int count)
    {
        Dificulty = dificulty;
        DificultyIncrease = increase;
        WaveCount = count;
    }
}
