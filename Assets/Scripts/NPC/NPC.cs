using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public enum NPCState
{
    Idle = 0,
    Walk = 1 ,
    Talk = 2
}
public class NPC : MonoBehaviour
{
    public string name;
    public string introduce;// ù�λ�
    public string des; // ù�λ� ����
    private int firstMet; // TODO : NPC ù�λ� ���� // chatBubble Ŭ����
    public Transform[] moveRnd; //TODO : �� 3�������� ���� �̵� ���� 
    NPCState state;
    Animator animator;
    float InteractRange; // �÷��̾� ��ȣ�ۿ� ������ ����
    ChatBubble bubble;

    private void Awake()
    {
        state = NPCState.Idle;
        animator = GetComponent<Animator>();
        bubble = GetComponent<ChatBubble>();
    }
    private void Update()
    {
        switch (state)
        {
            case NPCState.Idle: // 0
                break;
            case NPCState.Walk: // 1
                break;
            case NPCState.Talk: // 2
                break;
        }
        animator.SetInteger("State", (int)state); // �ִϸ����� �Ķ���� State

        Collider[] colliderArray = Physics.OverlapSphere(transform.position, InteractRange);
        foreach (Collider collider in colliderArray)
        {
            if (collider.TryGetComponent(out Hero player)) // �÷��̾ �ö� ��ȣ�ۿ뿡 �ʿ��� 
            {
                bubble.showInteractKey();
                // CHATBUBBLE Ŭ������ �Լ� ȣ��
            }
        }
    }

}
