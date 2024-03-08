using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemSlot : MonoBehaviour
{
    [SerializeField] private Sprite[] ratingSprites;
    [SerializeField] private Image itemRatingBackgroundImage;
    [SerializeField] private Image itemIconImage;
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemSummaryText;
    [SerializeField] private TextMeshProUGUI itemPriceText;
    [SerializeField] private GameObject selectFrame;
    private int slotIndex;
    private bool isSelect;
    public bool IsSelect { get { return isSelect; } }
    private void Awake()
    {
        isSelect = false;
    }

    public void SetItemInfo(int newSlotIndex, Item.ItemRatingType itemRatingType, Sprite IconSprite, string itemName, string itemSummary, string itemPrice)
    {
        slotIndex = newSlotIndex;
        if (itemRatingType == Item.ItemRatingType.Normal)
            itemRatingBackgroundImage.sprite = ratingSprites[0];
        else if (itemRatingType == Item.ItemRatingType.Rair)
            itemRatingBackgroundImage.sprite = ratingSprites[1];
        else if (itemRatingType == Item.ItemRatingType.Unique)
            itemRatingBackgroundImage.sprite = ratingSprites[2];
        else if (itemRatingType == Item.ItemRatingType.Legenery)
            itemRatingBackgroundImage.sprite = ratingSprites[3];

        itemIconImage.sprite = IconSprite;
        itemNameText.text = itemName;
        itemSummaryText.text = itemSummary;
        itemPriceText.text = $"АЁАн : {itemPrice}";
    }
    public void SelectItem(bool isOn)
    {
        isSelect = isOn;
        selectFrame.SetActive(isSelect);
    }


}
