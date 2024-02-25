using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerAction
{
    protected Hero owner;
    protected Animator animator;
    protected bool isEndAction;
    public void ChangeAnimator(Animator newAnimator)
    {
        
        animator = newAnimator;
    }
    public bool IsEndAction { get { return isEndAction; } }
    public abstract bool IsCanle(PlayerAction action);
    public PlayerAction(Animator animator, Hero owner)
    {
        this.animator = animator;
        this.owner = owner;
        isEndAction = false;
    }

    public abstract void StartAction();


    public abstract void UpdateAction();
    public abstract void StopAction();
}