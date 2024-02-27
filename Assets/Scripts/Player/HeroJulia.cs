using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroJulia : Hero
{
    protected override void Awake()
    {
        base.Awake();
        moveAction = new HeroMoveAction(scheduler,animator, this, agent, 3.5f);
        attackAction = new HeroAttackAction(scheduler,animator, this, 2);
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
