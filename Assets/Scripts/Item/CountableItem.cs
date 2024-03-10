using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CountableItem : Item
{
    public CountableItem(ItemSO itemData, int count) : base(itemData)
    {
        MaxCount = 99;
        SetCount(count);
    }

    public int MaxCount;
    public int Count { get; protected set; }

    public bool IsEmpty => Count == 0;
    public bool IsMax => Count >= MaxCount;

    public void SetCount(int count)
    {
        this.Count = Mathf.Clamp(count, 0, MaxCount);
    }
    public int AddCountAndGetExcess(int count)
    {
        int nextCount = Count + count;
        SetCount(nextCount);

        return (nextCount > MaxCount) ? (nextCount - MaxCount) : 0;
    }

}
