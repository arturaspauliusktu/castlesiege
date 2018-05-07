using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Commands : MonoBehaviour {

    private bool isReady = false;
    private bool ini = false;
    private bool ini1 = false;
    private bool ini2 = false;
    private int Count = 0;


    public Objective stage = Objective.s1;
    public Unit unit;

    public enum Objective { s1, s2, s3, s4 }

    public GameObject waypoint1;
    public GameObject waypoint2;
    public GameObject waypoint3;
    public GameObject waypoint4;
    public GameObject waypoint5;


    private void Start()
    {
        unit = GetComponent<Unit>();
    }

    private void Update()
    {
        WarriorMission();

    }


    private void WarriorMission()
    {
        if (!isReady)
        {
            isReady = true;
            return;
        }

        if (!ini1)
        {
            unit.Guard();
            ini1 = true;
        }

        switch (stage)
        {
            case Objective.s1:
                if (!ini)
                {
                    unit.ExecuteCommand(new AICommand(AICommand.CommandType.GoToAndGuard, waypoint1.transform.position));
                    ini = true;
                }
                    

                bool allSet = true;
                if (unit.state != Unit.UnitState.Guarding && !Unit.IsDeadOrNull(unit))
                    allSet = false;
                if (!allSet) return;
                stage = Objective.s2;
                ini = false;
                break;
            case Objective.s2:

                if (!unit.ifPathEexists(waypoint2.transform.position))
                    return;
                if (!ini)
                {
                    if (unit.stats.unitType == UnitStats.UnitType.Archer)
                        unit.ExecuteCommand(new AICommand(AICommand.CommandType.GoToAndGuard, waypoint5.transform.position));
                    else
                        unit.ExecuteCommand(new AICommand(AICommand.CommandType.GoToAndGuard, waypoint2.transform.position));
                    ini = true;
                }




                foreach (Unit unit in UnitManager.instance.DefenderUnits)
                {
                    if (Unit.IsDeadOrNull(unit))
                        return;
                }

                stage = Objective.s3;
                ini = false;


                break;
            case Objective.s3:
                if (!ini)
                {
                    unit.ExecuteCommand(new AICommand(AICommand.CommandType.GoToAndGuard, waypoint3.transform.position));
                    ini = true;
                }

                if (unit.stats.unitType == UnitStats.UnitType.Archer)
                {
                    if (unit.state != Unit.UnitState.Guarding && unit.agent.remainingDistance <= 120f)
                        return;
                }
                else
                {
                    if (unit.state != Unit.UnitState.Guarding && unit.agent.remainingDistance <= 30f)
                        return;
                }
                    

                if (!ini2)
                    unit.ExecuteCommand(new AICommand(AICommand.CommandType.AttackTarget, UnitManager.instance.door));


                if (UnitManager.instance.door.state != Unit.UnitState.Dead)
                    return;
                stage = Objective.s4;
                ini = false;
                break;
            case Objective.s4:
                if (ini)
                {
                    unit.ExecuteCommand(new AICommand(AICommand.CommandType.AttackTarget, waypoint4.transform.position));
                    ini = true;
                }


                if (unit.stats.unitType == UnitStats.UnitType.Archer)
                {
                    if (unit.state != Unit.UnitState.Guarding && unit.agent.remainingDistance <= 60f)
                        return;
                }
                else
                {
                    if (unit.state != Unit.UnitState.Guarding && unit.agent.remainingDistance <= 30f)
                        return;
                }



                unit.ExecuteCommand(new AICommand(AICommand.CommandType.AttackTarget, UnitManager.instance.king));
                break;
        }
    }

        #region oldcode
        //private void oldMission()
        //{
        //    if (!isReady)
        //    {
        //        isReady = true;
        //        return;
        //    }

        //    if (!ini1)
        //    {
        //        foreach (Unit unit in D_Units)
        //        {
        //            unit.Guard();
        //        }
        //        ini1 = true;
        //    }

        //    switch (stage)
        //    {
        //        case Objective.s1:
        //            if (Count <= A_Units.Length)
        //            {
        //                ini = true;
        //                foreach (Unit unit in A_Units)
        //                {
        //                    if (!Unit.IsDeadOrNull(unit))
        //                    {
        //                        unit.ExecuteCommand(new AICommand(AICommand.CommandType.GoToAndGuard, waypoint1.transform.position));
        //                    }
        //                }

        //                Count++;
        //            }
        //            bool allSet = true;
        //            foreach (Unit unit in A_Units)
        //            {
        //                if (unit.state != Unit.UnitState.Guarding && !Unit.IsDeadOrNull(unit))
        //                    allSet = false;
        //            }
        //            if (!allSet) return;
        //            stage = Objective.s2;
        //            Count = 0;
        //            break;
        //        case Objective.s2:
        //            if (Count <= A_Units.Length)
        //            {
        //                foreach (Unit unit in A_Units)
        //                {
        //                    if (!Unit.IsDeadOrNull(unit))
        //                    {
        //                        if (!unit.ifPathEexists(waypoint2.transform.position))
        //                        {
        //                            Count++;
        //                            return;
        //                        }
        //                    }
        //                    unit.ExecuteCommand(new AICommand(AICommand.CommandType.GoToAndGuard, waypoint2.transform.position));
        //                }
        //                Count++;
        //            }

        //            allSet = true;
        //            foreach (Unit unit in A_Units)
        //            {
        //                if (unit.state != Unit.UnitState.Dead)
        //                    if (unit.agent.remainingDistance >= 60f)
        //                        unit.ExecuteCommand(new AICommand(AICommand.CommandType.GoToAndGuard, waypoint2.transform.position));
        //            }


        //            foreach (Unit unit in D_Units)
        //            {
        //                if (unit.state != Unit.UnitState.Dead)
        //                    return;
        //            }
        //            stage = Objective.s3;
        //            Count = 0;


        //            break;
        //        case Objective.s3:
        //            if (Count <= A_Units.Length)
        //            {
        //                foreach (Unit unit in A_Units)
        //                {
        //                    if (unit.state != Unit.UnitState.Dead)
        //                        unit.ExecuteCommand(new AICommand(AICommand.CommandType.GoToAndGuard, waypoint3.transform.position));
        //                }
        //                Count++;
        //                return;
        //            }

        //            foreach (Unit unit in A_Units)
        //            {
        //                if (unit.state != Unit.UnitState.Dead)
        //                    if (unit.state != Unit.UnitState.Guarding && unit.agent.remainingDistance <= 60f)
        //                        return;

        //            }

        //            foreach (Unit unit in A_Units)
        //            {
        //                if (unit.state != Unit.UnitState.Dead)
        //                    unit.ExecuteCommand(new AICommand(AICommand.CommandType.AttackTarget, Door));
        //            }

        //            if (Door.state != Unit.UnitState.Dead)
        //                return;
        //            stage = Objective.s4;
        //            Count = 0;
        //            break;
        //        case Objective.s4:
        //            if (Count <= A_Units.Length)
        //            {
        //                foreach (Unit unit in A_Units)
        //                {
        //                    unit.ExecuteCommand(new AICommand(AICommand.CommandType.AttackTarget, waypoint4.transform.position));
        //                }
        //                Count++;
        //                return;
        //            }

        //            foreach (Unit unit in A_Units)
        //            {
        //                if (unit.state != Unit.UnitState.Dead)
        //                    if (unit.state != Unit.UnitState.Guarding)
        //                        return;

        //            }

        //            foreach (Unit unit in A_Units)
        //            {
        //                if (unit.state != Unit.UnitState.Dead)
        //                    unit.ExecuteCommand(new AICommand(AICommand.CommandType.AttackTarget, King));
        //            }
        //            break;
        //    }
        //}

        #endregion
}
