using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Reward/Gold", fileName = "RewardGold_")]

public class GoldReward : Reward
{
    public override void Give(Quest quest)
    {
        GameManager.Instance.inven.Gold += Quantity;
    }
}
