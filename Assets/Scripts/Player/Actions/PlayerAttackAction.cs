using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackAction : PlayerAction
{
    private Vector3 targetPos;
    private int maxCombo;
    protected int curruntAttackCombo;
    float currentTime;
    float attackClipLength;
    Hero mainHero;


    public PlayerAttackAction(Animator animator, PlayerController owner, Hero mainHero, int maxCombo) : base(animator, owner)
    {
        this.maxCombo = maxCombo;
        this.mainHero = mainHero;
        curruntAttackCombo = 0;
    }
    public void SetTargetTo(Vector3 targetPos)
    {
        this.targetPos = targetPos;
        if (mainHero.GetAnimType() != EnumType.HeroAnimType.Battle)
            mainHero.ChangeAnimatorController(EnumType.HeroAnimType.Battle);
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
        owner.StartCoroutine(TargetToLoock(targetPos,0.1f));
        owner.transform.forward = targetPos;
        //owner.StartCoroutine(TargetToLoock(targetPos,0.1f));
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
            isEndAction = true;
            return;
        }

    }
    public IEnumerator TargetToLoock(Vector3 targetPos, float smoothTime)
    {
        Vector3 velocity = Vector3.zero;
        while (Vector3.Dot(owner.transform.forward, targetPos) <= 0.99f)
        {
            owner.transform.forward = Vector3.SmoothDamp(owner.transform.forward, targetPos, ref velocity, smoothTime);

            yield return null;
        }
    }
    public bool IsLastAttack()
    {
        return curruntAttackCombo >= maxCombo;
    }


}
