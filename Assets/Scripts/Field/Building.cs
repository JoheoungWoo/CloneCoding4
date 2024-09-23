using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Image))]
public class Building : MonoBehaviour
{
    private BoxCollider boxCollider;

    private Image gage;

    [SerializeField]
    private float nowGage = 0.0f; // 현재 게이지
    private float maxGage = 1.0f; // 최대 게이지
    [SerializeField]
    private float gageIncreaseRate = 0.1f; // 게이지 증가량 (1초에 증가하는 게이지 양)

    private TMP_Text gateText;

    private void Awake()
    {
        gage = GetComponent<Image>();
        boxCollider = GetComponent<BoxCollider>();
        gateText = GetComponentInChildren<TMP_Text>();
    }

    private void Start()
    {
        MakeBoxCollider();
    }

    private void Update()
    {
        if (nowGage > maxGage)
            return;

        gateText.text = $"{nowGage.ToString("F1")}/{maxGage}";
        nowGage += Time.deltaTime * gageIncreaseRate;
    }

    private void MakeBoxCollider()
    {
        // 하위 자식들의 전체 범위를 계산하기 위해 초기화
        Bounds combinedBounds = new Bounds(transform.position, Vector3.zero);

        // 모든 자식 오브젝트의 Renderer를 순회하며 범위를 계산
        foreach (Renderer renderer in GetComponentsInChildren<Renderer>())
        {
            combinedBounds.Encapsulate(renderer.bounds);
        }

        // BoxCollider의 크기와 중심을 설정
        boxCollider.center = combinedBounds.center - transform.position;
        boxCollider.size = combinedBounds.size;
    }

    private void OnMouseDown()
    {
        if(nowGage >= maxGage)
        {
            GetMoney();
            nowGage = 0.0f;
        } else
        {
            print($"{nowGage} / {maxGage} / {name}");
        }
    }

    private void GetMoney()
    {
        GameManager.Instance.nowGold += GameManager.Instance.getGold;
    }

    private void OnEnable()
    {
        GameManager.Instance.animalArea.Add(this);
        GameManager.Instance.getGold += 100;
    }

}
