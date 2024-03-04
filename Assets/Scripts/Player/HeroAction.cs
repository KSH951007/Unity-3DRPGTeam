using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HeroAction
{
    public enum HeroActionState {Start,Progress,End }
    protected Hero owner;
    protected Animator animator;
    protected bool isEndAction;
    protected ActionScheduler scheduler;
    public void ChangeAnimator(Animator newAnimator)
    {
        
        animator = newAnimator;
    }
    public bool IsEndAction { get { return isEndAction; } }
    public abstract bool IsCanle(HeroAction action);
    public HeroAction(ActionScheduler scheduler, Animator animator, Hero owner)
    {
        this.scheduler = scheduler;
        this.animator = animator;
        this.owner = owner;
        isEndAction = false;
    }

    public abstract void StartAction();


    public abstract void UpdateAction();
    public abstract void StopAction();
}
