using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoShower : Skill
{
    public override void UseSkill()
    {
        base.UseSkill();
        GameObject projectile = PoolManager.Instance.Get("ProjectileMeteoShower");
        if (projectile != null)
        {
            projectile.GetComponent<ProjectileMeteoShower>().Init(hero.AttackPoint.position, hero.AttackPoint.rotation, hero.GetHeroData().GetDamage(),hero.HeroAnimator);            
        }

    }


}
