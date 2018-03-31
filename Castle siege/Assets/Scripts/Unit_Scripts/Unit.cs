using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour {

    public int MaxUnitHealth = 100;
    public int UnitHealth = 100;
    public NavMeshAgent agent;
    public Camera cam;
    bool isAlive = true;
    Unit_Health health;
    Break br;
    GameObject King;
    

    // Use this for initialization
    void Start () {
        health = new Unit_Health(MaxUnitHealth, UnitHealth);
        br = gameObject.GetComponent<Break>();
        King = GameObject.FindWithTag("King");
        Vector3 pos = King.transform.position;
        pos.y = 0;
        agent.SetDestination(pos);
    }

    private void OnCollisionEnter(Collision collision)
    {

        if(collision.relativeVelocity.magnitude > 50.0f)
        {
            Debug.Log(health.getHealth());

            if (health.giveDamage(10) && isAlive)
            {
                isAlive = false;
                br.StartCoroutine("BreakObject");
                Debug.Log(gameObject.name + " - Dead");
            }
        }

    }

    // Update is called once per frame
    void Update() {
        UnitHealth = health.getHealth();
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
            }
        }
	}
}
