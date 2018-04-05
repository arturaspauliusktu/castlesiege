using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Commands : MonoBehaviour {

    private Unit[] A_Units;
    private Unit[] D_Units;
    private bool isReady = false;
    private bool ini = false;

    public GameObject waypoint1;
    public GameObject waypoint2;

	// Use this for initialization
	void Awake () {
        A_Units = GameObject.FindGameObjectsWithTag("Attacker").Select(x => x.GetComponent<Unit>()).ToArray();
        D_Units = GameObject.FindGameObjectsWithTag("Defender").Select(x => x.GetComponent<Unit>()).ToArray();
    }


    private void Update()
    {
        if (!isReady)
        {
            isReady = true;
            return;
        }
        
        if (!ini) {
            foreach (Unit unit in D_Units)
            {
                unit.Guard();
            }

            foreach (Unit unit in A_Units)
            {
                unit.ExecuteCommand(new AICommand(AICommand.CommandType.GoToAndGuard, waypoint2.transform.position));
            }
            ini = true;
        }

    }
}
