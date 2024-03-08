using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CountableItem : Item
{
    public CountableItem(ItemSO itemData, int count) : base(itemData)
    {
        SetCount(count);
    }

    public int Count { get; protected set; }
    public int MaxCount => ((CountableItemSO)itemData).MaxCount;
    public bool IsEmpty => Count == 0;
    public bool IsMax => Count >= ((CountableItemSO)itemData).MaxCount;

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

    public CountableItem CountableItemClone(int count)
    {
        //if (count <= 1) return null;

        //if(count > Count -1)
        //    count = Count - 1;

        //Count -= count;
        return Clone(count);
    }

    protected abstract CountableItem Clone(int amount);
}
