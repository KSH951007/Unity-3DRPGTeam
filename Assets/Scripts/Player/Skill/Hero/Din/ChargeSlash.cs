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
        GameObject projectileSlash = PoolManager.Instance.Get("ProjectileChargeSlash");

        projectileSlash.GetComponent<ProjectileChargeSlash>().Init(transform.position, hero.transform.forward, hero.GetHeroData().GetDamage());

        CurrentCooldown = skillData.GetCoolDown();
    }

    public override void UpdateSkill()
    {
        base.UpdateSkill();
    }
}
