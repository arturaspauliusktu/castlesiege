using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Wall : Unit {


    protected override void Awake()
    {
    }

    protected override void UnitDie()
    {
        Debug.Log(gameObject.ToString() + " Just died");

        if (OnDie != null)
        {
            OnDie(this);
        }

        gameObject.tag = "Untagged"; //Kad kiti agentai nepultu lavono.
        gameObject.layer = 0;


        Destroy(gameObject.GetComponent<BoxCollider>());
        Destroy(this.gameObject, 30);
    }


    protected override void Start()
    {
    }

    public override void SufferAttack(int damage)
    {
        if (state == UnitState.Dead)
        {
            //already dead
            return;
        }

        stats.health -= damage;

        if (stats.health <= 0)
        {
            stats.health = 0;
            UnitDie();
            //animator.SetBool("Moving", false);
        }
    }
}
