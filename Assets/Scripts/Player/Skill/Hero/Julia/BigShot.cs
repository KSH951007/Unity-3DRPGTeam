using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigShot : Skill
{
    public override void UseSkill()
    {
        GameObject projectil = PoolManager.Instance.Get("ProjectileBigShot");
        projectil.GetComponent<ProjectileBigShot>().Init(hero.AttackPoint.position,hero.AttackPoint.rotation, hero.GetHeroData().GetDamage());
        CurrentCooldown = skillData.GetCoolDown();
    }
}
