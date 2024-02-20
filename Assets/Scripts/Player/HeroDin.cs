using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroDin : Hero
{

    protected override void Awake()
    {
        base.Awake();
        comboAttack = new ComboAttack(3);
        attackComboCount = 3;
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
