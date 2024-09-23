using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private Capture capture;
    private PhotoGallery photoGallery;


    private void Awake()
    {
        capture = GetComponent<Capture>();
        photoGallery = GetComponent<PhotoGallery>();
    }
    public void OpeneWindow(GameObject obj)
    {
        obj.SetActive(true);
        if (obj.name.Equals("GambleScene"))
        {
            var needTMP = GameObject.Find("NeetTMP").GetComponent<TMP_Text>();
            needTMP.text = $"Gold : {Helper.ToCurrencyString(GameManager.Instance.needGambleMoney)} / Heart : {Helper.ToCurrencyString(GameManager.Instance.needHeart)}";
        }
    }

    public void CloseWindow(GameObject obj)
    {
        obj.SetActive(false);
    }

    public IEnumerator CloseWindow(GameObject obj,float delay)
    {
        yield return new WaitForSeconds(delay);
        obj.SetActive(false);
    }


    public void StartProduction1(GameObject obj)
    {
        CloseWindow(obj);

        if (GameManager.Instance.nowGold < GameManager.Instance.needGambleMoney ||
            GameManager.Instance.nowHeart < GameManager.Instance.needHeart)
            return;

        GameManager.Instance.nowGold -= GameManager.Instance.needGambleMoney;
        GameManager.Instance.nowHeart -= GameManager.Instance.needHeart;
        GameManager.Instance.needGambleMoney *= 1.5f;
        GameManager.Instance.needHeart *= 3f;

        GameObject gambleObj = Resources.Load<GameObject>("Production/GambleEffect");
        Transform transform = GameObject.Find("CreateGambleEffectTransform").transform;
        GambleEffect effect = Instantiate(gambleObj, transform).GetComponent<GambleEffect>();
        effect.Production1();
    }

    public void CaptureProduction1(GameObject obj)
    {
        StartCoroutine(capture.Rendering((reslt) =>
        {
            OpeneWindow(obj);
            //photoGallery.AddPhoto();
            photoGallery.ReLoad();
            StartCoroutine(CloseWindow(obj,1f));
        }));
    }
}
