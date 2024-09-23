using UnityEngine;

public class UpgradeContent : MonoBehaviour
{
    [SerializeField]
    private GameObject animalRowPrefab;
    [SerializeField]
    private GameObject growCoinPrefab;

    private Transform createTransform;


    private void Awake()
    {
        createTransform = transform.FindNameChild("Content");
    }

    private void CreateUpgradeRow(Enums.Upgrade choiceUpgrade)
    {
        switch(choiceUpgrade)
        {
            case Enums.Upgrade.NormalAnimal:
            case Enums.Upgrade.RareAnimal:
                {
                    Instantiate(animalRowPrefab, createTransform);
                    break;
                }
            case Enums.Upgrade.GrowCoin:
                {
                    Instantiate(growCoinPrefab, createTransform);
                    break;
                }
        }
    }

    private void ClearTransforms()
    {
        createTransform.DetachChildren();
    }

    private void ReLoadContent()
    {
        
        ClearTransforms();
        for (int i = 0; i < 10; i++)
        {
            CreateUpgradeRow(GameManager.Instance.ChoiceUpgrade);
        }
    }

    private void OnEnable()
    {
        ReLoadContent();
    }
}
