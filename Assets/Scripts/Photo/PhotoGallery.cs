using DG.Tweening.Plugins.Core.PathCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PhotoGallery : MonoBehaviour
{
    private Slider slider;
    private Picture[] pictureAreas = new Picture[9];
    private Dictionary<int, Photo[]> photosData;

    [SerializeField]
    private Sprite[] tempSprite = new Sprite[5];

    [SerializeField]
    public GameObject photoObj;

    private int nowIndex = 0;
    private readonly int maxIndex = 5;

    [SerializeField]
    private Transform photoUITransform;


    private void Awake()
    {
        slider = photoUITransform.GetComponentInChildren<Slider>();
        slider.onValueChanged.AddListener(OnSliderValueChanged);
        pictureAreas = photoUITransform.GetComponentsInChildren<Picture>();
        //photoObj = GameObject.Find("PhotoArea").gameObject;
    }


    private void InitDictionry()
    {
        photosData = null;
        photosData = new Dictionary<int, Photo[]>();
        for (int i = 0; i < maxIndex; i++)
        {
            photosData.Add(i, new Photo[] { 
                new Photo(),
                new Photo(),
                new Photo(),
                new Photo(),
                new Photo(),
                new Photo(),
                new Photo(),
                new Photo(),
                new Photo(),
            }
            );
        }

        string path = Application.persistentDataPath;
        DirectoryInfo directory = new DirectoryInfo(path);

        FileInfo[] files = directory.GetFiles();

        int maxCount = files.Length;
        int nowCount = 0;

        if (maxCount <= 0)
            return;

        for(int i = 0; i < 5; i++)
        {
            for(int j = 0; j < 9; j++)
            {
                if (nowCount >= maxCount)
                    return;

                byte[] bytes = File.ReadAllBytes($"{path}/{files[nowCount].Name}");

                // Create Texture
                Texture2D texture = new Texture2D(100, 100);
                texture.LoadImage(bytes);
                texture.name = files[nowCount].Name;

                // Create Sprite
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                sprite.name = texture.name;
                photosData[i][j].SetSprite(sprite);
                nowCount++;
            }
        }

    }

    private void OnSliderValueChanged(float value)
    {
        Debug.Log(value);
        nowIndex = (int)value;
        LoadPhoto(nowIndex);
    }


    /*
    public void AddPhoto()
    {
        int photoIndex = 0;
        foreach (var key in photosData.Keys)
        {
            Photo[] photos = photosData[key];
            foreach (Photo photo in photos)
            {
                photoIndex++;
                if (photo.GetSprite() == null)
                {
                    Debug.Log("실행됬나");
                    photo.SetSprite(null);
                    return;
                }
            }
        }
    }
    */

    public void LoadPhoto(int nowIndex)
    {
        Photo[] photoDatas = photosData[nowIndex];
        for(int i = 0; i < pictureAreas.Length; i++)
        {
            pictureAreas[i].GetComponent<Image>().sprite = photoDatas[i].GetSprite();
        }
    }

    public void PhotoDelete(string photoName)
    {
        string path = Application.persistentDataPath;
        DirectoryInfo directory = new DirectoryInfo(path);
        FileInfo[] files = directory.GetFiles();

        foreach (FileInfo fileInfo in files)
        {
            if (fileInfo.Name == photoName)
            {
                fileInfo.Delete(); // 파일 삭제
                Debug.Log($"{fileInfo.Name} has been deleted.");
            }
        }
    }

    public void RemovePhoto(int index)
    {
        PhotoDelete(photosData[nowIndex][index].GetSprite().name);
        photosData[nowIndex][index].SetSprite(null);
        ArrayDictionary();
        LoadPhoto(nowIndex);
    }

    private void ArrayDictionary()
    {
        int beforeKey = 0;
        foreach (var key in photosData.Keys.ToList())
        {
            Photo[] photosArray = photosData[key];
            int length = photosArray.Length;
            beforeKey = key;

            for (int i = 0; i < length - 1; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    if(j == 8)
                    {
                        if(key < 4)
                        {
                            if (photosArray[j].GetSprite() == null)
                            {
                                var nextDatas = photosData[key + 1];
                                Sprite temp = photosArray[8].GetSprite();
                                photosArray[8].SetSprite(nextDatas[0].GetSprite());
                                nextDatas[0].SetSprite(temp);
                            }
                        }
                        
                        continue;
                    }

                    if (photosArray[j].GetSprite() == null)
                    {
                        Sprite temp = photosArray[j].GetSprite();
                        photosArray[j].SetSprite(photosArray[j + 1].GetSprite());
                        photosArray[j + 1].SetSprite(temp);
                    }
                }//for(j)
            }//for(i)     
        }
    }

    private void OnEnable()
    {
        ReLoad();
    }

    public void ReLoad()
    {
        Debug.Log("다시 온!");
        InitDictionry();
        ArrayDictionary();
        LoadPhoto(nowIndex);
    }
}
