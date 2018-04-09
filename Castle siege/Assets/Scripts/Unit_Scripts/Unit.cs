using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using System.Linq;

public class Unit : MonoBehaviour {

    public UnitStats stats;
    public UnitState state = UnitState.Idle;

    public enum UnitState
    {
        Idle,
        Guarding,
        Attacking,
        MovingToTarget,
        MovingToSpotIdle,
        MovingToSpotGuard,
        Dead
    }

    //Priesai
    protected Unit target;
    protected Unit[] enemies;

    public NavMeshAgent agent;
    protected Unit_Health health;
    protected Break br;
    protected bool isReady = false;
    protected float lastGuardCheckTime, guardCheckInterval = 1f;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        
    }

    // Use this for initialization
    void Start () {
        stats = Instantiate<UnitStats>(stats);
    }

    //Gauna damage jei uzkrenta ant unito kazkas.
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (state != UnitState.Dead)
    //        return;

    //    if (collision.relativeVelocity.magnitude > 50.0f)
    //    {

    //        health.giveDamage((int)(collision.relativeVelocity.magnitude / 10));
    //        Debug.Log(health.getHealth());
    //    }

    //}

    // Update is called once per frame
    void Update() {

        //Cia toks hackas kad agentas gautu laiko nustatyti kito kelio taska.
        if (!isReady)
        {
            isReady = true;
            return;
        }

        switch (state)
        {
            case UnitState.MovingToSpotIdle:
                if (agent.remainingDistance < agent.stoppingDistance + 20f)
                {
                    Stop();
                }
                break;

            case UnitState.MovingToSpotGuard:
                if (agent.remainingDistance < agent.stoppingDistance + 20f)
                {
                    Guard();
                }
                break;

            case UnitState.MovingToTarget:
                //check if target has been killed by somebody else
                if (IsDeadOrNull(target))
                {
                    Guard();
                }
                else
                {
                    //Check for distance from target
                    if (agent.remainingDistance < stats.engageDistance)
                    {
                        agent.velocity = Vector3.zero;
                        StartAttacking();
                    }
                    else
                    {
                        agent.SetDestination(target.transform.position); //update target position in case it's moving
                    }
                }

                break;

            case UnitState.Guarding:
                if (Time.time > lastGuardCheckTime + guardCheckInterval)
                {
                    
                    lastGuardCheckTime = Time.time;
                    Unit t = GetNearestHostileUnit();
                    if (t != null)
                    {
                        Debug.Log(gameObject + " Moves to attack");
                        MoveToAttack(t);
                    }
                }
                break;
            case UnitState.Attacking:
                //check if target has been killed by somebody else
                if (IsDeadOrNull(target))
                {
                    Guard();
                }
                else
                {
                    //look towards the target
                    Vector3 desiredForward = (target.transform.position - transform.position).normalized;
                    transform.forward = Vector3.Lerp(transform.forward, desiredForward, Time.deltaTime * 10f);
                }
                break;
        }

    }

    public bool ifPathEexists(Vector3 location)
    {
        NavMeshPath path = new NavMeshPath();
        agent.CalculatePath(location, path);
        if (path.status == NavMeshPathStatus.PathComplete)
            return true;
        return false;
    }

    public void ExecuteCommand(AICommand c)
    {
        if (state == UnitState.Dead)
        {
            //already dead
            return;
        }

        switch (c.commandType)
        {
            case AICommand.CommandType.GoToAndIdle:
                GoToAndIdle(c.destination);
                break;

            case AICommand.CommandType.GoToAndGuard:
                GoToAndGuard(c.destination);
                break;

            case AICommand.CommandType.Stop:
                Stop();
                break;

            case AICommand.CommandType.AttackTarget:
                MoveToAttack(c.target);
                break;

            case AICommand.CommandType.Die:
                UnitDie();
                break;
        }
    }

    //move to a position and be idle
    private void GoToAndIdle(Vector3 location)
    {
        state = UnitState.MovingToSpotIdle;
        target = null;
        isReady = false;

        agent.isStopped = false;
        agent.SetDestination(location);
    }

    //move to a position and be guarding
    private void GoToAndGuard(Vector3 location)
    {
        state = UnitState.MovingToSpotGuard;
        target = null;
        isReady = false;

        agent.isStopped = false;
        agent.SetDestination(location);
    }

    //stop and stay Idle
    private void Stop()
    {
        state = UnitState.Idle;
        target = null;
        isReady = false;

        agent.isStopped = true;
        agent.velocity = Vector3.zero;
    }

    //stop but watch for enemies nearby
    public void Guard()
    {
        state = UnitState.Guarding;
        target = null;
        isReady = false;

        agent.isStopped = true;
        agent.velocity = Vector3.zero;
    }

    //move towards a target to attack it
    private void MoveToAttack(Unit target)
    {
        if (!IsDeadOrNull(target))  
        {
            state = UnitState.MovingToTarget;
            this.target = target;
            isReady = false;

            agent.isStopped = false;
            agent.SetDestination(target.transform.position);
        }
        else
        {
            //if the command is dealt by a Timeline, the target might be already dead
            Guard();
        }
    }

    //reached the target (within engageDistance), time to attack
    private void StartAttacking()
    {
        //somebody might have killed the target while this Unit was approaching it
        if (!IsDeadOrNull(target))
        {
            state = UnitState.Attacking;
            isReady = false;
            agent.isStopped = true;
            StartCoroutine(DealAttack());
        }
        else
        {
            Guard();
        }
    }

    //the single blows
    private IEnumerator DealAttack()
    {
        while (target != null)
        {
            //Kautyniu animacija
            target.SufferAttack(stats.attackPower);

            yield return new WaitForSeconds(2f / stats.attackSpeed);

            //check is performed after the wait, because somebody might have killed the target in the meantime
            if (IsDeadOrNull(target))
            {
                //Mirimo animacija
                break;

            }

            if (state == UnitState.Dead)
            {
                yield break;
            }

            //Check if the target moved away for some reason
            if (Vector3.Distance(target.transform.position, transform.position) > stats.engageDistance)
            {
                MoveToAttack(target);
            }
        }


        //only move into Guard if the attack was interrupted (dead target, etc.)
        if (state == UnitState.Attacking)
        {
            Guard();
        }
    }

    //called by an attacker
    private void SufferAttack(int damage)
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
        }
    }

    private Unit GetNearestHostileUnit()
    {
        enemies = GameObject.FindGameObjectsWithTag(stats.GetOtherSide().ToString()).Select(x => x.GetComponent<Unit>()).ToArray();

        Unit nearestEnemy = null;
        float nearestEnemyDistance = 1000f;
        for (int i = 0; i < enemies.Count(); i++)
        {
            if (IsDeadOrNull(enemies[i]))
            {
                continue;
            }

            float distanceFromHostile = Vector3.Distance(enemies[i].transform.position, transform.position);
            if (distanceFromHostile <= stats.guardDistance)
            {
                if (distanceFromHostile < nearestEnemyDistance)
                {
                    nearestEnemy = enemies[i];
                    nearestEnemyDistance = distanceFromHostile;
                }
            }
        }

        return nearestEnemy;
    }

    /// <summary>
    /// TODO: Unitas pavirsta i ragdoll ir po kazkiek laiko ragdoll turi isnykti.
    /// </summary>
    protected virtual void UnitDie()
    {
        Debug.Log(gameObject.ToString() + " Just died");
        state = UnitState.Dead;

        gameObject.tag = "Untagged"; //Kad kiti agentai nepultu lavono.
        gameObject.layer = 0;

        Destroy(agent);
        Destroy(gameObject.GetComponent<MeshFilter>());
        Destroy(gameObject.GetComponent<BoxCollider>());
        Destroy(gameObject.GetComponent<Rigidbody>());
    }

    /// <summary>
    /// Patikrina ar unitas miras arb isviso neegzistuoja.
    /// Atsarga gedos nedaro
    /// </summary>
    /// <param name="u">unitas</param>
    /// <returns>grazina true arba false</returns>
    private bool IsDeadOrNull(Unit u)
    {
        return (u == null || u.state == UnitState.Dead);
    }

    private void OnDrawGizmos()
    {
        if (agent != null
            && agent.isOnNavMesh
            && agent.hasPath)
        {
            Gizmos.DrawLine(transform.position, agent.destination);
        }
    }
}
