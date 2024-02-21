using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerInteract : MonoBehaviour, IInteractable
{
    public float InteractRange;

    public void Interact() // WillDo: 인풋 시스템으로 함수만 가져오게 될것
    {
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, InteractRange);
        foreach (Collider collider in colliderArray)
        {
            if(collider.TryGetComponent(out NPC npc))
            {
               //npc와 상호작용이 가능할때 상호 작용 키를 통한 UI호출
            }
        }
    }
}