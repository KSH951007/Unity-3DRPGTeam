using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPageUI : CategoryPageUI
{
    public enum InventoryPageType { All, EquipItemPage, UsableItemPage, MiscItemPage, Size }
    public enum InventorySortType { NameSort, RatingSort }
    public enum InventoryControlButtonType { Sort, MultiSelect, Use, Sell }
    private InventoryPageType inventoryPageType;
    private InventorySortType inventorySortType;
    [SerializeField] private GameObject InventroySlotPrefab;
    [SerializeField] private Transform slotParentTr;
    private InventorySlotUI[] slots;
    [SerializeField] private Toggle[] itemPageToggle;
    [SerializeField] private Toggle[] inventorySortToggles;
    [SerializeField] private Button[] inventoryControlButtons;
    [SerializeField] private RenderModelViewUI renderModelViewUI;
    [SerializeField] private HeroSelectUI heroSelectUI;
    [SerializeField] private EquipmentManager equipmentManager;
    [SerializeField] private Inventory inventory;
    private bool IsMultiSelect;
    private void OnEnable()
    {
        inventoryPageType = InventoryPageType.All;
        inventorySortType = InventorySortType.NameSort;
        inventorySortToggles[(int)inventorySortType].isOn = true;
        IsMultiSelect = false;
        if (slots != null)
        {
            ChangeInventoryPage(inventoryPageType);
        }

        renderModelViewUI.ActiveModel(RenderTexModel.PreviewModelType.InvenNPC);
        renderModelViewUI.SetTalkText("어서와", "Awake");

    }
    private void OnDisable()
    {
        IsMultiSelect = false;
        inventoryControlButtons[(int)InventoryControlButtonType.Use].interactable = true;
    }
    private void Start()
    {

        slots = new InventorySlotUI[inventory.InventroyItems.Length];
        for (int i = 0; i < inventory.InventroyItems.Length; i++)
        {
            slots[i] = Instantiate(InventroySlotPrefab, slotParentTr).GetComponent<InventorySlotUI>();
            slots[i].SetItemSlotIndex(i);
            slots[i].SetInventorySlot(i, inventory.InventroyItems[i]);
            slots[i].oniSSingleSelected += OnSingleSelected;
        }
        inventory.onItemUpdate += UpdateSlot;
    }
    public void ActiveInventoryItemPageToggle(int index)
    {
        if (itemPageToggle[index].isOn)
        {
            ChangeInventoryPage((InventoryPageType)index);
        }
    }
    public void ChangeInventoryPage(InventoryPageType newType)
    {
        //if (inventoryPageType == newType)
        //    return;

        inventoryPageType = newType;
        itemPageToggle[(int)inventoryPageType].isOn = true;
        RemoveSlot();
        int slotIndex = -1;
        for (int i = 0; i < inventory.InventroyItems.Length; i++)
        {

            switch (inventoryPageType)
            {
                case InventoryPageType.All:
                    slots[i].SetInventorySlot(i, inventory.InventroyItems[i]);
                    break;
                case InventoryPageType.EquipItemPage:
                    if (inventory.InventroyItems[i] is EquipmentItem)
                    {
                        slotIndex++;

                        slots[slotIndex].SetInventorySlot(i, inventory.InventroyItems[i]);
                    }
                    break;
                case InventoryPageType.UsableItemPage:
                    if (inventory.InventroyItems[i] is IUsableItem)
                    {
                        slotIndex++;
                        slots[slotIndex].SetInventorySlot(i, inventory.InventroyItems[i]);
                    }
                    break;
                case InventoryPageType.MiscItemPage:
                    //if (inventory.InventroyItems[i] is mis)
                    //{
                    //    slots[slotIndex].SetInventorySlot(i, inventory.InventroyItems[i]);
                    //}
                    break;
            }
            if (slots[i].ItemIndex == -1)
            {
                if (slots[i].isActiveSelect)
                {
                    slots[i].ActiveMultiSelectToggle();
                }
            }
        }
        if (slotIndex != -1)
        {
            slotIndex++;
            for (int i = slotIndex; i < slots.Length; i++)
            {
                slots[i].SetInventorySlot(i, null);
            }
        }
    }
    public void RemoveSlot()
    {

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].SetInventorySlot(i, null);
        }

    }

    public void UpdateSlot(int itemIndex)
    {
        ChangeInventoryPage(inventoryPageType);
        //for (int i = 0; i < slots.Length; i++)
        //{
        //    if (slots[i].ItemIndex == itemIndex)
        //    {
        //        slots[i].SetInventorySlot(itemIndex, inventory.InventroyItems[itemIndex]);
        //        return;
        //    }

        //}
    }
    public void PressMultiSelectButton()
    {
        if (IsMultiSelect)
            IsMultiSelect = false;
        else
        {
            IsMultiSelect = true;
        }

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].ItemIndex == -1)
                continue;

            slots[i].ActiveSingleSelect(false);
            slots[i].ActiveMultiSelectToggle();
        }

        if (IsMultiSelect)
        {
            inventoryControlButtons[(int)InventoryControlButtonType.Use].interactable = false;
        }
        else
        {
            inventoryControlButtons[(int)InventoryControlButtonType.Use].interactable = true;
            Debug.Log("active");
        }
    }
    public void PressSellButton()
    {
        if (IsMultiSelect)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].isActiveSelect)
                {
                    if (slots[i].isSelectToggleOn)
                    {
                        int conut = 1;
                        CountableItem countableItem = inventory.InventroyItems[slots[i].ItemIndex] as CountableItem;
                        if (countableItem != null)
                        {
                            conut = countableItem.Count;
                        }
                        inventory.SellItem(slots[i].ItemIndex, conut);

                    }
                    slots[i].DisableMultiSelectToggle();
                }
            }
        }
        else
        {
            for (int i = 0; i < slots.Length; i++)
            {

                if (slots[i].IsActiveSingleSelect)
                {
                    int conut = 1;
                    CountableItem countableItem = inventory.InventroyItems[slots[i].ItemIndex] as CountableItem;
                    if (countableItem != null)
                    {
                        conut = countableItem.Count;
                    }
                    Debug.Log(i);
                    inventory.SellItem(slots[i].ItemIndex, conut);
                    return;
                }

            }
        }

        IsMultiSelect = false;
        inventory.ProgressSortByDefault();
        ChangeInventoryPage(inventoryPageType);

    }
    public void PressEquipItemButton()
    {

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].IsActiveSingleSelect)
            {
                Item item = inventory.InventroyItems[slots[i].ItemIndex];
                if (item is PortionItem)
                {
                    equipmentManager.SetPortionItem(item);
                    Debug.Log("asd");
                }
                else
                {
                    heroSelectUI.ActiveHeroSelectUI(item, SetEquipmentResult);
                }

                return;
            }
        }
    }
    public void SetEquipmentResult(Item item, int heroID)
    {
        Equipment.EquipmentResult result = equipmentManager.SetEquipment(item, heroID);
        switch (result)
        {
            case Equipment.EquipmentResult.Success:
                renderModelViewUI.SetTalkText("장착 성공이야!", "Awake");
                break;
            case Equipment.EquipmentResult.TypeMiss:
                renderModelViewUI.SetTalkText("영웅이 쓸수있는 \n 무기가 아니야!", "Awake");
                break;
            case Equipment.EquipmentResult.LevelMiss:
                renderModelViewUI.SetTalkText("레벨이 부족한걸?!", "Awake");
                break;
            case Equipment.EquipmentResult.SlotFull:
                renderModelViewUI.SetTalkText("더 넣을 공간이 없어", "Awake");
                break;
        }
    }
    public void ActiveSordToggle(int index)
    {
        if (inventorySortToggles[index].isOn)
        {
            inventorySortType = (InventorySortType)index;
        }


    }
    public void PressSortButton()
    {
        if (inventorySortType == InventorySortType.NameSort)
        {
            inventory.ProgressSortByName();
        }
        else if (inventorySortType == InventorySortType.RatingSort)
        {
            inventory.ProgressSortByRating();
        }
        ChangeInventoryPage(inventoryPageType);
    }
    public void OnSingleSelected(int slotIndex)
    {
        if (IsMultiSelect)
            return;


        for (int i = 0; i < slots.Length; i++)
        {
            if (i == slotIndex)
                continue;

            slots[i].ActiveSingleSelect(false);
        }

    }


}
