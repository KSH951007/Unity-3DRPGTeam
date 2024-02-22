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
    public string introduce;// 첫인사
    public string des; // 첫인사 이후
    private int firstMet; // TODO : NPC 첫인사 구현 // chatBubble 클래스
    public Transform[] moveRnd; //TODO : 총 3방향으로 랜덤 이동 구현 
    NPCState state;
    Animator animator;
    float InteractRange; // 플레이어 상호작용 범위와 같음
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
        animator.SetInteger("State", (int)state); // 애니메이터 파라미터 State

        Collider[] colliderArray = Physics.OverlapSphere(transform.position, InteractRange);
        foreach (Collider collider in colliderArray)
        {
            if (collider.TryGetComponent(out Hero player)) // 플레이어가 올때 상호작용에 필요한 
            {
                bubble.showInteractKey();
                // CHATBUBBLE 클래스의 함수 호출
            }
        }
    }

}
