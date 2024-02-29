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
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) //TODO : 인풋 시스템 적용//  병합 되기전까지 사용
        {
            GameManager.Instance.ui.QUI.showQW();
        }
        if (Input.GetKeyDown(KeyCode.R)) //TODO : 인풋 시스템 적용 //  병합 되기전까지 사용
        {
            Interact();
        }
    }

    public void Interact() // WillDo: 인풋 시스템으로 함수만 가져오게 될것
    {
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, InteractRange);
        foreach (Collider collider in colliderArray)
        {
            if (collider.TryGetComponent(out NPC npc))
            {
                isDialogue = true;
                GameManager.Instance.ui.QUI.showConversation();
                npc.bubble.Ontalk(npc);
            }
        }
    }
    public void nextDialogue(NPC npc)
    {
        npc.bubble.Ontalk(npc);
    }

}