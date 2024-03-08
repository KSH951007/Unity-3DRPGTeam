using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPageUI : MonoBehaviour
{

    [SerializeField] private InventorySlotUI[] slots;


    public void ActiveAllSelectTogle()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].ActiveSelectToggle();
        }
    }

   
}
