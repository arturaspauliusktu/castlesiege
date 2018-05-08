using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Knight : Unit {

    protected override void Start()
    {
        base.Start();
        if (this.stats.side == UnitStats.Sides.Attacker)
            UnitManager.instance.knights.Add(this);

    }

}
