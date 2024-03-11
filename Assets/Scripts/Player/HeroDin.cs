using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroDin : Hero
{

    [SerializeField] GameObject attackEffect;



    protected override void Awake()
    {
 
        base.Awake();
        moveAction = new HeroMoveAction(scheduler, animator, this, agent, 3.5f,2,"HeroDinStep");
        attackAction = new HeroDinAttackAction(attackEffect, scheduler, animator, this);

        for (int i = 0; i < skillAction.Length; i++)
        {
            skillAction[i].SetHeroName("HeroDin");
        }
    }
    // Start is called before the first frame update
 
}
