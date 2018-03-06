using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClickMovement : MonoBehaviour {

    public LayerMask clickableArea;

    private NavMeshAgent myAgent;
	
    // Use this for initialization
	void Start () {
        myAgent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0))
        {
            Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(myRay, out hitInfo, 100, clickableArea))
            {
                myAgent.SetDestination(hitInfo.point);
            }
        }
	}
}
