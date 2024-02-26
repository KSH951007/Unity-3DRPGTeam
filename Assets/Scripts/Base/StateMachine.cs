using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class StateMachine<T1, T2> where T1 : Enum where T2 : class
{
    private List<BaseState<T1, T2>> states;
    private T1 currentState;
    private T2 reservationState;
    private Animator animator;
    private PlayerControls controls;


    public PlayerControls Controls { get { return controls; } }
    public Animator Animator { get { return animator; } }
    public StateMachine(Animator animator, PlayerControls controls)
    {
        states = new List<BaseState<T1, T2>>();
        this.animator = animator;
        this.controls = controls;
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
        Debug.Log(currentState);
        states[Convert.ToInt32(currentState)]?.Update();
    }
    public void FixedUpdate()
    {
        states[Convert.ToInt32(currentState)]?.FixedUpdate();
    }

}
