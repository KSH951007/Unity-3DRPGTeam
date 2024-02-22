using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
public class PlayerInteract : MonoBehaviour, IInteractable
{
    [HideInInspector]
    public int curAmount; // ���� �����س��� ����Ʈ ����
    
    [HideInInspector]
    public int requiredAmount;

    bool questOpen;
    public float InteractRange;
    List<Quest> questArray;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q)) // ��ǲ �ý��� ����// TODO : ���յǱ������� ���
        {
            questOpen = true ? !questOpen : questOpen;
         
            openQuestWindow(questOpen);
        }
    }

    public void Interact() // WillDo: ��ǲ �ý������� �Լ��� �������� �ɰ�
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