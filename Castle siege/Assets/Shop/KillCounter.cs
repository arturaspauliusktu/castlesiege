using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillCounter : MonoBehaviour {

    public Text attackersText;
    public Text defendersText;

    int attackers;
    int defenders;
    int aliveAtk;
    int aliveDef;

	// Use this for initialization
	void Start () {
        attackers = GameObject.FindGameObjectsWithTag("Attacker").Length;
        defenders = GameObject.FindGameObjectsWithTag("Defender").Length;
        aliveAtk = attackers;
        aliveDef = defenders;
        attackersText.text = aliveAtk.ToString() + " / " + attackers.ToString();
        defendersText.text = aliveDef.ToString() + " / " + defenders.ToString();
    }

    public void AddDef()
    {
        defenders++;
        aliveDef++;
        print();
    }
    public void AddAtk()
    {
        attackers++;
        aliveAtk++;
        print();
    }
    public void RemDef()
    {
        Debug.Log("DIEEEEE");
        aliveDef--;
        print();
    }
    public void RemAtk()
    {
        Debug.Log("DIEEEEE");
        aliveAtk--;
        print();
    }

    void print()
    {
        defendersText.text = aliveDef.ToString() + " / " + defenders.ToString();
        attackersText.text = aliveAtk.ToString() + " / " + attackers.ToString();
    }

    public int getAttackers()
    {
        return attackers;
    }

    // Update is called once per frame
    void Update ()
    {

    }
}
