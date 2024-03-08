using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopPageUI : MonoBehaviour
{

    [SerializeField] private List<ItemSO> equipItemDatas;
    [SerializeField] private List<ItemSO> usableItemDatas;
    [SerializeField] private Transform itemParent;
    [SerializeField] private GameObject shopItemSlotPrefab;

    private ShopItemSlot[] itemSlots;
    private int maxCount;
    [SerializeField] private CategoryController categoryController;
    [SerializeField] private Inventory inventory;


    private void Awake()
    {
        CreateSlots();

    }

    private void CreateSlots()
    {
        maxCount = equipItemDatas.Count + usableItemDatas.Count;
        itemSlots = new ShopItemSlot[maxCount];
        int index = 0;


        ToggleGroup group = itemParent.GetComponent<ToggleGroup>();
        for (int i = 0; i < equipItemDatas.Count; i++)
        {
            index = i;
            GameObject itemSlotObj = Instantiate(shopItemSlotPrefab, itemParent);
            ShopItemSlot itemSlot = itemSlotObj.GetComponent<ShopItemSlot>();
            Toggle itemToggle = itemSlotObj.GetComponent<Toggle>();
            itemToggle.group = group;
            itemToggle.isOn = false;
            itemSlot.SetItemInfo(i, equipItemDatas[i].GetRatingType(), equipItemDatas[i].GetItemIcon(), equipItemDatas[i].GetItemName(), equipItemDatas[i].GetSummary(), equipItemDatas[i].GetItemBuyPrice().ToString());
            itemSlots[index] = itemSlot;
        }
        Debug.Log(index);
        for (int i = 0; i < usableItemDatas.Count; i++)
        {
            int newIndex = index + 1 + i;
            Debug.Log(index);
            GameObject itemSlotObj = Instantiate(shopItemSlotPrefab, itemParent);
            ShopItemSlot itemSlot = itemSlotObj.GetComponent<ShopItemSlot>();
            Toggle itemToggle = itemSlotObj.GetComponent<Toggle>();
            itemToggle.group = group;
            itemToggle.isOn = false;
            itemSlot.SetItemInfo(i, usableItemDatas[i].GetRatingType(), usableItemDatas[i].GetItemIcon(), usableItemDatas[i].GetItemName(), usableItemDatas[i].GetSummary(), usableItemDatas[i].GetItemBuyPrice().ToString());
            itemSlots[newIndex] = itemSlot;
            Debug.Log(itemSlots[newIndex]);
        }
    }
    public void PressBuyButton()
    {
        //for (int i = 0; i < equipItemSlots.Count; i++)
        //{
        //    if (equipItemSlots[i].IsSelect == true)
        //    {
        //        Debug.Log(equipItemSlots[i]);


        //        inventory.AddBuyItem(equipItemDatas[i]);

        //        return;
        //    }
        //}
    }

}
