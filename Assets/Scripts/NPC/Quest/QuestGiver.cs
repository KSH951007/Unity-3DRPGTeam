using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    [SerializeField]
    private List<Quest> quests;

    private void Start()
    {
        foreach (var quest in quests)
        {
            if (quest.IsAcceptable && !QuestSystem.Instance.ContainsInCompleteQuests(quest))
                QuestSystem.Instance.Register(quest);
        }
    }

<<<<<<< HEAD
    public void giveQuest(Quest quest)
    {
        quests.Add(quest);
    }
=======
    public void addQuest(Quest quest)
    {
        if (quest.IsAcceptable && !QuestSystem.Instance.ContainsInActiveAchievements(quest))
            quests.Add(quest);   
    }   
>>>>>>> Sample
}
