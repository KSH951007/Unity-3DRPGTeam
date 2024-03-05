using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{

    [SerializeField] protected SkillSO skillData;
    [SerializeField] protected Hero hero;
    protected int skillLevel;
    protected float currentCooldown;
    protected float cooldownFillAmount;

    public SkillSO SkillData {  get { return skillData; } }
    public float CurrentCooldown
    {
        get => currentCooldown;
        set
        {
            currentCooldown = value;
            cooldownFillAmount = currentCooldown / skillData.GetCoolDown();
        }
    }
    protected virtual void Awake()
    {
        CurrentCooldown = 0f;
    }
    public virtual bool CanUseSkill()
    {
        if (currentCooldown > 0f)
            return false;

        return true;
    }
    public abstract void UseSkill();
    public virtual void UpdateSkill()
    {
        if (CurrentCooldown > 0f)
        {
            CurrentCooldown -= Time.deltaTime;
            if (CurrentCooldown <= 0f)
                CurrentCooldown = 0f;
        }
    }
    public void SetCoolDown()
    {
        CurrentCooldown = skillData.GetCoolDown();

    }

}