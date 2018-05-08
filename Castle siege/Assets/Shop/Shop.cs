using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	public void onClickAnim(GameObject image)
    {
        image.GetComponent<Image>().color = Color.green;
    }
}
