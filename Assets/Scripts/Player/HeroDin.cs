using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroDin : Hero
{

    protected override void Awake()
    {
        base.Awake();
        attackComboCount = 3;
        moveAction = new PlayerMoveAction(animator, this, agent, 3.5f);
        attackAction = new PlayerAttackAction(animator, this, 3);
    }
    public override void Atacck()
    {

    }

    public override void Skill1()
    {
    }

    public override void Skill2()
    {

    }

    public override void Skill3()
    {

    }

}
