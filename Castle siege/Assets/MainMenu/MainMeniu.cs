using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMeniu : MonoBehaviour {

    public string gameScene;
    DificultyManager DM;

	// Use this for initialization
	void Start () {
        DM = GameObject.FindObjectOfType<DificultyManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetEasy()
    {
        DM.SetDificulty(5, 5, 2);
    }

    public void SetMedium()
    {
        DM.SetDificulty(10, 10, 4);
    }

    public void SetHard()
    {
        DM.SetDificulty(15, 15, 8);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(gameScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
