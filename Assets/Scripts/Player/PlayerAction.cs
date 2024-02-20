using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerAction
{
    protected PlayerController owner;
    protected Animator animator;
    protected bool isEndAction;
    public bool IsEndAction { get { return isEndAction; } }
    public abstract bool IsCanle(PlayerAction action);
    public PlayerAction(Animator animator, PlayerController owner)
    {
        this.animator = animator;
        this.owner = owner;
        isEndAction = false;
    }

    public abstract void StartAction();


    public abstract void UpdateAction();
    public abstract void StopAction();
}
