using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private int gold;
    private int maxSlotCount;
    private Item[] items;
    public event Action onItemUpdate;

    private void Awake()
    {
        gold = 50000;
        maxSlotCount = 60;
        items = new Item[maxSlotCount];
    }
    public bool HasGold(int gold)
    {
        if (this.gold < gold)
            return false;

        return true;
    }
    public void AddBuyItem(ItemSO itemData, int count = 1)
    {

        if (!HasGold(itemData.GetItemBuyPrice()))
            return;



        if (itemData is CountableItemSO)
        {
            bool hasItem = HasItem(itemData, out int index);
            if (hasItem)
            {
                CountableItem countableItem = ((CountableItem)items[index]);

                if (countableItem.Count + count >= countableItem.MaxCount)
                {

                }
            }
            else
            {

            }

        }
        else if (itemData is EquipmentItemSO)
        {
            if (isEmptySlot(out int index))
            {
                items[index] = itemData.CreateItem();

                Debug.Log(items[index].itemData.GetItemName());
            }
            else
            {
                Debug.Log("아이템공간 부족");
                return;
            }

        }

        gold -= itemData.GetItemBuyPrice();
        onItemUpdate?.Invoke();
    }
    public bool HasItem(ItemSO itemData, out int index)
    {

        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null)
                continue;

            if (items[i].itemData.GetItemID() == itemData.GetItemID())
            {
                index = i;
                return true;
            }
        }
        index = 0;

        return false;
    }
    public bool isEmptySlot(out int index)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null)
            {
                index = i;
                return true;
            }
        }
        index = 0;
        return false;
    }
}
