using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroJulia : Hero
{
 
    protected override void Awake()
    {
   
        base.Awake();
        moveAction = new HeroMoveAction(scheduler, animator, this, agent, 3.5f, 2, "HeroJuliaStep");
        attackAction = new HeroJuliaAttackAction(scheduler,animator, this);
        for (int i = 0; i < skillAction.Length; i++)
        {
            skillAction[i].SetHeroName("HeroJulia");
        }
    }
    // Start is called before the first frame update
  

}
