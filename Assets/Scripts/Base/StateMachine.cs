using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<T1, T2> where T1 : Enum where T2 : class
{
    private List<BaseState<T1, T2>> states;
    private T1 currentState;
    private Animator animator;


    public Animator GetAnimator { get { return animator; } }
    public StateMachine(Animator animator)
    {
        states = new List<BaseState<T1, T2>>();
        this.animator = animator;
    }
    public void AddState(BaseState<T1, T2> state)
    {
        if (states.Contains(state))
            return;

        states.Add(state);
    }

    public BaseState<T1, T2> GetState(T1 state)
    {
        return states[Convert.ToInt32(state)];
    }
    public void ChangeState(T1 newState)
    {
        states[Convert.ToInt32(currentState)]?.Exit();
        currentState = newState;
        states[Convert.ToInt32(currentState)].Enter();
    }
    public void Update()
    {
        states[Convert.ToInt32(currentState)]?.Update();
    }
    public void FixedUpdate()
    {
        states[Convert.ToInt32(currentState)]?.FixedUpdate();
    }

}
