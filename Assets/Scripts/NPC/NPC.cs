using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UIElements;

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

    public Transform[] moveRnd; //WILLDO : �� 3�������� ���� �̵� ���� 
    float InteractRange; // �÷��̾� ��ȣ�ۿ� ������ ����
    public bool dialogueNow = false;
    
    public GameObject pressSpace;
    public NPCData nTD;

    public DialogueWindow dia;

    private int rndInt;

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
        //if(false == nTD.npcSubQuest.IsComplatable && GameManager.Instance.plin.playerID >= nTD.questID)
        //{
             // ����Ʈ ���� �˸�
        //}

        if (Vector3.Distance(transform.position, GameManager.Instance.plin.transform.position) < InteractRange)
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
            state = NPCState.Idle;
            switch (state)
            {
                case NPCState.Idle: // 0
                    if(nTD.isMove)
                    {
                        this.rndInt = Random.Range(0, moveRnd.Length - 1);
                        Vector3 TarPos = moveRnd[rndInt].position;
                        Vector3 Dir = (TarPos - transform.position).normalized;
                        this.transform.LookAt(TarPos);
                        yield return new WaitForSeconds(3f);
                    }
                    break;
                case NPCState.Talk: // 2
                    Vector3 dir = GameManager.Instance.plin.transform.position - transform.position;
                    dir.y = 0;
                    transform.rotation = Quaternion.LookRotation(dir).normalized;
                    state = NPCState.Idle;
                    break;
            }
            yield return null;
        }
    }



    public void Ontalk(NPC npc) // ��ȭ
    {
        npc.dia.GiveComponent(npc.nTD);
        npc.state = NPCState.Talk;
    }


}