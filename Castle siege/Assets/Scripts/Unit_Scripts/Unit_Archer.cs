//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.AI;
//using UnityEngine.Events;
//using System.Linq;

//public class Unit_Archer : Unit {

//    public Transform spawnpoint;

//    public Rigidbody arrow;

//    public float h = 10;

//    public bool debugPath = false;

//    protected float gravity;

//    protected override void Start()
//    {
//        base.Start();
//        gravity = -Physics.gravity.magnitude;
//    }

//    protected override void Update()
//    {
//        base.Update();
//        if (debugPath)
//            DrawPath(target);
//    }

//    protected override IEnumerator DealAttack()
//    {
//        animator.SetTrigger("Attack1Trigger");
//        while (target != null)
//        {

//            //Kautyniu animacija
//            ShootTarget(target);
//            animator.SetBool("Moving", false);

//            yield return new WaitForSeconds(2f / stats.attackSpeed);

//            //check is performed after the wait, because somebody might have killed the target in the meantime
//            if (IsDeadOrNull(target))
//            {
//                //Mirimo animacija
//                break;

//            }

//            if (state == UnitState.Dead)
//            {
//                yield break;
//            }

//            //Check if the target moved away for some reason
//            if (Vector3.Distance(target.transform.position, transform.position) > stats.engageDistance)
//            {
//                MoveToAttack(target);
//            }
//        }
//    }

//    protected virtual void ShootTarget(Unit target)
//    {
//        Rigidbody newarrow = (Rigidbody)Instantiate(arrow, spawnpoint.position, spawnpoint.rotation);
//        newarrow.velocity = CalculateLounchVelocity(target).initialVelocity;
        
//    }

//    private LounchData CalculateLounchVelocity(Unit target)
//    {
//        Vector3 p = target.transform.position;      //Target position
//        Vector3 sp = spawnpoint.position; //Spawnpoint position

//        float displacementY = p.y - sp.y;
//        Vector3 displacementXZ = new Vector3(p.x - sp.x, 0, p.z - sp.z);
//        float time = Mathf.Sqrt(-2 * h / gravity) + Mathf.Sqrt(2 * (displacementY - h) / gravity);

//        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * h);
//        Vector3 velocityXZ = displacementXZ / time;
//        Vector3 velocity = velocityXZ + velocityY * -Mathf.Sign(gravity);

//        return new LounchData(velocity, time);
//    }

//    void DrawPath(Unit target)
//    {
//        LounchData lounchData = CalculateLounchVelocity(target);
//        Vector3 previousDrawPoint = spawnpoint.position;

//        int resolution = 30;
//        for (int i = 1; i <= resolution; i++)
//        {
//            float simulationTime = i / (float)resolution * lounchData.timeToTarget;
//            Vector3 displacement = lounchData.initialVelocity * simulationTime + Vector3.up * gravity * simulationTime * simulationTime / 2f;
//            Vector3 drawpoint = spawnpoint.position + displacement;
//            Debug.DrawLine(previousDrawPoint, drawpoint, Color.green);
//            previousDrawPoint = drawpoint;
//        }
//    }

//    struct LounchData
//    {
//        public readonly Vector3 initialVelocity;
//        public readonly float timeToTarget;

//        public LounchData(Vector3 initialVelocity, float timeToTarget)
//        {
//            this.timeToTarget = timeToTarget;
//            this.initialVelocity = initialVelocity;
//        }
//    }
//}
