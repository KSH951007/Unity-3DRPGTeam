using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class Inventory : MonoBehaviour, ISavable
{
    public enum ItemBuyResultType { Success, TribeSlot, TribeGold }
    private int gold;
    private int maxSlotCount;
    private Item[] items;
    public event Action<int> onItemUpdate;
    public event Action<int> onGoldUpdate;
    private List<ItemSO> itemDatas;


    public Item[] InventroyItems { get => items; }
    public int MaxCount { get { return maxSlotCount; } }
    public int Gold { get { return gold; } set { gold = value; onGoldUpdate?.Invoke(gold); } }
    private void Awake()
    {
        Gold = 50000;
        maxSlotCount = 60;
        items = new Item[maxSlotCount];


        int count = DataManager.Instance.GetFileCount("InventoryItems/");
        Debug.Log(count);
        for (int i = 0; i < count; i++)
        {
            if (DataManager.Instance.LoadData("InventoryItems/Item" + i, out Weapon data))
            {

                items[i] = data;

                Debug.Log(items[i]);
            }
        }


       






        DataManager.Instance.AddSaveHandler(this);
    }
    private void Start()
    {
        for (int i = 0; i < items.Length; i++)
        {

            onItemUpdate?.Invoke(i);
        }
    }
    public bool HasGold(int gold)
    {
        if (this.gold < gold)
            return false;

        return true;
    }
    public bool HasItem(Item item, out int index)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == item)
            {
                index = i;
                return true;
            }
        }
        index = -1;
        return false;

    }
    public void SetItem(Item item)
    {
        if (item is EquipmentItem)
        {
            if (isEmptySlot(out int index))
            {
                items[index] = item;
                Debug.Log(items[index].itemName);
                onItemUpdate?.Invoke(index);
                return;
            }
        }
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
        int sellPrice = (int)(items[index].itemBuyPrice * 0.5f) * Count;

        Gold += sellPrice;
        EraseItem(index);


    }
    public void EraseItem(int index)
    {
        items[index] = null;
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

            if (items[i].itemID == itemData.GetItemID())
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
                return compareInfo.Compare(item1.itemName, item2.itemName);

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
                if (item1.ratingType < item2.ratingType)
                {
                    return -1;
                }
                else if (item1.ratingType == item2.ratingType)
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

    public void SaveData()
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] != null)
                DataManager.Instance.SaveData(items[i], $"Item{i}", "InventoryItems/");

        }

    }
}
