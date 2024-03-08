using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Reward/Item", fileName = "RewardItem_")]

public class ItemReward : Reward
{
    [SerializeField]
    ItemSO targetItem;
    public override void Give(Quest quest)
    {
        //TODO : 인벤토리 구현후 들어감.
    }
}
