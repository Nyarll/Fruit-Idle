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
    private Color[] dimensionColors = new Color[]
    {
        new Color(1.0f, 0f, 0f, 1.0f),
        new Color(1.0f, 0.647f, 0f, 1.0f),
        new Color(1.0f, 1.0f, 0f, 1.0f),
        new Color(0.486f, 0.988f, 0f, 1.0f),
        new Color(0f, 0.98f, 0.603f, 1.0f),
        new Color(0f, 1.0f, 1.0f, 1.0f),
        new Color(0f, 0f, 0.803f, 1.0f),
        new Color(0.58f, 0f, 0.827f, 1.0f),
        new Color(1.0f, 0f, 1.0f, 1.0f),
        new Color(1.0f, 1.0f, 1.0f, 1.0f)
    };

    void init()
    {
        var firstDim = new Dimension("1st Dimension", 1, 10, 1);
        firstDim.amount = 1;
        dimensions.Add(firstDim);
        dimensions.Add(new Dimension("2nd Dimension", 2, 100, 1));
        dimensions.Add(new Dimension("3rd Dimension", 3, 1000, 1));
        dimensions.Add(new Dimension("4th Dimension", 4, 10000, 1));
        dimensions.Add(new Dimension("5th Dimension", 4, 100000, 1));
        dimensions.Add(new Dimension("6th Dimension", 4, 1000000, 1));
        dimensions.Add(new Dimension("7th Dimension", 4, 10000000, 1));
        dimensions.Add(new Dimension("8th Dimension", 4, 100000000, 1));
        dimensions.Add(new Dimension("9th Dimension", 4, 1000000000, 1));
        dimensions.Add(new Dimension("10th Dimension", 4, 10000000000, 1));
    }

    void Start()
    {
        init();
        for (int i = 0; i < dimensions.Count; i++)
        {
            GameObject ui = Instantiate(dimensionUIPrefab, dimensionPanelParent);

            Image background = ui.transform.Find("Panel").GetComponent<Image>();
            if (background != null && i < dimensionColors.Length)
            {
                background.color = dimensionColors[i];
            }

            var text = ui.transform.Find("Text").GetComponent<TextMeshProUGUI>();
            var button = ui.transform.Find("Button").GetComponent<Button>();
            dimensionTexts.Add(text);
            dimensionButtons.Add(button);

            int index = i;
            dimensions[index].UpdateCost();
            button.onClick.AddListener(() => BuyDimension(dimensions[index]));
        }
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
        crimeText.text = $"{FormatNumber(crime)}";

        for (int i = 0; i < dimensions.Count; i++)
        {
            dimensionTexts[i].text = $"{dimensions[i].name}: {FormatNumber(dimensions[i].amount, 100000)}\nCost: {FormatNumber(dimensions[i].cost)}";
        }
    }

    string FormatNumber(double value, int threshold = 100000000)
    {
        if (value < threshold)
        {
            return value.ToString("F2");
        }
        else
        {
            return value.ToString("E2");
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
