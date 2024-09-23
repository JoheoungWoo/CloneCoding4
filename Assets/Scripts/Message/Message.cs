using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEditor.VersionControl;

public class Message : MonoBehaviour
{
    [SerializeField]
    private List<TMP_Text> messagesTMP;

    private List<string> messageList;


    private void Awake()
    {
        messageList = new List<string>();
        int childCount = transform.childCount;

        for(int i = 0; i < childCount; i++)
        {
            messagesTMP.Add(transform.GetChild(i).GetComponentInChildren<TMP_Text>());
        }
    }

    public void SendTMPMsg(string msg)
    {
        int maxCount = messagesTMP.Count;

        // 모든 요소를 뒤로 밀기: 끝에서 시작해서 0까지
        for (int i = maxCount - 1; i > 0; i--)
        {
            messagesTMP[i].text = messagesTMP[i - 1].text;
        }

        // 리스트의 첫 번째 위치에 새로운 메시지 추가
        messagesTMP[0].text = msg;
    }

    public void ClearAllTMPMsg()
    {
        for(int i = 0; i < messagesTMP.Count; i++)
        {
            ClearTMPMsg(i);
        }
    }


    public void ClearTMPMsg(int tmpIndex)
    {
        Debug.Assert(tmpIndex < messagesTMP.Count, "tmpIndex < messagesTMP.Count");
        if (tmpIndex >= messagesTMP.Count)
            return;

        messagesTMP[tmpIndex].text = "X";
    }


    private void LoadMsg()
    {
        int ran = Random.Range(0, 5);

        // 랜덤 개수만큼 메시지 추가
        for (int i = 0; i < ran; i++)
        {
            messageList.Add($"{messageList.Count + 1} 데이터");
        }

        // 화면에 표시할 메시지의 시작 인덱스 계산
        int minCount = Mathf.Max(0, messageList.Count - 5);

        // 가장 최근 5개의 메시지를 화면에 추가
        for (int i = minCount; i < messageList.Count; i++)
        {
            SendTMPMsg(messageList[i]);
        }
    }

    private void OnEnable()
    {
        ClearAllTMPMsg();
        LoadMsg();
    }

}
