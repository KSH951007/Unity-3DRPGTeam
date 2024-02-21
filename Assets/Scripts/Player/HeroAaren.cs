using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class HeroAaren : Hero
{
    protected override void Awake()
    {
        base.Awake();
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
        //moveAction = new PlayerMoveAction(animator, this, agent, 3.5f);
        //attackAction = new PlayerAttackAction(animator, this, heroManager.GetMainHero(), 3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
