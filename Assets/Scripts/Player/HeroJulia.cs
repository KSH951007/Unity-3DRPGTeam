using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroJulia : Hero
{
    protected override void Awake()
    {
        base.Awake();
        moveAction = new PlayerMoveAction(animator, this, agent, 3.5f);
        attackAction = new PlayerAttackAction(animator, this, 2);
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
