using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortionItem : CountableItem,IUsableItem
{
    public PortionItem(ItemSO itemData, int count=1) : base(itemData, count)
    {

    }

    public bool Use()
    {
        Count--;


        return true;
    }

    protected override CountableItem Clone(int amount)
    {
       return new PortionItem(itemData,amount);
    }

}
