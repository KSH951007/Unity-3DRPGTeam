using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class HeroMoveAction : HeroAction
{
    private NavMeshAgent agent;
    private float moveSpeed;
    private Vector3 targetPoint;
    private string[] sounds;
    private int currentSoundIndex;
    public HeroMoveAction(ActionScheduler scheduler, Animator animator, Hero owner, NavMeshAgent agent, float moveSpeed, int soundCount, string SoundName) : base(scheduler, animator, owner)
    {
        this.agent = agent;
        this.moveSpeed = moveSpeed;
        agent.speed = this.moveSpeed;

        sounds = new string[soundCount];
        for (int i = 0; i < soundCount; i++)
        {
            sounds[i] = SoundName + i;
        }
    }

    public override bool IsCanle(HeroAction action)
    {

        if (action is HeroMoveAction || action is HeroAttackAction || action is HeroSkillAction)
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
        currentSoundIndex = 0;
        owner.AnimEvent.onStep += StepSound;
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
        owner.AnimEvent.onStep -= StepSound;

        //for (int i = 0; i < sounds.Length; i++)
        //{
        //    if (SoundManager.instance.IsPlaying(sounds[i]))
        //    {
        //        SoundManager.instance.StopSound(sounds[i]);
        //    }
        //}
    }

    public override void UpdateAction()
    {
        animator.SetFloat("Move", agent.remainingDistance);
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            scheduler.ChangeAction();
            return;
        }



    }
    public void StepSound()
    {
        SoundManager.instance.PlaySound(sounds[currentSoundIndex]);
        currentSoundIndex++;
        if (currentSoundIndex >= sounds.Length)
        {
            currentSoundIndex = 0;
        }
    }
}
