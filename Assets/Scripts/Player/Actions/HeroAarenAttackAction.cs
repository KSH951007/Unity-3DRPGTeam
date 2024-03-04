using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAarenAttackAction : HeroAttackAction
{

    public HeroAarenAttackAction(ActionScheduler scheduler, Animator animator, Hero owner) : base(scheduler, animator, owner)
    {

    }

    public override void ProgressAttack()
    {
        GameObject projectile = PoolManager.Instance.Get("ProjectileAarenAttack",owner.AttackPoint.position,owner.AttackPoint.rotation);
        if (projectile != null)
        {
            Debug.Log(projectile.transform.position);
            projectile.GetComponent<ProjectileAarenAttack>().Init(owner.GetHeroData().GetDamage(), owner.AttackPoint.position, owner.transform.forward);
            Debug.Log(projectile.transform.position);

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
