using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrapButton : MonoBehaviour {

    WallPlacer1 WP;
    CurrencyManager CM;
    public int price;
    bool isEnabled;

    // Use this for initialization
    void Start ()
    {
        isEnabled = true;
        WP = FindObjectOfType<WallPlacer1>();
        CM = FindObjectOfType<CurrencyManager>();
        price = WP.trapPrice;
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

    public void enableBuild()
    {
        if (CM.GetCurrency() >= price && isEnabled)
        {
            if (!WP.isBuildEnabled)
            {
                gameObject.GetComponent<Image>().color = Color.green;
                WP.enableTrapBuild();
            }
            else
            {
                WP.disableBuild();
                gameObject.GetComponent<Image>().color = Color.white;
            }
        }
    }

    public void EnableButton()
    {
        isEnabled = true;
    }

    public void DisableButton()
    {
        isEnabled = false;
    }
}
