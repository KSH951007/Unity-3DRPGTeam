using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.AssetImporters;
using Unity.VisualScripting;
using UnityEngine.PlayerLoop;

[CreateAssetMenu(fileName = "Quest Data", menuName = "Scriptable Object/NPC Talk Data", order = int.MinValue)]

public class NPCTalkData : ScriptableObject
{
    public int npcID;
    public string Name;
    public string introduce;// 첫인사
    [HideInInspector]
    public string des; // 출력되는 string
    [HideInInspector]
    public string dialogue; // 첫인사 이후
    public string[] dialogues; // 기본 인사 array
    private int rndInt;
    private bool isMeet = false;


    public void Run()
    {
        if(false == isMeet)
        {
            des = introduce;
            isMeet = true;
        }
        else
        {
            rndInt = Random.Range(0, dialogues.Length);
            dialogue = dialogues[rndInt];
            des = dialogue;
        }
    }
    public void FirstMet() // 첫인사가능
    {
        isMeet = false;
    }
}
