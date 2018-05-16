using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Ram : Unit {

    protected override void Start()
    {
        base.Start();
        if(stats.side == UnitStats.Sides.Attacker)
        {
            UnitManager.instance.units.Add(this);
            UnitManager.instance.rams.Add(this);
        }
    }
}
