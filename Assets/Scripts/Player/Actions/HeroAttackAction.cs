using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAttackAction : HeroAction
{
    protected Vector3 targetPos;
    protected int maxCombo;
    protected int curruntAttackCombo;
    protected bool isStartAttack;
    protected bool isEndAttack;


    public bool IsLastAttack()
    {
        return curruntAttackCombo == maxCombo;
    }
    public HeroAttackAction(ActionScheduler scheduler, Animator animator, Hero owner, int maxCombo) : base(scheduler,animator, owner)
    {
        this.scheduler = scheduler;
        this.maxCombo = maxCombo;
        curruntAttackCombo = 0;
    }
    public void SetTargetTo(Vector3 targetPos)
    {
        this.targetPos = targetPos;
        if (owner.GetAnimType() != EnumType.HeroAnimType.Battle)
            owner.ChangeAnimatorController(EnumType.HeroAnimType.Battle);

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
        if (curruntAttackCombo > maxCombo)
        {
            curruntAttackCombo = 0;
        }
        owner.StartCoroutine(owner.TargetToLoock(targetPos, 0.05f));
    }

    public override void StopAction()
    {
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

        Debug.Log("asd");


    }


}
