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
        attackAction = new HeroDinAttackAction(attackPoint,attackEffect,scheduler, animator, this, 3);
       
        
        skillAction[0] = new HeroSkillAction(scheduler,skillManager,0,animator, this);
        skillAction[1] = new HeroSkillAction(scheduler,skillManager, 1,animator, this);
    }

}
