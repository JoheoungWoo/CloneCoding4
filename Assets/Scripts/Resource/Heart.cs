using UnityEngine;
using TMPro;
using System.Runtime.ConstrainedExecution;

public class Heart : MonoBehaviour, IConsumableResource
{
    private TMP_Text heartTMP;

    public double NowValue => GameManager.Instance.nowHeart;

    private double valuePerSecond;
    public double ValuePerSecond => valuePerSecond;

    private void Awake()
    {
        heartTMP = GetComponentInChildren<TMP_Text>();
    }
    private void Update()
    {
        PrintResource(NowValue);
    }

    public void PrintResource(double value)
    {
        heartTMP.text = value.ToCurrencyString();
    }


}
