using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemQuickSlotControlUI : MonoBehaviour
{
    [SerializeField] private ItemQuickSlotUI[] itemQuickSlots;
   private EquipmentManager equipmentManager;

    private void Awake()
    {
        equipmentManager = GameObject.Find("Player").transform.Find("Heros").GetComponent<EquipmentManager>();
    }
    private void Start()
    {

        for (int i = 0; i < itemQuickSlots.Length; i++)
        {
            UpdateSlot(i);
        }
        equipmentManager.onPortionItemUpdate += UpdateSlot;
    }

    public void UpdateSlot(int index)
    {
        itemQuickSlots[index].SetItemInfo(equipmentManager.GetPortionItems()[index]);
    }

}
