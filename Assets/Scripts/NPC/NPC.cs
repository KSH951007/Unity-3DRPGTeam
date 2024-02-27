using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public int npcID;

    NPCState state;
    Animator animator;

    public List<Transform> moveRnd; //WILLDO : �� 3�������� ���� �̵� ���� 
    float InteractRange; // �÷��̾� ��ȣ�ۿ� ������ ����
    public bool playerIsHere = false;
    public bool dialogueNow = false;

    NPCUI bubble;
    private void Awake()
    {
        InteractRange = 2f;
        state = NPCState.Idle;
        animator = GetComponent<Animator>();
        bubble = GetComponent<NPCUI>();
    }
    private void Start()
    {
        StartCoroutine(stateMachine());
    }
    private void Update()
    {
        animator.SetInteger("State", (int)state); // �ִϸ����� �Ķ���� State

        Collider[] colliderArray = Physics.OverlapSphere(transform.position, InteractRange);
        foreach (Collider collider in colliderArray)
        {
            if (collider.TryGetComponent(out Hero player)) // �÷��̾ �ö� ��ȣ�ۿ뿡 �ʿ��� 
            {
                playerIsHere = true;
                bubble.pressSpace.SetActive(true);
                // CHATBUBBLE Ŭ������ �Լ� ȣ��
            }
            else
            {
                playerIsHere = false;
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
        while (true)
        {
            switch (state)
            {
                case NPCState.Idle: // 0
                    break;
                case NPCState.Walk: // 1
                    break;
                case NPCState.Talk: // 2
                    state = NPCState.Idle;
                    break;
            }
            yield return null;
        }
    }

}
