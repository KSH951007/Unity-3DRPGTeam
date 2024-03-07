using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class EquipmentItem : Item
{
    public EquipmentType equipmentType;
    
    public EquipmentItem(ItemSO itemData) : base(itemData)
    {
    }

    
}
