using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EquipmentManager : MonoBehaviour, ISavable
{
    private Equipment[] equipments;
    private PortionItem[] portionItems;
    private int maxPortionItemSlotCount;
    private HeroManager heroManager;
    [SerializeField] private Inventory inventory;
    public event Action<int> onPortionItemUpdate;
    private void Awake()
    {
        maxPortionItemSlotCount = 6;
        portionItems = new PortionItem[maxPortionItemSlotCount];
        equipments = GetComponentsInChildren<Equipment>();
        heroManager = GetComponent<HeroManager>();

        for (int i = 0; i < portionItems.Length; i++)
        {

            if (DataManager.Instance.LoadData("Equipment/Portion/item" + i, out PortionItem portion))
            {
                portionItems[i] = portion;
            }

        }



        DataManager.Instance.AddSaveHandler(this);
    }

    public PortionItem[] GetPortionItems() { return portionItems; }
    public Equipment.EquipmentResult SetEquipment(Item item, int heroID)
    {
        if (item is EquipmentItem)
        {
            for (int i = 0; i < heroManager.MaxHeroCount; i++)
            {
                if (heroManager.GetSelectHero(i).GetHeroData().GetHeroID() == heroID)
                {
                    equipments[i].SetItem(item);
                    return;
                }
            }
        }
    }
    public void SetPortionItem(Item item)
    {

        bool isEmpty = false;
        int emptyIndex = 0;
        for (int i = 0; i < portionItems.Length; i++)
        {
            if (portionItems[i] == null)
            {
                emptyIndex = i;
                isEmpty = true;
                break;
            }

        }
        if (isEmpty == false)
        {
            Debug.Log("full");
            return;
        }

        PortionItem portion = item as PortionItem;

        if (inventory.HasItem(portion, out int index))
        {
            inventory.EraseItem(index);
        }
        portionItems[emptyIndex] = portion;
        onPortionItemUpdate?.Invoke(emptyIndex);
    }
    public Equipment GetEquipment(int heroIndex)
    {
        if (equipments == null)
            return null;

        return equipments[heroIndex];
    }
    public void ReleasePortionItem(int index)
    {
        inventory.SetItem(portionItems[index]);

        portionItems[index] = null;
        onPortionItemUpdate?.Invoke(index);
    }
    public void ReleaseEquipmentItem(int heroIndex, int itemIndex)
    {
        EquipmentItem item = equipments[heroIndex].GetEquipmentItem((Equipment.EquipmentSlotType)itemIndex);
        inventory.SetItem(item);
        equipments[heroIndex].ReleaseEquipmentItem((Equipment.EquipmentSlotType)itemIndex);

    }
    public void UsePortion(int index)
    {
        if (portionItems[index] == null)
        {
            Debug.Log(index);
            return;
        }



        Debug.Log(portionItems[index].portionType);
        Debug.Log(portionItems[index].itemName);

        if (portionItems[index].portionType == HealthPortionType.HpPortion)
        {
            Health health = heroManager.GetMainHero().GetComponent<Health>();
            if (health != null)
            {
                if (!health.CanHealing())
                    return;

                health.Healing(portionItems[index].health);
            }
        }
        else
        {
            ManaSystem manaSystem = heroManager.GetMainHero().GetComponent<ManaSystem>();
            if (!manaSystem.CanHealing())
                return;

            manaSystem.Healing(portionItems[index].health);

        }


        if (!portionItems[index].Use())
        {

            portionItems[index] = null;

        }



        onPortionItemUpdate?.Invoke(index);
    }

    public void SaveData()
    {
        for (int i = 0; i < portionItems.Length; i++)
        {
            if (portionItems[i] == null)
                continue;

            DataManager.Instance.SaveData(portionItems[i], "Item" + i, "Equipment/Portion/");
        }
    }
}
