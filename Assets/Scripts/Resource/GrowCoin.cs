using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GrowCoin : MonoBehaviour, IConsumableResource
{
    private TMP_Text growCoinTMP;

    public double NowValue => GameManager.Instance.nowGrowCoin;

    private double valuePerSecond;
    public double ValuePerSecond => valuePerSecond;

    private void Awake()
    {
        growCoinTMP = GetComponentInChildren<TMP_Text>();
    }

    private void Update()
    {
        PrintResource(NowValue);
    }

    public void PrintResource(double value)
    {
        growCoinTMP.text = value.ToCurrencyString();
    }
}
