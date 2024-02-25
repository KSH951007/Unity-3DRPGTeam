using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSO : ScriptableObject
{
    [SerializeField] protected Sprite skillIcon;
    [SerializeField] protected string skillName;
    [SerializeField] protected string skillDescription;
    [SerializeField] protected float skillCooldown;
}
