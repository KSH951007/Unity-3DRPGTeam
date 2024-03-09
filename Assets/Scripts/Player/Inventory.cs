using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public enum ItemBuyResultType { Success, TribeSlot, TribeGold }
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
    public ItemBuyResultType AddBuyItem(ItemSO itemData, int count = 1)
    {

        if (!HasGold(itemData.GetItemBuyPrice()))
            return ItemBuyResultType.TribeGold;

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
                    return ItemBuyResultType.TribeSlot;
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
                return ItemBuyResultType.TribeSlot;
            }

        }

        Gold -= itemData.GetItemBuyPrice() * count;

        return ItemBuyResultType.Success;
    }
    public void SellItem(int index, int Count = 1)
    {
        int sellPrice = (int)(items[index].itemData.GetItemBuyPrice() * 0.5f) * Count;

        Gold += sellPrice;



        items[index] = null;
        //Array.Sort(items, (x, y) =>
        //{
        //    if (y == null)
        //        return 1;
        //    else
        //        return -1;
        //});
        onItemUpdate?.Invoke(index);


    }
    public void ProgressSortByDefault()
    {
        Array.Sort(items, (x, y) =>
        {
            if (x == null && y == null)
                return 0;
            else if (x == null && y != null)
                return 1;
            else if (y == null && x != null)
                return -1;
            else
                return 0;

        });
        for (int i = 0; i < items.Length; i++)
        {
            onItemUpdate?.Invoke(i);
        }
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
    public void ProgressSortByName()
    {
        CompareInfo compareInfo = CultureInfo.GetCultureInfo("ko-KR").CompareInfo;
        Array.Sort(items, (item1, item2) =>
        {
            if (item1 == null && item2 == null)
                return 0;
            else if (item1 == null && item2 != null)
                return 1;
            else if (item2 == null && item1 != null)
                return -1;
            else
            {
                return compareInfo.Compare(item1.itemData.GetItemName(), item2.itemData.GetItemName());

            }

        });

        for (int i = 0; i < items.Length; i++)
        {
            onItemUpdate?.Invoke(i);
        }
    }
    public void ProgressSortByRating()
    {
        Array.Sort(items, (item1, item2) =>
        {
            if (item1 == null && item2 == null)
                return 0;
            else if (item1 == null && item2 != null)
                return 1;
            else if (item2 == null && item1 != null)
                return -1;
            else
            {
                if (item1.itemData.GetRatingType() < item2.itemData.GetRatingType())
                {
                    return -1;
                }
                else if (item1.itemData.GetRatingType() == item2.itemData.GetRatingType())
                {
                    return 0;
                }
                else
                {
                    return 1;
                }

            }

        });

        for (int i = 0; i < items.Length; i++)
        {
            onItemUpdate?.Invoke(i);
        }
    }


}
