using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill
{
    
    
    private int skillLevel;
    protected float currentCooldown;

    public Skill()
    {
        currentCooldown = 0f;
    }
    public abstract void StartSkill();
    public abstract void UpdateSkill();
    public abstract void EndSkill();

}
