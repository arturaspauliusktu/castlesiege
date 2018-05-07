using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class wallButton : MonoBehaviour, IPointerClickHandler
{

    WallPlacer1 WP;

	// Use this for initialization
	void Start () {
        WP = FindObjectOfType<WallPlacer1>();
        Debug.Log(WP);
        //WP.enableBuild(); 
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("dsaa");
        // OnClick code goes here ...
    }
}
