using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerInteract : MonoBehaviour, IInteractable
{
    public float InteractRange;

    public void Interact() // WillDo: ��ǲ �ý������� �Լ��� �������� �ɰ�
    {
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, InteractRange);
        foreach (Collider collider in colliderArray)
        {
            if(collider.TryGetComponent(out NPC npc))
            {
               //npc�� ��ȣ�ۿ��� �����Ҷ� ��ȣ �ۿ� Ű�� ���� UIȣ��
            }
        }
    }
}