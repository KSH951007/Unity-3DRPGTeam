using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quest : MonoBehaviour
{
    public int playProgress; // �÷��� ��ô���� ���� // ����Ʈ ����
    int playerLevel;         // �÷��̾� ���� or �÷��̾� ��ô�� ������ �������Կ�
    private bool needHelp;   // ����Ʈ�� ������ NPC��
    public bool getHelped;  // ����Ʈ �Ϸ�(���̻� ����Ʈ�� ���� ����)

    public string questString; // �� ���Ǿ����� ����Ʈ�� �ؽ�Ʈ
    public string questCompleteString; // ����Ʈ �Ϸ� �ؽ�Ʈ
    public string questTitle; // ����Ʈ Ÿ��Ʋ
    public string questDetail; // ����Ʈ ���� /ex) ���� 10���� ���
    public string playerName; // �÷��̾� �̸�


    private void FixedUpdate()
    {
        if(playProgress <= playerLevel )
        {
            if (getHelped)
            {
                needHelp = false;
            }
            else
            {
                needHelp = true;
            }
        }
    }
    // ����Ʈ ����
    public void GetQuest(int progress, string name) // ��ô ��Ȳ�� �÷��̾� �г���
    {
        if (needHelp)
        {
            if (GameManager.Instance.plin.curAmount < GameManager.Instance.plin.requiredAmount)
            {
                // ����Ʈ Ȯ�ΰ���
            }
            else
            {
                // ����Ʈ�� �ʹ� ���� Ȯ���Ҽ� �����ϴ�. UI ���
            }
        }
        else
        {
            // ����Ʈ Ȯ�� �Ұ���.UI ���
        }

    }

    public void questAccept()
    {
        needHelp = false;
    }
    public void questReject()
    {
        needHelp = true;
    }

    public void questComplete()
    {
        getHelped = true;
    }
}
