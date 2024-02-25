using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionScheduler
{
    private PlayerAction currentAction;
    private PlayerAction nextAction;
    private PlayerAction[] actions;
    private int actionIndex;
    public ActionScheduler()
    {
        actions = new PlayerAction[2];
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

    public void AddAction(PlayerAction action)
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

            if (actions[NextIndex()] != null)
            {
                actions[NextIndex()].StopAction();
            }

            actions[NextIndex()] = action;

        }


    }
    public void ProcessAction()
    {
        if (actions[actionIndex] != null)
        {
            if (actions[actionIndex].IsEndAction)
            {
                actions[actionIndex].StopAction();


                actions[actionIndex] = null;

                actionIndex = NextIndex();

                actions[actionIndex]?.StartAction();


                return;

            }

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
    public PlayerAction GetNextAction()
    {
        int nextIndex = NextIndex();

        if (actions[nextIndex] != null)
        {
            return actions[nextIndex];
        }
        return null;
    }

}
