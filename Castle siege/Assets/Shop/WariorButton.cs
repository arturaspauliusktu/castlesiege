using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WariorButton : MonoBehaviour {

    WallPlacer1 WP;
    CurrencyManager CM;
    public int price;

    // Use this for initialization
    void Start () {
        WP = FindObjectOfType<WallPlacer1>();
        CM = FindObjectOfType<CurrencyManager>();
        price = WP.wariorPrice;
    }

    private void Update()
    {
        if (CM.GetCurrency() < price)
        {
            gameObject.GetComponent<Image>().color = Color.red;
        }
        else if (!WP.isBuildEnabled)
        {
            gameObject.GetComponent<Image>().color = Color.white;
        }
    }

    public void EnableBuild () {
        if (CM.GetCurrency() >= price)
        {
            if (WP.isBuildEnabled)
            {
                WP.disableBuild();
                gameObject.GetComponent<Image>().color = Color.white;
            }
            else
            {
                WP.enableWariorBuild();
                gameObject.GetComponent<Image>().color = Color.green;
            }
        }
	}
}
