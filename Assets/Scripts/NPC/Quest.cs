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
    public bool isComplete;  // 퀘스트 완료(더이상 퀘스트가 뜨지 않음)
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
    // 퀘스트 보상
    public void GetQuest(int progress, string name) // 진척 상황과 플레이어 닉네임
    {
        if (qData.needHelp)
        {
            if (GameManager.Instance.plin.curAmount < GameManager.Instance.plin.requiredAmount)
            {
                // 퀘스트 확인가능
            }
            else
            {
                // 퀘스트가 너무 많아 확인할수 없습니다. UI 출력
            }
        }
        else
        {
            // 퀘스트 확인 불가능.UI 출력
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
        // 경험치나 골드 보상 획득
    }

    public void updateProgress()
    {

    }
}
