using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HeroAttackAction : HeroAction
{
    protected Vector3 targetPos;
    protected int curruntAttackCombo;


    public bool IsLastAttack()
    {
        return curruntAttackCombo == owner.data.maxAttackCombo;
    }
    public HeroAttackAction(ActionScheduler scheduler, Animator animator, Hero owner) : base(scheduler, animator, owner)
    {
        this.scheduler = scheduler;
        curruntAttackCombo = 0;
    }
    public void SetTargetTo(Vector3 targetPos)
    {
        this.targetPos = targetPos;
    }
    public override bool IsCanle(HeroAction action)
    {


        return false;
    }
    public override void StartAction()
    {
        animator.SetTrigger("Attack");
        isEndAction = false;
        curruntAttackCombo++;
        if (curruntAttackCombo > owner.data.maxAttackCombo)
        {
            curruntAttackCombo = 0;
        }
        owner.StartCoroutine(owner.TargetToLoock(targetPos, 0.05f));
        owner.AnimEvent.onProgressAttack += ProgressAttack;
        owner.AnimEvent.onEndAttack += EndAttack;
    }

    public override void StopAction()
    {
        owner.AnimEvent.onProgressAttack -= ProgressAttack;
        owner.AnimEvent.onEndAttack -= EndAttack;
        isEndAction = false;
    }
    public override void UpdateAction()
    {

        if (scheduler.GetNextAction() == this)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName($"attack{curruntAttackCombo}") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.7f)
            {
                scheduler.ChangeAction();
                return;
            }
        }
        else
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName($"attack{curruntAttackCombo}") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
            {

                isEndAction = true;
                curruntAttackCombo = 0;
                scheduler.ChangeAction();
                return;
            }
        }

  
    }
    public abstract void ProgressAttack();
    public virtual void EndAttack()
    {
        isEndAction = true;
    }
}
