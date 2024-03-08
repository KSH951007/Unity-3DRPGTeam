using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPageUI : MonoBehaviour
{
    [SerializeField] private List<ItemSO> equipItemDatas;
    [SerializeField] private List<ItemSO> usableItemDatas;
    [SerializeField] private Transform equipItemParent;
    [SerializeField] private Transform usableItemParent;
    [SerializeField] private GameObject shopItemSlotPrefab;

    private List<ShopItemSlot> equipSlots;
    private List<ShopItemSlot> usableSlots;


    private void Awake()
    {
        CreateSlots();

    }

    private void CreateSlots()
    {
        equipSlots = new List<ShopItemSlot>();
        usableSlots = new List<ShopItemSlot>();


        for (int i = 0; i < equipItemDatas.Count; i++)
        {
            GameObject itemSlotObj = Instantiate(shopItemSlotPrefab, equipItemParent);
            ShopItemSlot itemSlot = itemSlotObj.GetComponent<ShopItemSlot>();
            itemSlot.SetItemInfo(i, equipItemDatas[i].GetRatingType(), equipItemDatas[i].GetItemIcon(), equipItemDatas[i].GetItemName(), equipItemDatas[i].GetSummary(), equipItemDatas[i].GetItemBuyPrice().ToString());
            equipSlots.Add(itemSlot);
        }
    }


}
