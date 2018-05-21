using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public string mainMenuScene;
    public GameObject pauseMenu;
    public bool isPaused;
    CurrencyManager CM;
    WallButton WB;
    TrapButton TB;
    WariorButton WRB;

	// Use this for initialization
	void Start () {
        CM = GameObject.FindObjectOfType<CurrencyManager>();
        WB = GameObject.FindObjectOfType<WallButton>();
        TB = GameObject.FindObjectOfType<TrapButton>();
        WRB = GameObject.FindObjectOfType<WariorButton>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
	}

    public void ResumeGame()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        CM.StartCurrencyIncrease();
        WB.EnableButton();
        TB.EnableButton();
        WRB.EnableButton();
    }

    public void PauseGame()
    {
        isPaused = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        CM.StopCurrencyIncrease();
        WB.DisableButton();
        TB.DisableButton();
        WRB.DisableButton();
    }
    
    public void RestartGame()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void QuitToMain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuScene);
    }
}
