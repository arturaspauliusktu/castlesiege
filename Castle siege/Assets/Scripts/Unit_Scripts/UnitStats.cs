using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpecificUnit", menuName = "Unit - Stats", order = 1)]
public class UnitStats : ScriptableObject {

    public Sides side = Sides.Defender;

    public enum Sides {
            Defender,
            Attacker
    }

    [Tooltip("Kiek damage gali atsaugoti")]
    public int health = 100;
    [Tooltip("Kiek duoda damage")]
    public int attackPower = 12;
    [Tooltip("Koks kautyniu greitis")]
    public float attackSpeed = 1f;
    [Tooltip("Atstumas kai inicijuojamos kautynes")]
    public float engageDistance = 1f;
    [Tooltip("Did. Atstumas kai pradedama pulti priesa")]
    public float guardDistance = 5f;

    public Sides GetOtherSide()
    {
        if (side == Sides.Defender)
        {
            return Sides.Attacker;
        }
        else
        {
            return Sides.Defender;
        }
    }
}
