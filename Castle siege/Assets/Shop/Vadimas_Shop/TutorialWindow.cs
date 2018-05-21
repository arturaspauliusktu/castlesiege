using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialWindow : MonoBehaviour {

    public bool isImageOn;
    public GameObject tutorialWndow;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        ChooseOnOff();
	}

    public void ChooseOnOff()
    {
        if (isImageOn)
        {
            ShowTutorialWindow();
        }
        else
        {
            HideTutorialWindow();
        }	
    }

    public void ShowTutorialWindow()
    {
        isImageOn = true;
        tutorialWndow.SetActive(true);
        Time.timeScale = 0f;
    }

    public void HideTutorialWindow()
    {
        isImageOn = false;
        tutorialWndow.SetActive(false);
        Time.timeScale = 1f;
    }
}