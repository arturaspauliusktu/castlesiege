using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildableObject : MonoBehaviour {
    
    private GameObject buildableBox;
    
    // Use this for initialization
    void Start () {
        buildableBox = GameObject.FindGameObjectWithTag("buildableBox");
    }
	
	// Update is called once per frame
	void Update () {
     
    }


    public void setUnbuildable()
    {
        Material buildDenied = Resources.Load("Materials/BuildMaterials/BuildDenied", typeof(Material)) as Material;
        buildableBox.GetComponent<Renderer>().material = buildDenied;
    }

    public void setBuildable()
    {
        Material buildAlowed = Resources.Load("Materials/BuildMaterials/BuildAlowed", typeof(Material)) as Material;
        buildableBox.GetComponent<Renderer>().material = buildAlowed;
    }

    void OnTriggerEnter(Collider collision)
    {
        Debug.Log("ENTER");
        Material buildDenied = Resources.Load("Materials/BuildMaterials/BuildDenied", typeof(Material)) as Material;
        buildableBox.GetComponent<Renderer>().material = buildDenied;
    }

    void OnTriggerExit(Collider collision)
    {
        Debug.Log("ENTER");
        Material buildDenied = Resources.Load("Materials/BuildMaterials/BuildAlowed", typeof(Material)) as Material;
        buildableBox.GetComponent<Renderer>().material = buildDenied;
        Debug.Log("ENTER");
    }

    void OnTriggerStay(Collider collision)
    {
        Debug.Log("ENTER");
        Material buildDenied = Resources.Load("Materials/BuildMaterials/BuildDenied", typeof(Material)) as Material;
        buildableBox.GetComponent<Renderer>().material = buildDenied;
        Debug.Log("ENTER");
    }

}
