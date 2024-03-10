using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class InventoyData
{
    public int gold;
}
public class Inventory : MonoBehaviour, ISavable
{
    public enum ItemBuyResultType { Success, TribeSlot, TribeGold }
    private int maxSlotCount;
    private Item[] items;
    public event Action<int> onItemUpdate;
    public event Action<int> onGoldUpdate;

    public InventoyData data;
    public Item[] InventroyItems { get => items; }
    public int MaxCount { get { return maxSlotCount; } }
    public int Gold { get { return data.gold; } set { data.gold = value; onGoldUpdate?.Invoke(data.gold); } }
    private void Awake()
    {
        maxSlotCount = 60;
        items = new Item[maxSlotCount];
        data = new InventoyData();

        ItemLoad<Weapon>("InventoryItems/Weapon/");
        ItemLoad<Armor>("InventoryItems/Armor/");
        ItemLoad<PortionItem>("InventoryItems/Portion/");
        if (DataManager.Instance.LoadData<InventoyData>("InventoryGold", out InventoyData gold))
        {
            data = gold;
        }
        else
        {
            data.gold = 50000;
        }


        DataManager.Instance.AddSaveHandler(this);
    }

    public void ItemLoad<T>(string path) where T : Item
    {
        int count = DataManager.Instance.GetFileCount(path);
        if (count > 0)
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (DataManager.Instance.LoadData(path + "Item" + i, out T weaponData))
                {
                    items[i] = (T)weaponData;

                }
            }
        }
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
        if (this.data.gold < gold)
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
            Debug.Log(remainingCount);
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
                    Debug.Log(remainingCount);
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
            {
                if (items[i] is Weapon)
                    DataManager.Instance.SaveData(items[i], $"Item{i}", "InventoryItems/Weapon/");
                else if (items[i] is Armor)
                    DataManager.Instance.SaveData(items[i], $"Item{i}", "InventoryItems/Armor/");
                else if (items[i] is PortionItem)
                    DataManager.Instance.SaveData(items[i], $"Item{i}", "InventoryItems/Portion/");


            }

        }
        DataManager.Instance.SaveData(data, "InventoryGold");

    }
}
