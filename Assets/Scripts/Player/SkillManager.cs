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
            if (skills[hero][skillIndex].CanUseSkill())
                return true;
        }

        return false;
    }
    public void UseSkill(Hero hero, int skillIndex)
    {
        if (skills.ContainsKey(hero))
        {
            skills[hero][skillIndex].UseSkill();
            return;
        }
    }
    private void Update()
    {
        foreach (var skill in skills)
        {
            for (int i = 0; i < skill.Value.Length; i++)
            {
                skill.Value[i].UpdateSkill();
            }
        }
    }


}
