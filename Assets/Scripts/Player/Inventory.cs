using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private int gold;
    private int maxSlotCount;
    private Item[] items;
    public event Action<int> onItemUpdate;
    public event Action<int> onGoldUpdate;



    public Item[] InventroyItems { get => items; }
    public int MaxCount { get { return maxSlotCount; } }
    public int Gold { get { return gold; } set { gold = value; onGoldUpdate?.Invoke(gold); } }
    private void Awake()
    {
        Gold = 50000;
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

            int remainingCount = count;
            if (hasItem)
            {
                for (int i = index; i < items.Length; i++)
                {
                    if (items[i] is CountableItem)
                    {

                        CountableItem countableItem = ((CountableItem)items[i]);
                        remainingCount = countableItem.AddCountAndGetExcess(remainingCount);

                        onItemUpdate?.Invoke(i);
                        if (remainingCount > 0)
                        {
                            continue;

                        }
                        else
                        {
                            break;
                        }
                    }

                }
                if (remainingCount > 0)
                {
                    CountableItemSO countableItemSO = itemData as CountableItemSO;
                    if (countableItemSO != null)
                    {
                        CountableItem newCountableItem = ((CountableItem)countableItemSO.CreateItem());
                        remainingCount--;
                        remainingCount = newCountableItem.AddCountAndGetExcess(remainingCount);

                        if (isEmptySlot(out int emptyIndex))
                        {
                            items[emptyIndex] = newCountableItem;
                            onItemUpdate?.Invoke(emptyIndex);
                        }
                    }

                }
            }
            else
            {
                if (isEmptySlot(out int emptyIndex))
                {
                    items[emptyIndex] = itemData.CreateItem();
                    ((CountableItem)items[emptyIndex]).AddCountAndGetExcess(remainingCount);
                    onItemUpdate?.Invoke(emptyIndex);
                }
                else
                {
                    Debug.Log("공간없음");
                }

            }

        }
        else if (itemData is EquipmentItemSO)
        {
            if (isEmptySlot(out int index))
            {
                items[index] = itemData.CreateItem();
                onItemUpdate?.Invoke(index);

            }
            else
            {
                Debug.Log("아이템공간 부족");
                //TODO : 메세지박스(팝업UI) 띄울예정
                return;
            }

        }

        Gold -= itemData.GetItemBuyPrice() * count;
        // onItemUpdate?.Invoke();
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
