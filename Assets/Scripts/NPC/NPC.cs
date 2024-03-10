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
    
    private bool questComplete;
    private bool questRunning;
    private bool canQuestAccept;

    public GameObject CompleteQuest;
    public GameObject RunningQuest;
    public DialogueWindow dia;

    [SerializeField]
    private LayerMask Player;

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
        if (nTD.questID <= GameManager.Instance.plin.playerID)
        {
            canQuestAccept = true;

            foreach (var clearQuest in QuestSystem.Instance.CompletedQuests)
            {
                if (clearQuest.CodeName == nTD.npcSubQuest.CodeName)
                {
                    questComplete = true;
                    questRunning = false;
                }
                else
                {
                    questComplete = false;
                }
            }
            foreach (var runningQuest in QuestSystem.Instance.ActiveQuests)
            {
                if (runningQuest.CodeName == nTD.npcSubQuest.CodeName)
                {
                    questRunning = true;
                    questComplete = false;
                }
                else
                {
                    questRunning = false;
                }
            }
        }
        else
        {
            canQuestAccept = false;
        }

        if(canQuestAccept && questRunning)
        {
            RunningQuest.SetActive(true);
        }
        else if (canQuestAccept && !questRunning && !questComplete)
        {
            RunningQuest.SetActive(true);
        }
        else
        {
            RunningQuest.SetActive(false);
        }

        if(!canQuestAccept)
        {
            RunningQuest.SetActive(false);
            CompleteQuest.SetActive(false);
        }

        if(nTD.npcSubQuest.IsComplatable)
        {
            CompleteQuest.SetActive(true);
        }
        else
        {
            CompleteQuest.SetActive(false);
        }



        if (Physics.OverlapSphere(transform.position, 4, Player).Length >= 1)
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
