using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ChargeSlash : Skill
{
    protected override void Awake()
    {
        base.Awake();

    }
    public override void UseSkill()
    {
        base.UseSkill();
        GameObject projectileSlash = PoolManager.Instance.Get("ProjectileChargeSlash");
      
        projectileSlash.GetComponent<ProjectileChargeSlash>().Init(transform.position, hero.transform.forward, hero.GetHeroData().GetDamage());

    }

    public override void UpdateSkill()
    {
        base.UpdateSkill();
    }
}
