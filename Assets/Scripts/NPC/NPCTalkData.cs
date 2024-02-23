using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Quest Data", menuName = "Scriptable Object/NPC Talk Data", order = int.MinValue + 1)]

public class NPCTalkData : ScriptableObject
{
    public int npcID;
    public string Name;
    public string introduce;// ù�λ�
    public string des; // ù�λ� ����
}
