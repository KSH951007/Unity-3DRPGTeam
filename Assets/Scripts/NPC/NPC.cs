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

    public List<Transform> moveRnd; //WILLDO : 총 3방향으로 랜덤 이동 구현 
    float InteractRange; // 플레이어 상호작용 범위와 같음
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
        animator.SetInteger("State", (int)state); // 애니메이터 파라미터 State

        Collider[] colliderArray = Physics.OverlapSphere(transform.position, InteractRange);
        foreach (Collider collider in colliderArray)
        {
            if (collider.TryGetComponent(out Hero player)) // 플레이어가 올때 상호작용에 필요한 
            {
                playerIsHere = true;
                bubble.pressSpace.SetActive(true);
                // CHATBUBBLE 클래스의 함수 호출
            }
            else
            {
                playerIsHere = false;
                bubble.pressSpace.SetActive(false);
            }
        }
    }
    IEnumerator walk() // WILLDO : NPC 걷는 로직 완성
    {
        yield return null;
    }

    IEnumerator stateMachine() // WillDO : NPC 상태머신 완성
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
