using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.AssetImporters;
using Unity.VisualScripting;
using UnityEngine.PlayerLoop;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "Quest Data", menuName = "Scriptable Object/NPC Talk Data", order = int.MinValue)]

public class NPCTalkData : ScriptableObject
{
    public int questID; // 퀘스트 적정 진행도
    public string Name;
    
    public string introduce;// 첫인사
    
    [HideInInspector]
    public string des; // 출력되는 string
    
    public string[] dialogues; // 기본 인사 array
    private int rndInt;
    private bool isMeet = false;

    public string[] questDialogue;
    public Quest npcSubQuest;

    public void Run()
    {
        if (false == isMeet)
        {
            des = introduce;
            isMeet = true;
        }
        else
        {
            rndInt = Random.Range(0, dialogues.Length);
            des = dialogues[rndInt];
        }
    }
    public string runForQuest(string s,int i)
    {
         s = questDialogue[i];
        return s;
    }
    public void FirstMet() // 첫인사가능
    {
        isMeet = false;
    }
}