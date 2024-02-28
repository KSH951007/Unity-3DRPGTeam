using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill
{
    
    private int skillLevel;

    public abstract void StartSkill();
    public abstract void UpdateSkill();
    public abstract void EndSkill();

}
