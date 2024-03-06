using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class HeroAaren : Hero
{
    protected override void Awake()
    {
        base.Awake();
        attackAction = new HeroAarenAttackAction(scheduler, animator, this);
    }
    protected override void Start()
    {
        base.Start();
    }
}
