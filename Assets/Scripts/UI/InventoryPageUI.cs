using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPageUI : MonoBehaviour
{
    public enum InventoryPageType { All, EquipItemPage, UsableItemPage, MiscItemPage, Size }
    private InventoryPageType inventoryPageType;
    [SerializeField] private GameObject InventroySlotPrefab;
    [SerializeField] private Transform slotParentTr;
    private InventorySlotUI[] slots;
    [SerializeField] private Toggle[] itemPageToggle;
    [SerializeField] private Inventory inventory;


    private void OnEnable()
    {
        inventoryPageType = InventoryPageType.All;
        if (slots != null)
        {
            ChangeInventoryPage(inventoryPageType);

        }

    }
    private void Start()
    {

        slots = new InventorySlotUI[inventory.InventroyItems.Length];
        for (int i = 0; i < inventory.InventroyItems.Length; i++)
        {
            slots[i] = Instantiate(InventroySlotPrefab, slotParentTr).GetComponent<InventorySlotUI>();
            slots[i].SetItemSlotIndex(i);
            slots[i].SetInventorySlot(i, inventory.InventroyItems[i]);
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
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].ItemIndex == itemIndex)
            {
                slots[i].SetInventorySlot(itemIndex, inventory.InventroyItems[itemIndex]);
                return;
            }

        }
    }

}
