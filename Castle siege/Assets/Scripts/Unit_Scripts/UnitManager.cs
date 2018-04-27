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

    public GameObject Unit;
}
