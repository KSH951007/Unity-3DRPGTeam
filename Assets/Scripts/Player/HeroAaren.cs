using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class HeroAaren : Hero
{
    protected override void Awake()
    {
        base.Awake();
        moveAction = new HeroMoveAction(animator, this, agent, 3.5f);
        attackAction = new HeroAttackAction(scheduler, animator, this, 2);
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
