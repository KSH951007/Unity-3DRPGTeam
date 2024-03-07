using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionScheduler
{
    private HeroAction currentAction;
    private HeroAction nextAction;
    private HeroAction[] actions;
    private int actionIndex;
    public ActionScheduler()
    {
        actions = new HeroAction[2];
        actionIndex = 0;
    }
    public bool IsEmptyAction()
    {
        bool IsEmptyaction = true;

        for (int i = 0; i < actions.Length; i++)
        {
            if (actions[i] != null)
            {
                IsEmptyaction = false;
                break;
            }
        }
        return IsEmptyaction;
    }

    public void AddAction(HeroAction action)
    {
        if (IsEmptyAction())
        {
            actions[actionIndex] = action;
            actions[actionIndex].StartAction();
        }
        else
        {
            if (actions[actionIndex].IsCanle(action))
            {
                actions[actionIndex].StopAction();
                actions[actionIndex] = action;
                actions[actionIndex].StartAction();
                return;
            }



            actions[NextIndex()] = action;

        }
    }

    public void ChangeAction()
    {
        actions[actionIndex].StopAction();
        actions[actionIndex] = null;
        actionIndex = NextIndex();
        actions[actionIndex]?.StartAction();


        return;
    }
    public void ProcessAction()
    {
        if (actions[actionIndex] != null)
        {
            actions[actionIndex].UpdateAction();
        }
    }
    public void ResetActions()
    {
        for (int i = 0; i < actions.Length; i++)
        {
            if (actions[i] != null)
            {
                actions[i].StopAction();
                actions[i] = null;

            }
        }
    }
    public int NextIndex()
    {
        int nextIndex = actionIndex;
        nextIndex++;
        if (nextIndex >= actions.Length)
            nextIndex = 0;

        return nextIndex;
    }
    public HeroAction GetCurrentAction()
    {
        if (actions[actionIndex] != null)
        {
            return actions[actionIndex];
        }
        return null;
    }
    public HeroAction GetNextAction()
    {
        int nextIndex = NextIndex();

        if (actions[nextIndex] != null)
        {
            return actions[nextIndex];
        }
        return null;
    }

}
