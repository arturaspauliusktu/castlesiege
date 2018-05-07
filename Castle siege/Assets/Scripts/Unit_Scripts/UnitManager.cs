using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour {

    #region Singleton

    public static UnitManager instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    public List<Unit> units;
    public List<Unit> archers;
    public List<Unit> fighters;
    public List<Unit> knights;
    public List<Unit> catapults;
    public List<Unit> rams;
    public List<Unit> towers;
    public List<Unit> DefenderUnits;
    public Unit king;
    public Unit door;
}
