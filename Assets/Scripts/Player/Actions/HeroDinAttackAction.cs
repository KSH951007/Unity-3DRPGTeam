using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class HeroDinAttackAction : HeroAttackAction
{
    GameObject attackEffect;
    Transform attackPoint;
    VisualEffect slashEffect;
    public HeroDinAttackAction(Transform attackPoint, GameObject attackEffect, ActionScheduler scheduler, Animator animator, Hero owner, int maxCombo) : base(scheduler, animator, owner, maxCombo)
    {
        this.attackPoint = attackPoint;
        this.attackEffect = attackEffect;
        slashEffect = GameObject.Instantiate(attackEffect, attackPoint.position, attackPoint.rotation).GetComponent<VisualEffect>();

        this.scheduler = scheduler;
        this.maxCombo = maxCombo;
        curruntAttackCombo = 0;

        
    }
    public override bool IsCanle(HeroAction action)
    {


        return false;
    }
    public override void StartAction()
    {
        base.StartAction();

        owner.AnimEvent.onStartAttack += StartAttack;
        owner.AnimEvent.onProgressAttack += ProgressAttack;
        owner.AnimEvent.onEndAttack += EndAttack;


    }

    public override void StopAction()
    {
        owner.AnimEvent.onStartAttack -= StartAttack;
        owner.AnimEvent.onProgressAttack -= ProgressAttack;
        owner.AnimEvent.onEndAttack -= EndAttack;
        isStartAttack = false;
        isEndAttack = false;
    }
    public override void UpdateAction()
    {
        if (scheduler.GetNextAction() == this)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName($"attack{curruntAttackCombo}") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.7f)
            {
                isEndAction = true;
                return;
            }
        }
        else
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName($"attack{curruntAttackCombo}") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
            {
                isEndAction = true;
                curruntAttackCombo = 0;
                return;
            }
        }

    }
    public void StartAttack()
    {
        isStartAttack = true;
    }
    public void EndAttack()
    {
        isEndAttack = true;
    }
    public void ProgressAttack()
    {
        slashEffect.transform.position = attackPoint.position;
        slashEffect.transform.rotation = attackPoint.rotation;
        slashEffect.Play();

        RaycastHit[] hits = Physics.BoxCastAll(owner.transform.position, owner.transform.lossyScale / 2, owner.transform.forward, Quaternion.identity, 1f);
        if (hits != null)
        {
            foreach (RaycastHit hitObject in hits)
            {
                if (hitObject.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    if (hitObject.transform.gameObject.TryGetComponent(out Health health))
                    {
                        health.TakeHit(512, HitType.None);
                    }
                }

            }
        }
    }
}
