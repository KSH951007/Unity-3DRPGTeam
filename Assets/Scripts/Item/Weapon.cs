using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Weapon : EquipmentItem
{
    public int weaponAttackPower;
    public int EquipHeroID;

    public Weapon(ItemSO itemData) : base(itemData)
    {
        weaponAttackPower = ((WeaponSO)itemData).GetAttackPower();
        EquipHeroID = ((WeaponSO)itemData).GetEquipHeroID();
    }
}
