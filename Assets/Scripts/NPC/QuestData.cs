using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest Data", menuName = "Scriptable Object/Quest Data", order = int.MinValue)]
public class QuestData : ScriptableObject
{
    public int questID;
    private int PlayerProgress;
    public int playProgress { get { return PlayerProgress; } }  // �÷��� ��ô���� ���� // ����Ʈ ����
    public int playerLevel; // �÷��̾� ���� or �÷��̾� ��ô�� ������ �������Կ�

    [HideInInspector]
    public bool needHelp;   // ����Ʈ�� ������ NPC��

    public string playerName; // �÷��̾� �̸�
    
    public string questTitle; // ����Ʈ Ÿ��Ʋ
    public string questString; // �� ���Ǿ����� ����Ʈ�� �ؽ�Ʈ(���丮��)
    
    public string questDetail; // ����Ʈ ���� /ex) ���� 10���� ���
    public int detailInteger;

    public string rewardEXP; // ����Ʈ ���� EXP
    public string questGold; // ����Ʈ ���� ���
    public string questCompleteString; // ����Ʈ �Ϸ� �ؽ�Ʈ
}
