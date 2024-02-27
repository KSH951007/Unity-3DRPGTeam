using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.AssetImporters;

[CreateAssetMenu(fileName = "Quest Data", menuName = "Scriptable Object/NPC Talk Data", order = int.MinValue)]

public class NPCTalkData : ScriptableObject
{
    public int npcID;
    public string Name;
    public string introduce;// 첫인사
    public string des; // 첫인사 이후

}
