using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMover : MonoBehaviour, IPlayerAction
{
    private NavMeshAgent agent;
    private float moveSpeed;
    

    public NavMeshAgent Agent { get { return agent; } }
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        moveSpeed = 4f;
    }
    public void MoveTo(Vector3 targetPos)
    {
        Cancle();

        agent.isStopped = false;
        agent.speed = moveSpeed;
        agent.SetDestination(targetPos);
    }
    public bool Whetherstatus()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            Cancle();
            return true;
        }
        return false;
    }

    public void Cancle()
    {
        if (agent.hasPath)
        {
            agent.ResetPath();
            agent.isStopped = true;
            agent.velocity = Vector3.zero;
        }
    }
}
