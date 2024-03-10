using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class RewardClearDetail : MonoBehaviour
{
    Item target;
    [SerializeField]
    private TextMeshProUGUI itemName;
    [SerializeField]
    private Image Icon;

    public void Setup(Item GetItemData)
    {
        target = GetItemData;
        Icon.sprite = GetItemData.itemData.GetItemIcon();
        itemName.text = GetItemData.itemName;
    }
}
