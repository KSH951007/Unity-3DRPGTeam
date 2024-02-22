using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
public class PlayerInteract : MonoBehaviour, IInteractable
{
    [HideInInspector]
    public int curAmount; // 현재 수락해놓은 퀘스트 갯수
    
    [HideInInspector]
    public int requiredAmount;

    bool questOpen;
    public float InteractRange;
    List<Quest> questArray;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q)) // 인풋 시스템 적용// TODO : 병합되기전까지 사용
        {
            questOpen = true ? !questOpen : questOpen;
         
            openQuestWindow(questOpen);
        }
    }

    public void Interact() // WillDo: 인풋 시스템으로 함수만 가져오게 될것
    {
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, InteractRange);
        foreach (Collider collider in colliderArray)
        {
            if(collider.TryGetComponent(out NPC npc))
            {
                
            }
        }
    }

    public void openQuestWindow(bool isOpen)
    {
        GameManager.Instance.ui.QUI.showQW(isOpen);
    }
    public void questAccept(NPC npc, Quest quest)
    {
        questArray.Add(quest);
        quest.questAccept();
    }
    public void questComplete(NPC npc,Quest quest)
    {
        questArray.Remove(quest);
        quest.questComplete();
    }
}