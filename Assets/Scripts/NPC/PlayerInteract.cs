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
    int EXP;
    int Gold;
    Transform heroTr;

    public Transform MainHeroTranform { set => heroTr = value; }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) //TODO : ��ǲ �ý��� ����//  ���� �Ǳ������� ���
        {
            GameManager.Instance.qui.showQW();
        }
    }

    public void Interact() // WillDo: ��ǲ �ý������� �Լ��� �������� �ɰ�
    {
        Collider[] colliderArray = Physics.OverlapSphere(heroTr.position, InteractRange);
        foreach (Collider collider in colliderArray)
        {
            if (collider.TryGetComponent(out NPC npc))
            {
                //if (npc.nTD.npcSubQuest.IsComplatable)
                //{
                //    npc.nTD.npcSubQuest.Complete();
                //}
                isDialogue = true;
                npc.Ontalk(npc);
                print(npc.nTD.name);
                GameManager.Instance.qui.showConversation();
            }
        }
    }

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
}