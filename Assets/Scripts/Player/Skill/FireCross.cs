using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCross : Skill
{

    [SerializeField] private Hero hero;

    protected override void Awake()
    {
        base.Awake();
    }
    public override void UseSkill()
    {
        GameObject projectileFireCross = PoolManager.Instance.Get("ProjectileFireCross");
        projectileFireCross.transform.position = hero.transform.position;
        projectileFireCross.transform.rotation = Quaternion.FromToRotation(projectileFireCross.transform.forward, hero.transform.forward);
        projectileFireCross.GetComponent<ProjectileFireCross>().Init(hero.GetHeroData().GetDamage());

    }


}
