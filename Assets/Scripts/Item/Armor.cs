using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : EquipmentItem
{
    public Armor(ItemSO itemData) : base(itemData)
    {
        equipmentType = EquipmentType.Armor;
    }

}
