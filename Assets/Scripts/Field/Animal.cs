using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    [SerializeField]
    private float nowGage = 0.0f; // 현재 게이지
    private float maxGage = 1.0f; // 최대 게이지
    [SerializeField]
    private float gageIncreaseRate = 0.1f; // 게이지 증가량 (1초에 증가하는 게이지 양)


    private void Update()
    {
        if (nowGage > maxGage)
        {
            GetHeart();
            nowGage = 0.0f;
            return;
        }


        nowGage += Time.deltaTime * gageIncreaseRate;
    }

    private void GetHeart()
    {
        GameManager.Instance.nowHeart += GameManager.Instance.getHeart;
    }

    private void OnEnable()
    {
        GameManager.Instance.getHeart += 100;
    }
}
