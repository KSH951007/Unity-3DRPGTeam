using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum DefensiveItemType {Helmet,Armor,Glove,Shoes }
public class Armor : EquipmentItem
{
    public float defensivePercent;
    public DefensiveItemType defensiveType;

    public Armor(ItemSO itemData) : base(itemData)
    {
        defensivePercent = ((ArmorSO)itemData).GetDefensivePercent();
        defensiveType = ((ArmorSO)itemData).GetDefensiveItemType();
    }

}
