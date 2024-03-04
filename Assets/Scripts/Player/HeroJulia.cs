using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroJulia : Hero
{
    protected override void Awake()
    {
        base.Awake();
        moveAction = new HeroMoveAction(scheduler,animator, this, agent, 3.5f);
        attackAction = new HeroJuliaAttackAction(scheduler,animator, this);
    }
    // Start is called before the first frame update
    void Start()
    {
        Transform skillsTr = transform.Find("Skills");
        Skill[] skills = new Skill[3];
        for (int i = 0; i < skills.Length; i++)
        {
            skills[i] = skillsTr.GetChild(i).GetComponent<Skill>();
        }
        skillManager.AddSkill(this, skills);
        GetComponent<Health>().SetHealth(heroData.GetMaxHealth(),heroData.GetDefensive());

        skillAction = new HeroSkillAction[skillsTr.childCount];


        skillAction[0] = new HeroSkillAction(scheduler, skillManager, 0, animator, this);
        skillAction[1] = new HeroSkillAction(scheduler, skillManager, 1, animator, this);
        skillAction[2] = new HeroSkillAction(scheduler, skillManager, 2, animator, this);
    }

 
}
