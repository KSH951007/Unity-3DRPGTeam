using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class HeroJuliaAttackAction : HeroAttackAction
{
    public HeroJuliaAttackAction(ActionScheduler scheduler, Animator animator, Hero owner) : base(scheduler, animator, owner)
    {

    }

    public override void ProgressAttack()
    {


        GameObject projectile = PoolManager.Instance.Get("ProjectileJuliaAttack",owner.AttackPoint.position,owner.AttackPoint.rotation);
        if (projectile != null)
        {
            projectile.GetComponent<ProjectileJuliaAttack>().Init(owner.AttackPoint.position, owner.data.damage);
       
        }

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


}
