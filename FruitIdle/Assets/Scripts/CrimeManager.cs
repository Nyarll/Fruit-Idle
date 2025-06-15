using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    public TextMeshProUGUI crimeText;
    public Transform dimensionPanelParent;
    public GameObject dimensionUIPrefab;

    private double crime = 0;

    private List<Dimension> dimensions = new();
    private List<TextMeshProUGUI> dimensionTexts = new();
    private List<Button> dimensionButtons = new();

    void Start()
    {
        var firstDimension = new Dimension("1st Dimension", 1, 10, 1);
        dimensions.Add(firstDimension);
        dimensions.Add(new Dimension("2nd Dimension", 2, 100, 1));
        dimensions.Add(new Dimension("3rd Dimension", 3, 1000, 1));

        foreach (Dimension dim in dimensions)
        {
            GameObject ui = Instantiate(dimensionUIPrefab, dimensionPanelParent);
            var text = ui.transform.Find("Text").GetComponent<TextMeshProUGUI>();
            var button = ui.transform.Find("Button").GetComponent<Button>();
            dimensionTexts.Add(text);
            dimensionButtons.Add(button);
            dim.UpdateCost();

            button.onClick.AddListener(() => BuyDimension(dim));
        }
        firstDimension.amount += 1;
    }

    void Update()
    {
        double[] produced = new double[dimensions.Count];

        // 生産
        for (int i = dimensions.Count - 1; i >= 0; i--)
        {
            double prod = dimensions[i].Produce();
            produced[i] = prod;

            if (i == 0)
                crime += prod * Time.deltaTime;
            else
                dimensions[i - 1].storedProduction += prod * Time.deltaTime;
        }

        // 反映
        for (int i = 0; i < dimensions.Count; i++)
        {
            int gain = (int)dimensions[i].storedProduction;
            dimensions[i].amount += gain;
            dimensions[i].storedProduction -= gain;
        }

        // 表示更新
        crimeText.text = $"{crime:F2}";

        for (int i = 0; i < dimensions.Count; i++)
        {
            dimensionTexts[i].text = $"{dimensions[i].name}: {dimensions[i].amount}\nCost: {dimensions[i].cost:F2}";
        }
    }

    void BuyDimension(Dimension dim)
    {
        if (crime >= dim.cost)
        {
            crime -= dim.cost;
            dim.amount += 1;
            dim.UpdateCost();
        }
    }
}
