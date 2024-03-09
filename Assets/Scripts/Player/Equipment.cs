using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    public enum EquipmentSlotType { Weapon, Head, Body, Hand, Foot, Size }
    private EquipmentItem[] equipItems;

    private void Awake()
    {
        equipItems = new EquipmentItem[(int)EquipmentSlotType.Size];




    }
    public void SetItem(Item item)
    {
        if (item is Weapon)
        {
            Weapon weapon = equipItems[(int)EquipmentSlotType.Weapon] as Weapon;
            if (weapon != null)
            {

            }


        }
        else if (item is Armor)
        {
            DefensiveItemType type = ((ArmorSO)item.itemData).GetDefensiveItemType();

            if (type == DefensiveItemType.Helmet)
            {

            }
            else if (type == DefensiveItemType.Armor)
            {

            }
            else if (type == DefensiveItemType.Glove)
            {

            }
            else if (type == DefensiveItemType.Shoes)
            {

            }
        }





    }

}
