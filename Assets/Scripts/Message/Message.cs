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

        // ��� ��Ҹ� �ڷ� �б�: ������ �����ؼ� 0����
        for (int i = maxCount - 1; i > 0; i--)
        {
            messagesTMP[i].text = messagesTMP[i - 1].text;
        }

        // ����Ʈ�� ù ��° ��ġ�� ���ο� �޽��� �߰�
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

        // ���� ������ŭ �޽��� �߰�
        for (int i = 0; i < ran; i++)
        {
            messageList.Add($"{messageList.Count + 1} ������");
        }

        // ȭ�鿡 ǥ���� �޽����� ���� �ε��� ���
        int minCount = Mathf.Max(0, messageList.Count - 5);

        // ���� �ֱ� 5���� �޽����� ȭ�鿡 �߰�
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
