using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class GambleEffect : MonoBehaviour
{
    // Move from NowTransform to UpgradeBtn
    public void Production1()
    {
        Transform upgradeTransform = GameObject.Find("UpgradeBtn").transform;


        transform.DOMove(upgradeTransform.position, 3f, false)
            .OnComplete(() => {
                DeleteProduction1();
        });
    }

    public void DeleteProduction1()
    {
        CreateAnimal();
        Destroy(gameObject);
    }

    public void CreateAnimal()
    {
        if (GameManager.Instance.animalPrefabs.Count <= 0)
            return;

        if (GameManager.Instance.animalArea.Count <= 0)
            return;

        GameObject obj = Instantiate(GameManager.Instance.animalPrefabs[Random.Range(0, GameManager.Instance.animalPrefabs.Count)], GameManager.Instance.animalArea[Random.Range(0,GameManager.Instance.animalArea.Count)].transform);
        obj.transform.localPosition = new Vector3(0, 5, 0);
        GameManager.Instance.animalList.Add(obj.GetComponent<Animal>());

        CinemachineCamera gambleObj = GameObject.Find("CinemachineCamera").GetComponent<CinemachineCamera>();
        StartCoroutine(gambleObj.SwapCamera(obj.name));      
    }
}
