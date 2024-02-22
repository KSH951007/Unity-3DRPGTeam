using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackAction : PlayerAction
{
    private Vector3 targetPos;
    private int maxCombo;
    private int curruntAttackCombo;
    float currentTime;
    float attackClipLength;


    public PlayerAttackAction(Animator animator, Hero owner, int maxCombo) : base(animator, owner)
    {
        this.maxCombo = maxCombo;
        curruntAttackCombo = 0;
    }
    public void SetTargetTo(Vector3 targetPos)
    {
        this.targetPos = targetPos;
        if (owner.GetAnimType() != EnumType.HeroAnimType.Battle)
            owner.ChangeAnimatorController(EnumType.HeroAnimType.Battle);

        animator.SetTrigger("Attack");
    }
    public override bool IsCanle(PlayerAction action)
    {


        return false;
    }
    public override void StartAction()
    {


        isEndAction = false;
        currentTime = 0;
        curruntAttackCombo++;
        if (curruntAttackCombo >= maxCombo)
        {
            curruntAttackCombo = 0;
        }
        owner.StartCoroutine(owner.TargetToLoock(targetPos, 0.05f));
    }

    public override void StopAction()
    {


    }

    public override void UpdateAction()
    {


        currentTime += Time.deltaTime;

        attackClipLength = animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
        if (currentTime >= attackClipLength)
        {
            Debug.Log(attackClipLength);
            isEndAction = true;
            return;
        }

    }
   
    public bool IsLastAttack()
    {
        return curruntAttackCombo >= maxCombo;
    }


}
