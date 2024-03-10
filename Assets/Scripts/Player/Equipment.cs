using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    public enum EquipmentSlotType { Weapon, Head, Body, Hand, Foot, Size }
    private EquipmentItem[] equipItems;
    [SerializeField] private Inventory inventory;

    private void Awake()
    {
        equipItems = new EquipmentItem[(int)EquipmentSlotType.Size];


     
    }
    public EquipmentItem GetEquipmentItem(EquipmentSlotType type)
    {
        return equipItems[(int)type];
    }
    public void SetItem(Item item)
    {


        if (item is Weapon)
        {
            Weapon weapon = equipItems[(int)EquipmentSlotType.Weapon] as Weapon;
            if (weapon != null)
            {
                if (inventory.HasItem(item, out int index))
                {
                    inventory.EraseItem(index);
                }

                inventory.SetItem(weapon);
                equipItems[(int)EquipmentSlotType.Weapon] = item as Weapon;

            }
            else
            {
                if (inventory.HasItem(item, out int index))
                {
                    inventory.EraseItem(index);
                    equipItems[(int)EquipmentSlotType.Weapon] = item as Weapon;
                }
            }

            Debug.Log(equipItems[(int)EquipmentSlotType.Weapon].itemName);


        }
        else if (item is Armor)
        {
            DefensiveItemType type = ((Armor)item).defensiveType;

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
