using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Catapult : Unit_Archer {

    private bool isReadyToShoot;

    // Use this for initialization
    protected override void Start () {
        stats = Instantiate<UnitStats>(stats);
        h = 2;
        this.gravity = -Physics.gravity.magnitude;

        if (stats.side == UnitStats.Sides.Attacker)
        {
            UnitManager.instance.units.Add(this);
        }

        if (stats.side == UnitStats.Sides.Attacker)
        {
            UnitManager.instance.catapults.Add(this);
        }
    }

    public void setReady()
    {
        isReadyToShoot = true;
    }

    protected override void ShootTarget(Unit target)
    {
        animator.SetTrigger("Attack1Trigger");
    }

    private void Loose()
    {
        Rigidbody newball = (Rigidbody)Instantiate(projectile, spawnpoint.position, spawnpoint.rotation);
        newball.velocity = CalculateLounchVelocity(target).initialVelocity;

        newball.useGravity = true;
    }


    protected override void UnitDie()
    {
        Debug.Log(gameObject.ToString() + " Just died");
        state = UnitState.Dead;

        if (OnDie != null)
        {
            OnDie(this);
        }

        gameObject.tag = "Untagged"; //Kad kiti agentai nepultu lavono.
        gameObject.layer = 0;

        gameObject.SetActive(false);

        GetComponent<Break>().BreakObject();

        UnitManager.instance.catapults.Remove(this);
        UnitManager.instance.units.Remove(this);
    }
}
