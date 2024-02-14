using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState<T1,T2>  where T1 : Enum where T2 : class
{
    protected Animator animator;
    protected StateMachine<T1, T2> stateMachine;
    protected T2 owner;
    protected string stateName;
    public BaseState(Animator animator, T2 owner, StateMachine<T1, T2> stateMachine)
    {
        this.animator = animator;
        this.stateMachine = stateMachine;
        this.owner = owner;
    }
    public abstract void Enter();
    public abstract void Update();
    public virtual void FixedUpdate()
    {

    }
    public abstract void Exit();
}
