
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "New Skill", menuName = "ScriptableObject/SkillSO")]
public class SkillSO : ScriptableObject
{
    [SerializeField] protected Sprite skillIcon;
    [SerializeField] protected string skillName;
    [SerializeField] protected string skillDescription;
    [SerializeField] protected float skillCooldown;
    [SerializeField] protected int skillManaCost;


    public Sprite GetIcon() { return skillIcon; }
    public string GetSkillName() { return skillName; }
    public string GetDescription() { return skillDescription; }
    public float GetCoolDown() { return skillCooldown; }
    public int GetManaCost() { return skillManaCost; }
}
