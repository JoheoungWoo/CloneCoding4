using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public float GameSpeed;

    private bool isLevelUp = false;
    private int maxLevel = 3;
    private int nowLevel = 0;

    

    public double nowGrowCoin = 0;
    public double nowHeart = 0;
    public double nowGold = 0;

    public double getGold = 0;
    public double getHeart = 0;

    public double needGambleMoney = 100;
    public double needHeart = 100;

    public List<Animal> animalList = new List<Animal>();

    public List<GameObject> animalPrefabs;

    public List<Building> animalArea;

    public Enums.Upgrade ChoiceUpgrade;

    public List<GameObject> openFieldsObject; 

    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();

                if (_instance == null)
                {
                    GameObject singleton = new GameObject(typeof(GameManager).Name);
                    _instance = singleton.AddComponent<GameManager>();
                    DontDestroyOnLoad(singleton);
                }
            }
            return _instance;
        }
    }


    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void Choice(int choiceIndex)
    {
        switch (choiceIndex)
        {
            case 1:
                ChoiceUpgrade = Enums.Upgrade.NormalAnimal;
                break;
            case 2:
                ChoiceUpgrade = Enums.Upgrade.RareAnimal;
                break;
            case 3:
                ChoiceUpgrade = Enums.Upgrade.GrowCoin;
                break;
        }
    }


    private void Update()
    {
        Time.timeScale = GameSpeed;

        // 동물의 수에 따라 현재 레벨을 조정합니다
        int targetLevel = Mathf.Min((animalList.Count / 3) + 1, maxLevel);

        // 현재 레벨이 목표 레벨과 다를 경우 레벨업을 진행합니다
        if (nowLevel < targetLevel)
        {
            UpLevel();
        }
    }

    public void UpLevel()
    {
        if (nowLevel > maxLevel)
            return;

        openFieldsObject[nowLevel].SetActive(true);
        nowLevel++;

        nowGold += needGambleMoney * 5;
        nowHeart += needHeart * 10;
    }
}
