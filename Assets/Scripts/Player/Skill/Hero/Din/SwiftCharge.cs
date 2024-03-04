using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwiftCharge : Skill
{
    private float chargeSpeed;
    private ProjectileSwiftCharge projectileSwiftChage;
    private Vector3 startPos;
    private float distance;
    private float progressTime;

    private float operationTime;
    protected override void Awake()
    {
        base.Awake();
        chargeSpeed = 20f;
        projectileSwiftChage = this.transform.GetChild(0).GetComponent<ProjectileSwiftCharge>();
        operationTime = 0.4f;
        distance = 3f;
        progressTime = distance / chargeSpeed;
    }
    public override void UseSkill()
    {
        startPos = hero.transform.position;
        Debug.Log(projectileSwiftChage);
        projectileSwiftChage.Init(operationTime, hero.GetHeroData().GetDamage());
        projectileSwiftChage.gameObject.SetActive(true);
        CurrentCooldown = skillData.GetCoolDown();
        StartCoroutine(SwiftChargeRoutine());
    }
    IEnumerator SwiftChargeRoutine()
    {
        float currentProgressTime = 0f;
        while (currentProgressTime < progressTime)
        {
            hero.transform.Translate(hero.transform.forward * chargeSpeed * Time.deltaTime, Space.World);
            currentProgressTime += Time.deltaTime;
            yield return null;
        }
    }

    public override void UpdateSkill()
    {
        base.UpdateSkill();
    }
}
