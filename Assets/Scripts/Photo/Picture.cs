using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Picture : MonoBehaviour, IPointerClickHandler
{
    private PhotoGallery photoGallery;
    private Image image;
    [SerializeField]
    private int myIndex;

    private void Awake()
    {
        image = GetComponent<Image>();
        //photoGallery = GetComponentInParent<PhotoGallery>();
        photoGallery = GameObject.Find("UIManager").GetComponent<PhotoGallery>();
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            photoGallery.photoObj.SetActive(true);
            SizeUpImg();
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            DeletePicture();
        }
    }

    private void SizeUpImg()
    {
        Image image = GameObject.Find("MyPhotoImg").GetComponent<Image>();
        image.sprite = this.image.sprite;
    }

    private void DeletePicture()
    {
        photoGallery.RemovePhoto(myIndex);
    }



}
