using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    Dictionary<Hero, Skill[]> skills;

    private void Awake()
    {
        skills = new Dictionary<Hero, Skill[]>();
    }
    public void AddSkill(Hero hero, Skill[] skill)
    {
        if (!skills.ContainsKey(hero))
        {
            skills[hero] = skill;
        }
    }
    public Skill GetSkill(Hero hero, int skillIndex)
    {
        if (skills.ContainsKey(hero))
        {
            return skills[hero][skillIndex];
        }
        return null;
    }
    public bool CanUseSkill(Hero hero, int skillIndex)
    {
        if (skills.ContainsKey(hero))
        {
            if (skills[hero][skillIndex].CurrentCooldown <= 0f)
            {
                return true;
            }
        }

        return false;
    }
    public void UseSkill(Hero hero, int skillIndex)
    {
        if (skills.ContainsKey(hero))
        {
            skills[hero][skillIndex].UseSkill();
            StartCoroutine(skills[hero][skillIndex].CoolDownRoutine());
            return;
        }
        Debug.Log("notfind");
    }
    public void SetCooldownTimer(IEnumerator cooldownRoutin)
    {
        StartCoroutine(cooldownRoutin);
    }

}
