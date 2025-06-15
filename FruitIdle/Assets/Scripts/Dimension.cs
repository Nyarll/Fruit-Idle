using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dimension
{
    public string name;
    public int amount = 0;
    public double baseCost;
    public double cost;
    public double productionRate;

    public Dimension(string name, double baseCost, double productionRate)
    {
        this.name = name;
        this.baseCost = baseCost;
        this.cost = baseCost;
        this.productionRate = productionRate;
    }

    public void UpdateCost()
    {
        cost = baseCost * System.Math.Pow(1.15, amount);
    }
}
