using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwiftCharge : Skill
{
    [SerializeField] private Hero hero;
    private float chargeSpeed;
    protected override void Awake()
    {
        base.Awake();
        chargeSpeed = 10f;
        
    }
    public override void UseSkill()
    {

    }


}
