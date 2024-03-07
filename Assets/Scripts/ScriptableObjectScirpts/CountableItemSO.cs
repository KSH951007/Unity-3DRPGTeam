using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class CountableItemSO : ItemSO
{
    [SerializeField] protected int maxCount = 99;
    public int MaxCount { get { return maxCount; } }

}
