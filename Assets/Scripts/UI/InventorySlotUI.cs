using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    [SerializeField] Sprite[] itemRatingSprite;
    [SerializeField] Sprite itemRatingDefaultSprite;
    [SerializeField] GameObject countPanel;
    [SerializeField] TextMeshProUGUI countText;
    [SerializeField] GameObject singleSelectImage;
    private int slotIndex;
    private int itemIndex;
    private Image ratingImage;
    private Image itemIconImage;
    private Toggle SelectToggle;
    public event Action<int> oniSSingleSelected;

    public int ItemIndex { get => itemIndex; }
    public bool IsActiveSingleSelect { get => singleSelectImage.activeSelf; }
    public bool isActiveSelect { get => SelectToggle.gameObject.activeSelf; }
    public bool isSelectToggleOn { get => SelectToggle.isOn; }
    private void Awake()
    {
        ratingImage = GetComponent<Image>();
        itemIconImage = transform.Find("Icon").GetComponent<Image>();
        SelectToggle = transform.Find("SelectToggle").GetComponent<Toggle>();
        itemIconImage.enabled = false;
        SelectToggle.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        SelectToggle.isOn = false;

    }
    private void OnDisable()
    {
        if (isActiveSelect)
            ActiveMultiSelectToggle();
    }
    public void SetItemSlotIndex(int index)
    {
        this.slotIndex = index;
    }
    public void SetInventorySlot(int itemIndex, Item item)
    {
        ActiveSingleSelect(false);

        if (item == null)
        {
            ratingImage.sprite = itemRatingDefaultSprite;
            itemIconImage.enabled = false;
            this.itemIndex = -1;
            countPanel.SetActive(false);
            return;
        }
        else
        {
            this.itemIndex = itemIndex;

            if (item is CountableItem)
            {
                countPanel.SetActive(true);
                countText.text = ((CountableItem)item).Count.ToString();
            }
            else
            {
                countPanel.SetActive(false);
            }

            itemIconImage.enabled = true;

            if (item.ratingType == Item.ItemRatingType.Normal)
                ratingImage.sprite = itemRatingSprite[0];
            else if (item.ratingType == Item.ItemRatingType.Rair)
                ratingImage.sprite = itemRatingSprite[1];
            else if (item.ratingType == Item.ItemRatingType.Unique)
                ratingImage.sprite = itemRatingSprite[2];
            else if (item.ratingType == Item.ItemRatingType.Legenery)
                ratingImage.sprite = itemRatingSprite[3];

            this.itemIconImage.sprite = item.itemData.GetItemIcon();
        }



    }
    public void ActiveSingleSelect(bool isOn)
    {
        singleSelectImage.SetActive(isOn);
    }
    public void ActiveMultiSelectToggle()
    {
        if (SelectToggle.gameObject.activeSelf)
        {
            SelectToggle.gameObject.SetActive(false);
        }
        else
        {
            SelectToggle.isOn = false;
            SelectToggle.gameObject.SetActive(true);

        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (itemIndex == -1)
            return;


    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (itemIndex == -1)
            return;


        oniSSingleSelected?.Invoke(slotIndex);

        if (singleSelectImage.activeSelf)
        {
            singleSelectImage.SetActive(false);
        }
        else
        {
            singleSelectImage.SetActive(true);
        }


    }
}
