using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Break : MonoBehaviour {
    public GameObject BrokenObject;

    public void BreakObject()
    {
        Instantiate(BrokenObject, transform.position, transform.rotation);
        Destroy(gameObject.transform.parent.gameObject);
    }

    //private void OnMouseDown()
    //{
    //    Instantiate(BrokenObject, transform.position, transform.rotation);
    //    Destroy(gameObject);
    //}
}
