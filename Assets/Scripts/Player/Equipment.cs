using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour, ISavable
{
    public enum EquipmentSlotType { Weapon, Head, Body, Hand, Foot, Size }
    public enum EquipmentResult { Success, TypeMiss, LevelMiss, SlotFull }
    private EquipmentItem[] equipItems;
    [SerializeField] private Inventory inventory;
    public string heroName;
    private string path;
    private void Awake()
    {
        equipItems = new EquipmentItem[(int)EquipmentSlotType.Size];
        path = "Equipment/Hero/" + heroName;


        ItemLoad<Weapon>(EquipmentSlotType.Weapon, path + "/Weapon/");
        ItemLoad<Armor>(EquipmentSlotType.Head, path + "/Head/");
        ItemLoad<Armor>(EquipmentSlotType.Body, path + "/Body/");
        ItemLoad<Armor>(EquipmentSlotType.Hand, path + "/Hand/");
        ItemLoad<Armor>(EquipmentSlotType.Foot, path + "/Foot/");


        DataManager.Instance.AddSaveHandler(this);
    }
    public void ItemLoad<T>(EquipmentSlotType type, string path) where T : EquipmentItem
    {
        int count = DataManager.Instance.GetFileCount(path);
        if (count > 0)
        {
            if (DataManager.Instance.LoadData(path + "item", out T data))
            {
                equipItems[(int)type] = (T)data;
            }
        }
    }
    public EquipmentItem GetEquipmentItem(EquipmentSlotType type)
    {
        return equipItems[(int)type];
    }
    public void ReleaseEquipmentItem(EquipmentSlotType type)
    {
        HeroData heroData = GetComponent<Hero>().data;

        if (equipItems[(int)type] is Weapon)
        {
            heroData.damage -= ((Weapon)equipItems[(int)type]).weaponAttackPower;

        }
        else
        {
            heroData.defensivePercent -= ((Armor)equipItems[(int)type]).defensivePercent;
        }

        equipItems[(int)type] = null;
    }
    public EquipmentResult SetItem(Item item)
    {

        Hero hero = GetComponent<Hero>();

        if (item.itemLevel > hero.data.level)
        {
            return EquipmentResult.LevelMiss;
        }


        if (item is Weapon)
        {

            if (((Weapon)item).EquipHeroID != GetComponent<Hero>().data.heroID)
            {
                return EquipmentResult.TypeMiss;
            }



            Weapon weapon = equipItems[(int)EquipmentSlotType.Weapon] as Weapon;

            if (inventory.HasItem(item, out int index))
            {
                inventory.EraseItem(index);
            }
            if (weapon != null)
            {
                inventory.SetItem(weapon);
                hero.data.damage -= weapon.weaponAttackPower;
            }


            equipItems[(int)EquipmentSlotType.Weapon] = item as Weapon;
            hero.data.damage += ((Weapon)equipItems[(int)EquipmentSlotType.Weapon]).weaponAttackPower;

        }
        else if (item is Armor)
        {
            DefensiveItemType type = ((Armor)item).defensiveType;

            Armor armor = (Armor)item;

            if (type == DefensiveItemType.Helmet)
            {
                int slotType = (int)EquipmentSlotType.Head;

                if (inventory.HasItem(armor, out int index))
                {
                    inventory.EraseItem(index);
                }
                if (equipItems[slotType] != null)
                {
                    inventory.SetItem(equipItems[slotType]);
                    hero.data.defensivePercent -= ((ArmorSO)equipItems[slotType].itemData).GetDefensivePercent();
                    equipItems[slotType] = null;
                }
                equipItems[slotType] = armor;
                hero.data.defensivePercent += ((ArmorSO)equipItems[slotType].itemData).GetDefensivePercent();
            }
            else if (type == DefensiveItemType.Armor)
            {
                int slotType = (int)EquipmentSlotType.Body;

                if (inventory.HasItem(armor, out int index))
                {
                    inventory.EraseItem(index);
                }
                if (equipItems[slotType] != null)
                {
                    inventory.SetItem(equipItems[slotType]);
                    hero.data.defensivePercent -= ((ArmorSO)equipItems[slotType].itemData).GetDefensivePercent();
                    equipItems[slotType] = null;
                }
                equipItems[slotType] = armor;
                hero.data.defensivePercent += ((ArmorSO)equipItems[slotType].itemData).GetDefensivePercent();
            }
            else if (type == DefensiveItemType.Glove)
            {
                int slotType = (int)EquipmentSlotType.Hand;

                if (inventory.HasItem(armor, out int index))
                {
                    inventory.EraseItem(index);
                }
                if (equipItems[slotType] != null)
                {
                    inventory.SetItem(equipItems[slotType]);
                    hero.data.defensivePercent -= ((ArmorSO)equipItems[slotType].itemData).GetDefensivePercent();
                    equipItems[slotType] = null;
                }
                equipItems[slotType] = armor;
                hero.data.defensivePercent += ((ArmorSO)equipItems[slotType].itemData).GetDefensivePercent();
            }
            else if (type == DefensiveItemType.Shoes)
            {
                int slotType = (int)EquipmentSlotType.Foot;

                if (inventory.HasItem(armor, out int index))
                {
                    inventory.EraseItem(index);
                }
                if (equipItems[slotType] != null)
                {
                    inventory.SetItem(equipItems[slotType]);
                    hero.data.defensivePercent -= ((ArmorSO)equipItems[slotType].itemData).GetDefensivePercent();
                    equipItems[slotType] = null;
                }
                equipItems[slotType] = armor;
                hero.data.defensivePercent += ((ArmorSO)equipItems[slotType].itemData).GetDefensivePercent();
            }
        }

        return EquipmentResult.Success;

    }

    public void SaveData()
    {
        if (equipItems[(int)EquipmentSlotType.Weapon] != null)
        {
            DataManager.Instance.SaveData(equipItems[(int)EquipmentSlotType.Weapon], "item", path + "/Weapon/");
        }
        if (equipItems[(int)EquipmentSlotType.Head] != null)
        {
            DataManager.Instance.SaveData(equipItems[(int)EquipmentSlotType.Head], "item", path + "/Head/");
        }
        if (equipItems[(int)EquipmentSlotType.Body] != null)
        {
            DataManager.Instance.SaveData(equipItems[(int)EquipmentSlotType.Body], "item", path + "/Body/");
        }
        if (equipItems[(int)EquipmentSlotType.Hand] != null)
        {
            DataManager.Instance.SaveData(equipItems[(int)EquipmentSlotType.Hand], "item", path + "/Hand/");
        }
        if (equipItems[(int)EquipmentSlotType.Foot] != null)
        {
            DataManager.Instance.SaveData(equipItems[(int)EquipmentSlotType.Foot], "item", path + "/Foot/");
        }

    }
}
