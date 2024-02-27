using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ChargeSlash : Skill
{
    [SerializeField] private Hero hero;
    protected override void Awake()
    {
        base.Awake();

    }
 
    public override void UseSkill()
    {
        GameObject projectileSlash = PoolManager.Instance.Get("ProjectileChargeSlash");

        projectileSlash.GetComponent<ProjectileChargeSlash>().Init(transform.position, hero.transform.forward, hero.GetHeroData().GetDamage());

        Debug.Log("use");
    }


}
