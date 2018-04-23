using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unit_shooting_test : MonoBehaviour {

    public Transform spawnpoint;

    public Rigidbody arrow;
    public Transform target;

    public float initialAngle;

    public float h = 25;
    public float gravity;

    public bool debugPath = false;

    // Update is called once per frame
    private void Start()
    {
        gravity = - Physics.gravity.magnitude;
    }

    void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
            Shoot();

        if (debugPath)
            DrawPath();
    }


    private void Shoot()
    {
        Rigidbody newarrow = (Rigidbody)Instantiate(arrow, spawnpoint.position, spawnpoint.rotation);
        newarrow.velocity = CalculateLounchVelocity().initialVelocity;
    }


    private LounchData CalculateLounchVelocity()
    {
        Vector3 p = target.position;      //Target position
        Vector3 sp = spawnpoint.position; //Spawnpoint position

        float displacementY = p.y - sp.y;
        Vector3 displacementXZ = new Vector3(p.x - sp.x, 0, p.z - sp.z);
        float time = Mathf.Sqrt(-2 * h / gravity) + Mathf.Sqrt(2 * (displacementY - h) / gravity);

        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * h);
        Vector3 velocityXZ = displacementXZ / time;

        return new LounchData(velocityXZ + velocityY * -Mathf.Sign(gravity), time);
    }

    void DrawPath()
    {
        LounchData lounchData = CalculateLounchVelocity();
        Vector3 previousDrawPoint = spawnpoint.position;

        int resolution = 30;
        for (int i = 1; i <= resolution; i++)
        {
            float simulationTime = i / (float)resolution * lounchData.timeToTarget;
            Vector3 displacement = lounchData.initialVelocity * simulationTime + Vector3.up * gravity * simulationTime * simulationTime / 2f;
            Vector3 drawpoint = spawnpoint.position + displacement;
            Debug.DrawLine(previousDrawPoint, drawpoint, Color.green);
            previousDrawPoint = drawpoint;
        }
    }

    struct LounchData
    {
        public readonly Vector3 initialVelocity;
        public readonly float timeToTarget;

        public LounchData ( Vector3 initialVelocity, float timeToTarget)
        {
            this.timeToTarget = timeToTarget;
            this.initialVelocity = initialVelocity;
        }
    }
}
