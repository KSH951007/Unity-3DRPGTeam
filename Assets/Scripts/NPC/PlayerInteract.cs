using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.UI;
public class PlayerInteract : MonoBehaviour, IInteractable
{
    public int playerID;

    float InteractRange = 2f;
    bool isDialogue = false; // TODO : true �϶� ������ ����
<<<<<<< HEAD
    int EXP;
    int Gold;
=======
>>>>>>> Sample
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) //TODO : ��ǲ �ý��� ����//  ���� �Ǳ������� ���
        {
            GameManager.Instance.qui.showQW();
        }
        if (Input.GetKeyDown(KeyCode.R)) //TODO : ��ǲ �ý��� ���� //  ���� �Ǳ������� ���
        {
            Interact();
        }
    }

    public void Interact() // WillDo: ��ǲ �ý������� �Լ��� �������� �ɰ�
    {
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, InteractRange);
        foreach (Collider collider in colliderArray)
        {
            if (collider.TryGetComponent(out NPC npc))
            {
<<<<<<< HEAD
                if (npc.nTD.npcSubQuest.IsComplatable)
                {
                    npc.nTD.npcSubQuest.Complete();
                }
=======
>>>>>>> Sample
                isDialogue = true;
                GameManager.Instance.qui.showConversation();
                npc.Ontalk(npc);
            }
        }
    }
    public void nextDialogue(NPC npc)
    {
        npc.Ontalk(npc);
    }

<<<<<<< HEAD
    public int GetEXP(int i) // ToDo : ����Ʈ ���� // �κ��丮 �߰��� ���
    {
        EXP = i + EXP;
        return EXP;
    }
    public int GetGold(int i)
    {
        Gold = i + Gold;
        return Gold;
    }
=======
>>>>>>> Sample
}