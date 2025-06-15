using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    public TextMeshProUGUI crimeText;
    public TextMeshProUGUI firstDimensionText;
    public Button buyFirstDimensionButton;

    private double crime = 0;
    public double crimePerSecond = 1;

    private Dimension firstDimension;

    void Start()
    {
        firstDimension = new Dimension("1st Dimension", 10, 1);
        firstDimension.UpdateCost();

        buyFirstDimensionButton.onClick.AddListener(BuyFirstDimension);
    }

    void Update()
    {
        crime += crimePerSecond * Time.deltaTime;
        crimeText.text = $"Crime: {crime:F2}";

        firstDimensionText.text = $"{firstDimension.name}: {firstDimension.amount}\nCost: {firstDimension.cost:F2}";
    }

    void BuyFirstDimension()
    {
        if (crime >= firstDimension.cost)
        {
            crime -= firstDimension.cost;
            firstDimension.amount += 1;
            firstDimension.UpdateCost();

            crimePerSecond += firstDimension.productionRate;
        }
    }
}
