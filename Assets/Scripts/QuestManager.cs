using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;

    public List<QuestData> qDataList = new List<QuestData>();
    private Dictionary<int, Quest> questDic = new Dictionary<int, Quest>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        foreach(QuestData questData in qDataList)
        {
            Quest quest = new Quest(questData);
            questDic.Add(quest.qData.questID, quest);
        }
    }

    public Quest GetQuest(int questID)
    {
        if (questDic.ContainsKey(questID))
        {
            return questDic[questID];
        }
        return null;
    }

    public void UpdateQuestProgress(int questID, int amount)
    {
        Quest quest = GetQuest(questID);
        if (quest != null)
        {
            //quest.Progress(amount);
        }
    }

    public string CheckQuestCompletion(int questID)
    {
        Quest quest = GetQuest(questID);
        if (quest != null && quest.isComplete)
        {
            return quest.qData.rewardEXP;
        }
        return null;
    }

}
