using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapScript : MonoBehaviour {

    public int captureCount;
    LinkedList<Unit> wariorsIncide;
    bool trapSet;
    Animator anim;

    // Use this for initialization
    void Start () {
        wariorsIncide = new LinkedList<Unit>();
        trapSet = true;
        anim = gameObject.transform.GetChild(0).GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Unit>().GetType().Equals(new Unit_Knight().GetType()) ||
            other.gameObject.GetComponent<Unit>().GetType().Equals(new Unit_Fighter().GetType()) ||
            other.gameObject.GetComponent<Unit>().GetType().Equals(new Unit_Archer().GetType()) )
        {
            Debug.Log("ENTER trap");
            Unit n = other.gameObject.GetComponent<Unit>();
            wariorsIncide.AddFirst(n);
            if (wariorsIncide.Count >= captureCount && trapSet)
            {
                anim.SetTrigger("TrapsTriggered");
                gameObject.transform.position += new Vector3(0, 2, 0);
                for (int i = 0; i < wariorsIncide.Count; i++)
                {
                    Debug.Log("Killing unit");
                    AICommand s = new AICommand(AICommand.CommandType.Die);
                    wariorsIncide.First.Value.ExecuteCommand(s);
                    wariorsIncide.RemoveFirst();
                }
                trapSet = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("LEAVE Trap");
        Unit n = other.gameObject.GetComponent<Unit>();
        wariorsIncide.Remove(n);
    }
}
