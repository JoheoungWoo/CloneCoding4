using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CinemachineCamera : MonoBehaviour
{
    private List<CinemachineVirtualCamera> visualCamearas;
    private Transform defaultTransform;

    private float speed = 100f;


    private void Awake()
    {
        defaultTransform = GameObject.Find("DefaultView").transform;
        visualCamearas = new List<CinemachineVirtualCamera>();
        int cameraCount = transform.childCount;
        for(int i = 0; i < cameraCount; i++)
        {
            visualCamearas.Add(transform.GetChild(i).GetComponent<CinemachineVirtualCamera>());
        }
    }

    private void Update()
    {
        // 이유 모를 현상으로 GetAxisRaw에서 GetKey로 변경
        // 방향키 입력 확인
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            MoveCamera(-1); // 왼쪽 방향키 눌림
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            MoveCamera(1); // 오른쪽 방향키 눌림
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            ZoomCamera(1); // 위쪽 방향키 눌림
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            ZoomCamera(-1); // 아래쪽 방향키 눌림
        }

    }

    private void MoveCamera(float x)
    {
        Vector3 vec = defaultTransform.position;
        vec.x += x * Time.deltaTime * speed;

        vec.x = Mathf.Clamp(vec.x, -60, 60);
        defaultTransform.position = vec;
    }

    private void ZoomCamera(float x)
    {
        float nowValue = visualCamearas[0].m_Lens.FieldOfView;
        float value = Mathf.Clamp(x + nowValue, 30, 60);

        foreach (CinemachineVirtualCamera cam in visualCamearas)
        {
            cam.m_Lens.FieldOfView = value;
        }
    }

    public IEnumerator SwapCamera(string objectName)
    {
        if (objectName != null)
        {
            Transform targetTransform = GameObject.Find(objectName)?.transform;

            if (targetTransform != null)
            {
                visualCamearas[1].Follow = targetTransform;
                visualCamearas[1].LookAt = targetTransform;
                OnCamera(visualCamearas[1]);
                Invoke(nameof(OFFCamera), 5f);

            }
            else
            {
                OnCamera(visualCamearas[0]);
            }
        }
        else
        {
            OnCamera(visualCamearas[0]);
        }
        yield return null;
    }

    private void OFFCamera()
    {
        visualCamearas[1].Follow = null;
        visualCamearas[1].LookAt = null;
        OnCamera(visualCamearas[0]);
    }

    private void OnCamera(CinemachineVirtualCamera cinemachineVirtualCamera)
    {
        foreach(CinemachineVirtualCamera cam in visualCamearas)
        {
            if (cam == cinemachineVirtualCamera)
            {
                cam.enabled = true;
                continue;
            }
            cam.enabled = false;
             
        }
    }
}

