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
        owner.transform.LookAt(targetPos);
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

        Debug.Log(animator.GetCurrentAnimatorStateInfo(0).normalizedTime);

    }
    public IEnumerator TargetToLoock(Vector3 targetPos, float smoothTime)
    {
        Vector3 direction = (targetPos - this.owner.transform.position).normalized;
        Vector3 velocity = Vector3.zero;
        while (Vector3.Dot(owner.transform.forward, direction) <= 0.99f)
        {
            owner.transform.forward = Vector3.SmoothDamp(owner.transform.forward, direction, ref velocity, smoothTime);

            yield return null;
        }
    }


}
