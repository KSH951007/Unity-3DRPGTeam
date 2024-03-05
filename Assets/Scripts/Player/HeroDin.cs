using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroDin : Hero
{

    [SerializeField] GameObject attackEffect;
 
    protected override void Awake()
    {
        base.Awake();
        attackAction = new HeroDinAttackAction(attackEffect,scheduler, animator, this);
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }
}
