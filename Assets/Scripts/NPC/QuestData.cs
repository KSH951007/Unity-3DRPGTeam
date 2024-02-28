using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "Quest Data", menuName = "Scriptable Object/Quest Data", order = int.MinValue)]
public class QuestData// : ScriptableObject
{
    public int questID;
    private int PlayerProgress;
    public int playProgress { get { return PlayerProgress; } }  // �÷��� ��ô���� ���� // ����Ʈ ����
    public int playerLevel; // �÷��̾� ���� or �÷��̾� ��ô�� ������ �������Կ�

    public string rewardEXP; // ����Ʈ ���� EXP
    public string questGold; // ����Ʈ ���� ���
    public string questCompleteString; // ����Ʈ �Ϸ� �ؽ�Ʈ
}
