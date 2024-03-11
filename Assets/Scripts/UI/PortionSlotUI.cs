using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PortionSlotUI : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image itemIconImage;
    [SerializeField] private Image selectImage;
    [SerializeField] private GameObject itemCountPanel;
    [SerializeField] private TextMeshProUGUI itemCountText;
    private bool isSelect;
    private bool isEmpty;

    public bool IsEmpty { get { return isEmpty; } }
    public bool IsSelect { get { return isSelect; } }
    private void Awake()
    {
        isSelect = false;
        isEmpty = true;
    }
    private void OnDisable()
    {
        isSelect = false;
        selectImage.gameObject.SetActive(false);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        ActiveToggleSwich();
    }
    public void SetItemInfo(Item item)
    {
        isSelect = false;
        selectImage.gameObject.SetActive(false);
        if (item == null)
        {
            isEmpty = true;
            itemIconImage.enabled = false;
            itemCountPanel.SetActive(false);
        }
        else
        {
            itemCountPanel.SetActive(true);
            itemCountText.text = ((PortionItem)item).Count.ToString();
            itemIconImage.enabled = true;
            isEmpty = false;
            itemIconImage.sprite = item.itemData.GetItemIcon();
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
            if (isEmpty)
                return;

            isSelect = true;
            selectImage.gameObject.SetActive(true);
        }
        SoundManager.instance.PlaySound("UIToggle");
    }




}
