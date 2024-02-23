using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest Data", menuName = "Scriptable Object/Quest Data", order = int.MinValue)]
public class QuestData : ScriptableObject
{
    public int questID;
    private int PlayerProgress;
    public int playProgress { get { return PlayerProgress; } }  // 플레이 진척도에 따라 // 퀘스트 연계
    public int playerLevel; // 플레이어 레벨 or 플레이어 진척도 병합전 쓰기위함용

    [HideInInspector]
    public bool needHelp;   // 퀘스트를 보유한 NPC용

    public string playerName; // 플레이어 이름
    
    public string questTitle; // 퀘스트 타이틀
    public string questString; // 각 엔피씨마다 퀘스트용 텍스트(스토리용)
    
    public string questDetail; // 퀘스트 내용 /ex) 몬스터 10마리 잡기
    public int detailInteger;

    public string rewardEXP; // 퀘스트 보상 EXP
    public string questGold; // 퀘스트 보상 골드
    public string questCompleteString; // 퀘스트 완료 텍스트
}
