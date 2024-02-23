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
    public int requiredAmount;// 수락 가능한 퀘스트 갯수

    float InteractRange = 2f;
    List<Quest> questArray;
    

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q)) //TODO : 인풋 시스템 적용//  병합 되기전까지 사용
        {
            GameManager.Instance.ui.QUI.showQW();
        }
        if(Input.GetKeyDown(KeyCode.R)) //TODO : 인풋 시스템 적용 //  병합 되기전까지 사용
        {
            Interact();
        }
    }

    public void Interact() // WillDo: 인풋 시스템으로 함수만 가져오게 될것
    {
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, InteractRange);
        foreach (Collider collider in colliderArray)
        {
            if(collider.TryGetComponent(out NPC npc))
            {
                GameManager.Instance.ui.QUI.showConversation();
            }
        }
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