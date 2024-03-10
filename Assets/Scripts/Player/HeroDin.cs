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
        attackAction = new HeroDinAttackAction(attackEffect, scheduler, animator, this);
    }

}
