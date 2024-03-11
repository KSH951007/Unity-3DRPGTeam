using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class HeroDinAttackAction : HeroAttackAction
{
    GameObject attackEffect;
    VisualEffect slashEffect;
    public HeroDinAttackAction(GameObject attackEffect, ActionScheduler scheduler, Animator animator, Hero owner) : base(scheduler, animator, owner)
    {
        this.attackEffect = attackEffect;
        slashEffect = GameObject.Instantiate(attackEffect, owner.AttackPoint.position, owner.AttackPoint.rotation).GetComponent<VisualEffect>();
        slashEffect.gameObject.SetActive(false);
        this.scheduler = scheduler;
        curruntAttackCombo = 0;


    }
    public override bool IsCanle(HeroAction action)
    {
        return false;
    }
    public override void UpdateAction()
    {
        if (isEndAction)
        {
            if (scheduler.GetNextAction() != this)
            {
                curruntAttackCombo = 0;
            }
            scheduler.ChangeAction();
            return;
        }

    }
    public override void ProgressAttack()
    {
        if (!slashEffect.gameObject.activeSelf)
            slashEffect.gameObject.SetActive(true);
        slashEffect.transform.position = owner.AttackPoint.position;
        slashEffect.transform.rotation = owner.AttackPoint.rotation;
        slashEffect.Play();

        SoundManager.instance.PlaySound("HeroDinAttack" + curruntAttackCombo);
        SoundManager.instance.PlaySound("HeroDinAttackVoice" + curruntAttackCombo);
        Debug.Log(curruntAttackCombo);

        RaycastHit[] hits = Physics.BoxCastAll(owner.transform.position, owner.transform.lossyScale / 2, owner.transform.forward, Quaternion.identity, 1f);
        if (hits != null)
        {
            foreach (RaycastHit hitObject in hits)
            {
                if (hitObject.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    if (hitObject.transform.gameObject.TryGetComponent(out IHitable enemy))
                    {
                        enemy.TakeHit(owner.GetHeroData().GetDamage(), IHitable.HitType.None);
                    }
                }

            }
        }
    }
}
