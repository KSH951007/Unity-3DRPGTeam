using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroDin : Hero
{

    [SerializeField] GameObject attackEffect;
    [SerializeField] Transform attackPoint;
    protected override void Awake()
    {
        base.Awake();
        attackComboCount = 3;
        moveAction = new HeroMoveAction(scheduler,animator, this, agent, 3.5f);
           
     
    }
    private void Start()
    {
        Transform skillsTr = transform.Find("Skills");
        Skill[] skills = new Skill[skillsTr.childCount];
        for (int i = 0; i < skillsTr.childCount; i++)
        {
            skills[i] = skillsTr.GetChild(i).GetComponent<Skill>();
        }
        skillManager.AddSkill(this, skills);
        GetComponent<Health>().SetHealth(heroData.GetMaxHealth());
        skillAction = new HeroSkillAction[skillsTr.childCount];
        attackAction = new HeroDinAttackAction(attackPoint, attackEffect, scheduler, animator, this, 3);

        skillAction[0] = new HeroSkillAction(scheduler, skillManager, 0, animator, this);
        skillAction[1] = new HeroSkillAction(scheduler, skillManager, 1, animator, this);
    }
}
