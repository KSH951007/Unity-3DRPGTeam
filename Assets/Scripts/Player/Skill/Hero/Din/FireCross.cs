using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCross : Skill
{


    protected override void Awake()
    {
        base.Awake();
    }
    public override void UseSkill()
    {
        base.UseSkill();
        GameObject projectileFireCross = PoolManager.Instance.Get("ProjectileFireCross");
        projectileFireCross.transform.position = hero.transform.position;
        projectileFireCross.transform.rotation = Quaternion.FromToRotation(projectileFireCross.transform.forward, hero.transform.forward);
        projectileFireCross.GetComponent<ProjectileFireCross>().Init(hero.data.damage);

    }

    public override void UpdateSkill()
    {
        base.UpdateSkill();
    }
}
