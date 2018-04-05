using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Health {

    private int maxUnitHealth;

    private int UnitHealth;

    public Unit_Health(int maxUnitHealth, int unitHealth)
    {
        if(unitHealth > maxUnitHealth)
        {
            this.maxUnitHealth = maxUnitHealth;
            this.UnitHealth = maxUnitHealth;
        }
        else
        {
            this.maxUnitHealth = maxUnitHealth;
            this.UnitHealth = unitHealth;
        }
    }

    public Unit_Health(int maxUnitHealth)
    {
        this.maxUnitHealth = maxUnitHealth;
        this.UnitHealth = maxUnitHealth;
    }

    public int getHealth() { return UnitHealth; }

    public void setHealth(int health) {
        if(health > maxUnitHealth)
        {
            UnitHealth = maxUnitHealth;
        }
        else
        {
            UnitHealth = health;
        }
    }

    /// <summary>
    /// Duoda unitams nustatyta damage.
    /// </summary>
    /// <param name="dmg">Damage dydis</param>
    /// <returns>true jei unitas mire false jei junitas gyvas</returns>
    public bool giveDamage(int dmg)
    {
        UnitHealth -= dmg;
        if (UnitHealth <= 0)
        {
            UnitHealth = 0;
            return true;
        }
        return false;
    }

    
    /// <summary>
    /// Duoda unitams givybiu.
    /// </summary>
    /// <param name="health">Givybiu dydis</param>
    public void HealUnit(int health)
    {
        UnitHealth += health;
        if (UnitHealth > maxUnitHealth)
            UnitHealth = maxUnitHealth;
    }

    /// <summary>
    /// Unitas atgauna visas givybes.
    /// </summary>
    public void HealUnit()
    {
        UnitHealth = maxUnitHealth;
    }
}
