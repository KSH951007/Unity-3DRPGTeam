using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class HeroAaren : Hero
{
    protected override void Awake()
    {
        base.Awake();
        moveAction = new HeroMoveAction(scheduler, animator, this, agent, 3.5f, 1, "HeroAarenStep");
        attackAction = new HeroAarenAttackAction(scheduler, animator, this);
        for (int i = 0; i < skillAction.Length; i++)
        {
            skillAction[i].SetHeroName("HeroAaren");
        }
    }

}
