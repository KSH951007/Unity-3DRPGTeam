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
    private Toggle itemSelectToggle;
    private int slotIndex;
    private int itemIndex;


    public int ItemIndex { get { return itemIndex; } }
    public bool IsSelect { get => itemSelectToggle.isOn; }

    public Sprite ItemRatingSprite { get => itemRatingBackgroundImage.sprite; }
    private void Awake()
    {
        itemSelectToggle = GetComponent<Toggle>();
        itemSelectToggle.group = transform.parent.GetComponent<ToggleGroup>();
    }
    private void OnEnable()
    {
        itemSelectToggle.isOn = false;
    }
    public void SetItemSlotIndex(int newIndex)
    {
        slotIndex = newIndex;
    }

    public void SetItemInfo(int newItemIndex, ItemSO itemData)
    {
        if (itemData == null)
        {
            gameObject.SetActive(false);
            return;
        }
        else
        {
            if (!gameObject.activeSelf)
            {
                gameObject.SetActive(true);
                return;
            }
        }

        itemIndex = newItemIndex;
        if (itemData.GetRatingType() == Item.ItemRatingType.Normal)
            itemRatingBackgroundImage.sprite = ratingSprites[0];
        else if (itemData.GetRatingType() == Item.ItemRatingType.Rair)
            itemRatingBackgroundImage.sprite = ratingSprites[1];
        else if (itemData.GetRatingType() == Item.ItemRatingType.Unique)
            itemRatingBackgroundImage.sprite = ratingSprites[2];
        else if (itemData.GetRatingType() == Item.ItemRatingType.Legenery)
            itemRatingBackgroundImage.sprite = ratingSprites[3];

        itemIconImage.sprite = itemData.GetItemIcon();
        itemNameText.text = itemData.GetItemName();
        itemSummaryText.text = itemData.GetSummary();
        itemPriceText.text = $"АЁАн : {itemData.GetItemBuyPrice()}";
    }
    public void isEmptySlot()
    {


    }
    public void SelectItem(bool isOn)
    {
        selectFrame.SetActive(isOn);
    }


}
