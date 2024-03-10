using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemQuickSlotUI : MonoBehaviour
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private GameObject countPanel;
    [SerializeField] private TextMeshProUGUI countText;

    public void SetItemInfo(Item item)
    {
        if (item == null)
        {
            itemIcon.enabled = false;
            countPanel.SetActive(false);
        }
        else
        {
            itemIcon.enabled = true;
            itemIcon.sprite = item.itemData.GetItemIcon();
            countPanel.SetActive(true);
            countText.text = ((PortionItem)item).Count.ToString();

        }
    }
}
