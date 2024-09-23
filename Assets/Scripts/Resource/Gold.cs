using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Gold : MonoBehaviour, IConsumableResource
{
    private TMP_Text goldTMP;
    private TMP_Text goldPerSecondTMP;

    public double NowValue => GameManager.Instance.nowGold;

    [SerializeField]
    private double valuePerSecond = 0;
    public double ValuePerSecond => valuePerSecond;

    private void Awake()
    {
        goldTMP = transform.FindNameChild("GoldTMP").GetComponent<TMP_Text>();
        goldPerSecondTMP = transform.FindNameChild("GoldTooltipTMP").GetComponent<TMP_Text>();
    }
    private void Update()
    {
        PrintResource(NowValue);
        PrintPerSecond(ValuePerSecond);
    }

    public void PrintResource(double value)
    {
        goldTMP.text = value.ToCurrencyString();
    }
    
    public void PrintPerSecond(double value)
    {
        goldPerSecondTMP.text = $"{value.ToCurrencyString()}/��";
    }


}
