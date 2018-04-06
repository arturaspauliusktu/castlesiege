using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Commands : MonoBehaviour {

    private Unit[] A_Units;
    private Unit[] D_Units;
    private Unit Door;
    private Unit King;
    private bool isReady = false;
    private bool ini = false;
    private bool ini1 = false;
    private int Count = 0;


    public Objective stage = Objective.s1;

    public enum Objective { s1, s2, s3, s4 }

    public GameObject waypoint1;
    public GameObject waypoint2;
    public GameObject waypoint3;
    public GameObject waypoint4;

    // Use this for initialization
    void Awake () {
        A_Units = GameObject.FindGameObjectsWithTag("Attacker").Select(x => x.GetComponent<Unit>()).ToArray();
        D_Units = GameObject.FindGameObjectsWithTag("Defender").Select(x => x.GetComponent<Unit>()).ToArray();

        Door = GameObject.FindGameObjectsWithTag("Door")[0].GetComponent<Unit>();
        King = GameObject.FindGameObjectsWithTag("King")[0].GetComponent<Unit>();

    }


    private void Update()
    {
        if (!isReady)
        {
            isReady = true;
            return;
        }

        if (!ini1)
        {
            foreach (Unit unit in D_Units)
            {
                unit.Guard();
            }
            ini1 = true;
        }

        switch (stage)
        {
            case Objective.s1:
                if (Count <= A_Units.Length)
                {
                    ini = true;
                    foreach (Unit unit in A_Units)
                    {
                        unit.ExecuteCommand(new AICommand(AICommand.CommandType.GoToAndGuard, waypoint1.transform.position));
                    }

                    Count++;
                }
                foreach(Unit unit in A_Units)
                {
                    if (unit.state != Unit.UnitState.Guarding)
                        return;
                }
                stage = Objective.s2;
                Count = 0;
                break;
            case Objective.s2:
                if (Count <= A_Units.Length * 10)
                {
                    foreach (Unit unit in A_Units)
                    {
                        if (!unit.ifPathEexists(waypoint2.transform.position))
                        {
                            return;
                        }
                        unit.ExecuteCommand(new AICommand(AICommand.CommandType.GoToAndGuard, waypoint2.transform.position));
                    }
                    Count++;
                }

                foreach (Unit unit in A_Units)
                {
                    if(unit.agent.remainingDistance >= 40f)
                        unit.ExecuteCommand(new AICommand(AICommand.CommandType.GoToAndGuard, waypoint2.transform.position));
                }


                foreach (Unit unit in D_Units)
                {
                    if (unit.state != Unit.UnitState.Dead)
                        return;
                }
                stage = Objective.s3;
                Count = 0;

                
                break;
            case Objective.s3:
                if (Count <= A_Units.Length)
                {
                    foreach (Unit unit in A_Units)
                    {
                        if(unit.state != Unit.UnitState.Dead)
                            unit.ExecuteCommand(new AICommand(AICommand.CommandType.GoToAndGuard, waypoint3.transform.position));
                    }
                    Count++;
                    return;
                }

                foreach (Unit unit in A_Units)
                {
                    if (unit.state != Unit.UnitState.Guarding)
                        return;
                       
                }

                foreach (Unit unit in A_Units)
                {
                    if (unit.state != Unit.UnitState.Dead)
                        unit.ExecuteCommand(new AICommand(AICommand.CommandType.AttackTarget, Door));
                }

                if (Door.state != Unit.UnitState.Dead)
                    return;
                stage = Objective.s4;
                Count = 0;
                break;
            case Objective.s4:
                if (Count <= A_Units.Length)
                {
                    foreach (Unit unit in A_Units)
                    {
                        unit.ExecuteCommand(new AICommand(AICommand.CommandType.AttackTarget, waypoint4.transform.position));
                    }
                    Count++;
                    return;
                }

                foreach (Unit unit in A_Units)
                {
                    if (unit.state != Unit.UnitState.Guarding)
                        return;

                }

                foreach (Unit unit in A_Units)
                {
                    if (unit.state != Unit.UnitState.Dead)
                        unit.ExecuteCommand(new AICommand(AICommand.CommandType.AttackTarget, King));
                }
                break;
        }

    }
}
