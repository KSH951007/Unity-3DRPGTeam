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

}