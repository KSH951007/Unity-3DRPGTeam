using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveHole : Skill
{
    public override void UpdateSkill()
    {
        base.UpdateSkill();

    }

    public override void UseSkill()
    {
        GameObject projectil = PoolManager.Instance.Get("ProjectileLeavesHole");
        projectil.GetComponent<ProjectileLeaveHole>().Init(transform.position, hero.GetHeroData().GetDamage());
        CurrentCooldown = skillData.GetCoolDown();
    
    }
}
