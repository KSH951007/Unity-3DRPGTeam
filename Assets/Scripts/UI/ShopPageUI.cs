using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopPageUI : CategoryPageUI
{
    public enum ShopPageType { All, WeaponPage, ArmorPage, PortionPage }


    [SerializeField] private List<ItemSO> itemDatas;
    [SerializeField] private Transform itemParent;
    [SerializeField] private GameObject shopItemSlotPrefab;
    private ShopPageType shopPageType;
    private ShopItemSlot[] itemSlots;
    [SerializeField] private Inventory inventory;
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private ScrollRect pageScrollRect;
    [SerializeField] private Toggle[] pageToggles;
    [SerializeField] private CountItemBuyActionUI countItemBuyActionUI;
    [SerializeField] private RenderModelViewUI renderModelViewUI;
    private void Awake()
    {
        CreateSlots();

    }
    private void OnEnable()
    {
        renderModelViewUI.ActiveModel(RenderTexModel.PreviewModelType.ShopNPC);
        renderModelViewUI.SetTalkText("반가워", "Awake");
    }
  
    private void Start()
    {
        goldText.text = inventory.Gold.ToString();
        inventory.onGoldUpdate += (gold) => goldText.text = gold.ToString();
    }
    private void CreateSlots()
    {
        itemSlots = new ShopItemSlot[itemDatas.Count];

        for (int i = 0; i < itemDatas.Count; i++)
        {
            GameObject itemSlotObj = Instantiate(shopItemSlotPrefab, itemParent);
            ShopItemSlot itemSlot = itemSlotObj.GetComponent<ShopItemSlot>();
            itemSlot.SetItemSlotIndex(i);
            itemSlot.SetItemInfo(i, itemDatas[i]);
            itemSlots[i] = itemSlot;
        }
        shopPageType = ShopPageType.All;
    }
    public void PressBuyButton()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (!itemSlots[i].gameObject.activeSelf)
                continue;

            if (itemSlots[i].IsSelect)
            {
                int itemIndex = itemSlots[i].ItemIndex;
                if (!inventory.HasGold(itemDatas[itemIndex].GetItemBuyPrice()))
                {
                    renderModelViewUI.SetTalkText("돈이 없는거 같아!!");
                    return;
                }

                ItemSO buyItem = itemDatas[itemIndex];

                if (itemDatas[itemIndex] is CountableItemSO)
                {
                    int maxCount = Mathf.Min(inventory.Gold / buyItem.GetItemBuyPrice(), 99);

                    countItemBuyActionUI.SetItemInfo(itemSlots[i].ItemRatingSprite, buyItem, maxCount, BuyItem);
                }
                else
                {
                    BuyItem(buyItem);

                }


                return;
            }

        }
    }
    public void BuyItem(ItemSO itemData, int count = 1)
    {

        Inventory.ItemBuyResultType type = inventory.AddBuyItem(itemData,count);
        switch (type)
        {
            case Inventory.ItemBuyResultType.Success:
                renderModelViewUI.SetTalkText("고마워~@@", "Awake");
                break;
            case Inventory.ItemBuyResultType.TribeSlot:
                renderModelViewUI.SetTalkText("아이템 공간이\n 부족한거같아!!");
                break;
            case Inventory.ItemBuyResultType.TribeGold:
                renderModelViewUI.SetTalkText("돈이 없는거 같아!!");
                break;
        }
    }
    public void ToggleItemPage(int typeIndex)
    {
        if (pageToggles[typeIndex].isOn)
        {
            ChangeItemSlot((ShopPageType)typeIndex);
        }
    }

    public void ChangeItemSlot(ShopPageType type)
    {
        if (shopPageType == type)
            return;

        shopPageType = type;

        int slotIndex = -1;
        for (int i = 0; i < itemDatas.Count; i++)
        {
            itemSlots[i].SelectItem(false);
            switch (type)
            {
                case ShopPageType.All:
                    itemSlots[i].SetItemInfo(i, itemDatas[i]);
                    break;
                case ShopPageType.WeaponPage:
                    if (itemDatas[i] is WeaponSO)
                    {
                        slotIndex++;
                        itemSlots[slotIndex].SetItemInfo(i, itemDatas[i]);
                    }
                    break;
                case ShopPageType.ArmorPage:
                    if (itemDatas[i] is ArmorSO)
                    {
                        slotIndex++;
                        itemSlots[slotIndex].SetItemInfo(i, itemDatas[i]);
                    }
                    break;
                case ShopPageType.PortionPage:
                    if (itemDatas[i] is CountableItemSO)
                    {
                        slotIndex++;
                        itemSlots[slotIndex].SetItemInfo(i, itemDatas[i]);
                    }
                    break;
            }

        }
        if (slotIndex != -1)
        {
            slotIndex++;
            for (int i = slotIndex; i < itemSlots.Length; i++)
            {
                itemSlots[i].SetItemInfo(i, null);
            }
        }
        pageScrollRect.content.anchoredPosition = new Vector2(pageScrollRect.content.anchoredPosition.x, 0f);
        pageScrollRect.velocity = Vector2.zero;


    }
}
