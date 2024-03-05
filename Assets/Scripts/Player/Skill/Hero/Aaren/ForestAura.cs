using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestAura : Skill
{
    protected override void Awake()
    {
        base.Awake();
    }

    public override void UseSkill()
    {
        base.UseSkill();
        GameObject projectile = PoolManager.Instance.Get("ProjectileForestAura");
        if (projectile != null)
        {
            if(transform.childCount > 0)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(i).GetComponent<ProjectileForestAura>().Stop();
                }
            }
            projectile.GetComponent<ProjectileForestAura>().Init(hero, hero.GetHeroData().GetDamage());
          
        }

    }

    public override void UpdateSkill()
    {
        base.UpdateSkill();
    }
}
