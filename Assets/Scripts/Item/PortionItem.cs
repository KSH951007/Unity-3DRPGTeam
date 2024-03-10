using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortionItem : CountableItem, IUsableItem
{
    public int health;
    public HealthPortionType portionType;
    public PortionItem(ItemSO itemData, int count = 1) : base(itemData, count)
    {
        health = ((PortionItemSO)itemData).GetHealth();
        portionType = ((PortionItemSO)itemData).GetPortionType();

    }

    public bool Use()
    {
        Count--;

        if (Count <= 0)
            return false;

        return true;
    }


}
