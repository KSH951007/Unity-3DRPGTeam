using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlotUI : MonoBehaviour
{

    private Image itemIcon;


    private void Awake()
    {
        itemIcon = transform.Find("ItemIcon").GetComponent<Image>();
        itemIcon.enabled = false;
    }

    public void SetItemInfo(Item item)
    {
        if (item != null)
        {
            if (!itemIcon.enabled)
                itemIcon.enabled = true;

            itemIcon.sprite = item.itemData.GetItemIcon();

        }
        else
        {
            itemIcon.enabled = false;
        }

    }
}
