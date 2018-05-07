using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CurrencyManager : MonoBehaviour {

    public int startingCurrency;
    public int currencyIncreesePerTime;
    public int currencyIncreesePerUnitKill;
    public Text gold;
    int currency;
    int tick;

    // Use this for initialization
    void Start () {
        tick = 60;
        currency = startingCurrency;
	}
	
	// Update is called once per frame
	void Update () {
        if (DateTime.Now.Second != tick)
        {
            tick = DateTime.Now.Second;
            currency += currencyIncreesePerTime;
            gold.text = currency.ToString();
        }
	}

    public int GetCurrency()
    {
        return currency;
    }

    public bool BuyFor(int price)
    {
        if(price <= currency)
        {
            currency -= price;
            return true;
        }
        return false;
    }
}
