using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipmentSlotUI : MonoBehaviour ,IPointerClickHandler
{
    [SerializeField] private Image selectImage;
    [SerializeField] private Image itemIcon;
    private bool isSelect;

    public bool IsSelect {  get { return isSelect; } }

    private void Awake()
    {
        isSelect = false;
    }

    public void SetItemInfo(Item item)
    {
        isSelect = false;
        selectImage.gameObject.SetActive(false);

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
    public void ActiveToggleSwich()
    {
        if (isSelect)
        {
            isSelect = false;
            selectImage.gameObject.SetActive(false);
        }
        else
        {           
            isSelect = true;
            selectImage.gameObject.SetActive(true);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ActiveToggleSwich();
    }
}
