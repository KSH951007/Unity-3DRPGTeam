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
    public int questID; // ����Ʈ ���� ���൵
    public string Name;
    
    public string introduce;// ù�λ�
    
    [HideInInspector]
    public string des; // ��µǴ� string
    
    public string[] dialogues; // �⺻ �λ� array
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
    public void FirstMet() // ù�λ簡��
    {
        isMeet = false;
    }
}