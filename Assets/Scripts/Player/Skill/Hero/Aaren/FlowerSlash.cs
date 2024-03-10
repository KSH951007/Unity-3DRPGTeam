using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerSlash : Skill
{
    private float damagePercent;

    protected override void Awake()
    {
        base.Awake();
    }
    public override void UseSkill()
    {
        base.UseSkill();
        GameObject projectile = PoolManager.Instance.Get("ProjectileFlowerSlash");
        if (projectile != null)
        {
            projectile.GetComponent<ProjectileFlowerSlash>().Init(transform.position,hero.data.damage);
           
        }
    }
    public override void UpdateSkill()
    {
        base.UpdateSkill();
    }


}
