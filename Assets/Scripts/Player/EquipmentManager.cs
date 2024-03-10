using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EquipmentManager : MonoBehaviour
{
    private Equipment[] equipments;
    private PortionItem[] portionItems;
    private int maxPortionItemSlotCount;
    private HeroManager heroManager;
    [SerializeField] private Inventory inventory;
    private void Awake()
    {
        maxPortionItemSlotCount = 6;
        portionItems = new PortionItem[maxPortionItemSlotCount];
        equipments = GetComponentsInChildren<Equipment>();
        heroManager = GetComponent<HeroManager>();
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
                    return equipments[i].SetItem(item);
                }
            }
        }
        return Equipment.EquipmentResult.Success;
    }
    public void SetPortionItem(Item item)
    {
        if (!(item is PortionItem))
            return;

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

    }
    public void ReleaseEquipmentItem(int heroIndex, int itemIndex)
    {
        EquipmentItem item = equipments[heroIndex].GetEquipmentItem((Equipment.EquipmentSlotType)itemIndex);
        inventory.SetItem(item);
        equipments[heroIndex].ReleaseEquipmentItem((Equipment.EquipmentSlotType)itemIndex);

    }

}
