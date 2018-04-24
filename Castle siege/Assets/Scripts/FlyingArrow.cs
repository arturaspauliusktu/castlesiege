using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingArrow : MonoBehaviour {

    public int damage;
    private Rigidbody rb;
    private bool isHit = false;
    private Transform anchor;

	// Use this for initialization
	void Start () {
        rb = GetComponent <Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if(!this.isHit)
            transform.rotation = Quaternion.LookRotation(rb.velocity);
        else if (this.anchor != null)
        {
            this.transform.position = anchor.transform.position;
            this.transform.rotation = anchor.transform.rotation;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(!isHit)
        {
            ArrowStick(collision);
            this.isHit = true;
            Unit unit = collision.gameObject.GetComponent<Unit>();
            if(!Unit.IsDeadOrNull(unit))
                unit.SufferAttack(damage);
        }
    }

    private void ArrowStick(Collision other)
    {
        this.transform.position = other.contacts[0].point;
        this.GetComponent<Collider>().isTrigger = true;

        GameObject anchor = new GameObject("ARROW_ANCHOR");
        anchor.transform.position = this.transform.position;
        anchor.transform.rotation = this.transform.rotation;
        anchor.transform.parent = other.transform;
        this.anchor = anchor.transform;

        Destroy(rb);
    }
}
