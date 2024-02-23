using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum questType
{
    collect,
    introduce
}
public class Quest : MonoBehaviour
{
    public QuestData qData;

    [HideInInspector]
    public bool isComplete;  // ����Ʈ �Ϸ�(���̻� ����Ʈ�� ���� ����)
    private QuestData questData;

    public Quest(QuestData questData)
    {
        this.questData = questData;
    }

    private void FixedUpdate()
    {
        if(qData.playProgress <= qData.playerLevel )
        {
            if (isComplete)
            {
                qData.needHelp = false;
            }
            else
            {
                qData.needHelp = true;
            }
        }
    }
    // ����Ʈ ����
    public void GetQuest(int progress, string name) // ��ô ��Ȳ�� �÷��̾� �г���
    {
        if (qData.needHelp)
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
        qData.needHelp = false;
    }
    public void questReject()
    {
        qData.needHelp = true;
    }

    public void questComplete()
    {
        updateProgress();
        isComplete = true;
        // ����ġ�� ��� ���� ȹ��
    }

    public void updateProgress()
    {

    }
}
