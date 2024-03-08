using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Reward/EXP", fileName = "RewardExp_")]
public class ExpReward : Reward
{
    
    public override void Give(Quest quest)
    {
        GameManager.Instance.plin.GetEXP(Quantity);
    }
}
