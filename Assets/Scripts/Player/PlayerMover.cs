using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMover : MonoBehaviour
{
    private NavMeshAgent agent;
    private float moveSpeed;


    public NavMeshAgent Agent {  get { return agent; } }
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        moveSpeed = 4f;
    }
    public void MoveTo(Vector3 targetPos)
    {
        if (agent.hasPath)
        {
            agent.ResetPath();
            agent.velocity = Vector3.zero;
        }

        agent.speed = moveSpeed;
        agent.SetDestination(targetPos);
    }
    public bool Whetherstatus()
    {
        if(agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.ResetPath();
            agent.velocity = Vector3.zero;
            return true;
        }
        return false;
    }


}
