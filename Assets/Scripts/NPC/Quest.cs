using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quest : MonoBehaviour
{
    public int playProgress; // 플레이 진척도에 따라 // 퀘스트 연계
    int playerLevel;         // 플레이어 레벨 or 플레이어 진척도 병합전 쓰기위함용
    private bool needHelp;   // 퀘스트를 보유한 NPC용
    public bool getHelped;  // 퀘스트 완료(더이상 퀘스트가 뜨지 않음)

    public string questString; // 각 엔피씨마다 퀘스트용 텍스트
    public string questCompleteString; // 퀘스트 완료 텍스트
    public string questTitle; // 퀘스트 타이틀
    public string questDetail; // 퀘스트 내용 /ex) 몬스터 10마리 잡기
    public string playerName; // 플레이어 이름


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
    // 퀘스트 보상
    public void GetQuest(int progress, string name) // 진척 상황과 플레이어 닉네임
    {
        if (needHelp)
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
