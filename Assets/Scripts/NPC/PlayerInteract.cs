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
    bool isDialogue = false; // TODO : true 일때 움직임 제한
    int EXP;
    int Gold;
    Transform heroTr;

    public Transform MainHeroTranform { set => heroTr = value; }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) //TODO : 인풋 시스템 적용//  병합 되기전까지 사용
        {
            GameManager.Instance.qui.showQW();
        }
    }

    public void Interact() // WillDo: 인풋 시스템으로 함수만 가져오게 될것
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

    public int GetEXP(int i) // ToDo : 퀘스트 보상 // 인벤토리 추가후 사용
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