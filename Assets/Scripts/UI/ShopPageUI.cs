using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopPageUI : MonoBehaviour
{

    [SerializeField] private List<ItemSO> equipItemDatas;
    [SerializeField] private List<ItemSO> usableItemDatas;
    [SerializeField] private Transform equipItemParent;
    [SerializeField] private Transform usableItemParent;
    [SerializeField] private GameObject shopItemSlotPrefab;

    private List<ShopItemSlot> equipItemSlots;
    private List<ShopItemSlot> usableItemSlots;
    [SerializeField] private CategoryController categoryController;
    [SerializeField] private Inventory inventory;

    private void Awake()
    {
        CreateSlots();

    }

    private void CreateSlots()
    {
        equipItemSlots = new List<ShopItemSlot>();
        usableItemSlots = new List<ShopItemSlot>();


        ToggleGroup group = equipItemParent.GetComponent<ToggleGroup>();
        for (int i = 0; i < equipItemDatas.Count; i++)
        {
            GameObject itemSlotObj = Instantiate(shopItemSlotPrefab, equipItemParent);
            ShopItemSlot itemSlot = itemSlotObj.GetComponent<ShopItemSlot>();
            Toggle itemToggle = itemSlotObj.GetComponent<Toggle>();
            itemToggle.group = group;
            itemToggle.isOn = false;
            itemSlot.SetItemInfo(i, equipItemDatas[i].GetRatingType(), equipItemDatas[i].GetItemIcon(), equipItemDatas[i].GetItemName(), equipItemDatas[i].GetSummary(), equipItemDatas[i].GetItemBuyPrice().ToString());
            equipItemSlots.Add(itemSlot);
        }
    }
    public void PressBuyButton()
    {
        for (int i = 0; i < equipItemSlots.Count; i++)
        {
            if (equipItemSlots[i].IsSelect == true)
            {
                Debug.Log(equipItemSlots[i]);
              

                inventory.AddBuyItem(equipItemDatas[i]);

                return;
            }
        }
    }

}
