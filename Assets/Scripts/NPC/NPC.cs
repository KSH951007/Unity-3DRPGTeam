using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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

    public bool dialogueNow = false;
    
    public GameObject pressSpace;
    public NPCData nTD;
    
    private bool questComplete;
    private bool questRunning;
    private bool canQuestAccept;

    public GameObject CompleteQuest;
    public GameObject RunningQuest;
    public GameObject Canaccept;
    public DialogueWindow dia;

    [SerializeField]
    private LayerMask Player;

    private void Awake()
    {
        state = NPCState.Idle;
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        StartCoroutine(stateMachine());
    }
    private void Update()
    {
        CompleteCheck();
        RunningCheck();

        if (canQuestAccept)
        {
            Canaccept.SetActive(false);
            CompleteQuest.SetActive(false);
            RunningQuest.SetActive(false);

            if (questRunning && !questComplete) // 퀘스트 진행중
            {
                RunningQuest.SetActive(true);
            }
            else if (!questRunning && !questComplete) // 퀘스트 수락 가능
            {
                Canaccept.SetActive(true);
            }
            else if (questRunning && questComplete) // 퀘스트 클리어 가능
            {
                CompleteQuest.SetActive(true);
            }
            else
            {
                Canaccept.SetActive(false);
                CompleteQuest.SetActive(false);
                RunningQuest.SetActive(false);
            }
        }
        else
        {
            Canaccept.SetActive(false);
            CompleteQuest.SetActive(false);
            RunningQuest.SetActive(false);
        }



        if (Physics.OverlapSphere(transform.position, 2, Player).Length >= 1)
        {
            this.pressSpace.SetActive(true);
        }
        else
        {
            this.pressSpace.SetActive(false);
        }
        animator.SetInteger("State", (int)state); // 애니메이터 파라미터 State
    }

    IEnumerator stateMachine() // WillDO : NPC 상태머신 완성
    {
        while (true)
        {
            switch (state)
            {
                case NPCState.Idle: // 0
                    
                    break;
                case NPCState.Talk: // 1
                    Vector3 dir = GameManager.Instance.plin.transform.position - transform.position;// TODO : player 클래스
                    dir.y = 0;
                    transform.rotation = Quaternion.LookRotation(dir).normalized;
                    state = NPCState.Idle;
                    break;
            }
            yield return null;
        }
    }

    private void CompleteCheck()
    {
        foreach (var quest in QuestSystem.Instance.ActiveQuests)
        {
            if (nTD.npcSubQuest.CodeName == quest.CodeName)
            {
                if (quest.IsComplatable == true)
                    questComplete = true;

                else
                    questComplete = false;
            }
        }
    }
    private void RunningCheck()
    {
        if (nTD.questID <= GameManager.Instance.plin.playerID)
        {
            canQuestAccept = true;

            foreach (var runningQuest in QuestSystem.Instance.ActiveQuests)
            {
                if (runningQuest.CodeName == nTD.npcSubQuest.CodeName)
                {
                    questRunning = true;
                    return;
                }
                else
                    questRunning = false;
            }
        }
        else
            canQuestAccept = false;
    }


    public void subQuestComplete()
    {
        nTD.npcSubQuest.Complete();
    }

    public void Ontalk(NPC npc) // 대화
    {
        npc.dia.GiveComponent(npc.nTD);
        npc.state = NPCState.Talk;
    }
}
