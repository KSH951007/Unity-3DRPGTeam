using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{

    [SerializeField] protected SkillSO skillData;
    protected int skillLevel;
    protected float currentCooldown;
    protected virtual void Awake()
    {
        currentCooldown = 0f;
    }

    public abstract void UseSkill();

    public IEnumerator CoolDownRoutine()
    {
        currentCooldown = skillData.GetCoolDown();

        while (currentCooldown > 0f)
        {
            currentCooldown -= Time.deltaTime;
            yield return null;
        }
        currentCooldown = 0f;

    }

}
