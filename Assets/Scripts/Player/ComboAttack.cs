using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboAttack
{
    Action nextAction;
    private int maxComboCount;
    private int currentComboCount;
    

    
    public ComboAttack(int maxComboCount)
    {
        currentComboCount = 0;
        this.maxComboCount = maxComboCount;
    }

    public void AddNextAttackAction(Action action)
    {
        currentComboCount++;
        if (currentComboCount >= maxComboCount - 1)
        {
            currentComboCount = 0;
            nextAction = null;
            return;
        }

        nextAction = action;
    }
    public void RemoveAttackAction()
    {
        nextAction = null;
    }
    public bool HasNextAttackAction()
    {
        return nextAction != null;
    }
    public void CallNextAttack()
    {
        nextAction?.Invoke();
        nextAction = null;
    }
    public int GetCurrentAttackCount()
    {
        return currentComboCount;
    }


}
