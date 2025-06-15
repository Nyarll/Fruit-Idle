using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dimension
{
    public string name;
    public int level;
    public int amount = 0;
    public double baseCost;
    public double cost;
    public double productionRate;
    public double storedProduction = 0;

    public Dimension(string name, int level, double baseCost, double productionRate)
    {
        this.name = name;
        this.level = level;
        this.baseCost = baseCost;
        this.productionRate = productionRate;
    }

    public void UpdateCost()
    {
        cost = baseCost * System.Math.Pow(1.15, amount);
    }

    public double Produce()
    {
        return amount * productionRate;
    }
}
