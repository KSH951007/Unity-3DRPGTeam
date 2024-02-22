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
    NPCState state;
    Animator animator;

    public string name;
    public string introduce;// ù�λ�
    public string des; // ù�λ� ����
    private int firstMet; // WILLDO : NPC ù�λ� ���� // chatBubble Ŭ����
    public List<Transform> moveRnd; //WILLDO : �� 3�������� ���� �̵� ���� 
    float InteractRange; // �÷��̾� ��ȣ�ۿ� ������ ����
    
    bool needHelp;
    NPCUI bubble;
    Quest quest;

    private void Awake()
    {
        InteractRange = 4f;
        state = NPCState.Idle;
        animator = GetComponent<Animator>();
        bubble = GetComponent<NPCUI>();
        quest = GetComponent<Quest>();
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
        //animator.SetInteger("State", (int)state); // �ִϸ����� �Ķ���� State

        Collider[] colliderArray = Physics.OverlapSphere(transform.position, InteractRange);
        foreach (Collider collider in colliderArray)
        {
            if (collider.TryGetComponent(out Hero player)) // �÷��̾ �ö� ��ȣ�ۿ뿡 �ʿ��� 
            {
                bubble.pressSpace.SetActive(true);
                // CHATBUBBLE Ŭ������ �Լ� ȣ��
            }
            else
            {
                bubble.pressSpace.SetActive(false);
            }
        }
    }
    IEnumerator walk() // WILLDO : NPC �ȴ� ���� �ϼ�
    {
        yield return null;
    }

    IEnumerator stateMachine() // WillDO : NPC ���¸ӽ� �ϼ�
    {
        yield return null;
    }

}
