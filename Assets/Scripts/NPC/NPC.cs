using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UIElements;

public enum NPCState
{
    Idle = 0,
    Talk = 1
}
public class NPC : MonoBehaviour
{
    NPCState state;
    Animator animator;

    public Transform[] moveRnd; //WILLDO : �� 3�������� ���� �̵� ���� 
    float InteractRange; // �÷��̾� ��ȣ�ۿ� ������ ����
    public bool dialogueNow = false;
    
    public GameObject pressSpace;
    public NPCData nTD;
    
    public bool questCompleteNow;
    public GameObject CompleteQuest;
    public GameObject RunningQuest;
    public DialogueWindow dia;

    private void Awake()
    {
        InteractRange = 2f;
        state = NPCState.Idle;
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        StartCoroutine(stateMachine());
    }
    private void Update()
    {
        if(nTD.npcSubQuest.IsComplatable)
        {
            this.CompleteQuest.SetActive(true);
        }
        else
        {
            this.CompleteQuest.SetActive(false);

        }

        if (nTD.questID <= GameManager.Instance.plin.playerID && !nTD.npcSubQuest.IsComplete && !nTD.npcSubQuest.IsRegistered)
        {
            this.RunningQuest.SetActive(true);
        }
        else 
        {
            this.RunningQuest.SetActive(false);
        }

        if (Vector3.Distance(transform.position, GameManager.Instance.plin.transform.position) < InteractRange) // TODO : player Ŭ����
        {
            this.pressSpace.SetActive(true);
        }
        else
        {
            this.pressSpace.SetActive(false);
        }
        animator.SetInteger("State", (int)state); // �ִϸ����� �Ķ���� State
    }

    IEnumerator stateMachine() // WillDO : NPC ���¸ӽ� �ϼ�
    {
        while (true)
        {
            switch (state)
            {
                case NPCState.Idle: // 0
                    
                    break;
                case NPCState.Talk: // 1
                    Vector3 dir = GameManager.Instance.plin.transform.position - transform.position;// TODO : player Ŭ����
                    dir.y = 0;
                    transform.rotation = Quaternion.LookRotation(dir).normalized;
                    state = NPCState.Idle;
                    break;
            }
            yield return null;
        }
    }

    public void subQuestComplete()
    {
        nTD.npcSubQuest.Complete();
    }

    public void Ontalk(NPC npc) // ��ȭ
    {
        npc.dia.GiveComponent(npc.nTD);
        npc.state = NPCState.Talk;
    }
}
