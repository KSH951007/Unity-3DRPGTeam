using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "Quest Data", menuName = "Scriptable Object/Quest Data", order = int.MinValue)]
public class QuestData// : ScriptableObject
{
    public int questID;
    private int PlayerProgress;
    public int playProgress { get { return PlayerProgress; } }  // 플레이 진척도에 따라 // 퀘스트 연계
    public int playerLevel; // 플레이어 레벨 or 플레이어 진척도 병합전 쓰기위함용

    public string rewardEXP; // 퀘스트 보상 EXP
    public string questGold; // 퀘스트 보상 골드
    public string questCompleteString; // 퀘스트 완료 텍스트
}
