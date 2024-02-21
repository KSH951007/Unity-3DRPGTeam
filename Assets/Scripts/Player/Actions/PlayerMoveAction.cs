using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMoveAction : PlayerAction
{
    private NavMeshAgent agent;
    private float moveSpeed;
    private Vector3 targetPoint;

    public PlayerMoveAction(Animator animator, Hero owner, NavMeshAgent agent, float moveSpeed) : base(animator, owner)
    {
        this.agent = agent;
        this.moveSpeed = moveSpeed;
        agent.speed = this.moveSpeed;
    }

    public override bool IsCanle(PlayerAction action)
    {

        if (action is PlayerMoveAction || action is PlayerAttackAction)
        {
            return true;
        }

        return false;

    }

    public void SetMovePoint(Vector3 movePoint)
    {
        targetPoint = movePoint;
    }
    public override void StartAction()
    {
        isEndAction = false;
        agent.isStopped = false;
        agent.speed = moveSpeed;
        agent.SetDestination(targetPoint);
        animator.SetFloat("Move", agent.remainingDistance);
    }

    public override void StopAction()
    {
        if (agent.hasPath)
        {
            agent.ResetPath();
            animator.SetFloat("Move", 0f);
            agent.isStopped = true;
            agent.velocity = Vector3.zero;
        }

    }

    public override void UpdateAction()
    {
        animator.SetFloat("Move", agent.remainingDistance);
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            isEndAction = true;
            return;
        }
    }
}
